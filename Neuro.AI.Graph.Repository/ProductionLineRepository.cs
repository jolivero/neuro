using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class ProductionLineRepository
    {
        private readonly IDbConnection _db;

        public ProductionLineRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        public async Task<IEnumerable<ProductionLineBasicInfo>> Select_productionLines_basic(string lineId)
        {
            var sp = "sp_select_productionLine_basic";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);

            try
            {
                return await _db.QueryAsync<ProductionLineBasicInfo>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ProductionLine>> Select_productionLines_with_details(string lineId)
        {
            var sp = "sp_select_productionLine_details";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);

            var productionLineDict = new Dictionary<string, ProductionLine>();

            try
            {
                await _db.QueryAsync<ProductionLine, Group, Station, Machine, Part, Inventory, ProductionLine>(
                    sp,
                    (line, group, station, machine, part, inventory) =>
                    {
                        if (!productionLineDict.TryGetValue(line.LineId.ToString(), out var productionLineData))
                        {
                            productionLineData = line;
                            productionLineData.Groups = [];
                            productionLineDict.Add(line.LineId.ToString(), productionLineData);
                        }

                        var groupData = productionLineData.Groups.FirstOrDefault(ln => ln.GroupId == group.GroupId);
                        if (groupData == null)
                        {
                            groupData = group;
                            groupData.Stations = [];
                            productionLineData.Groups.Add(groupData);
                        }

                        var stationData = groupData.Stations.FirstOrDefault(g => g.StationId == station.StationId);
                        if (stationData == null)
                        {
                            stationData = station;
                            stationData.Machines = [];
                            stationData.Parts = [];
                            groupData.Stations.Add(stationData);
                        }

                        if (machine != null && !stationData.Machines.Any(s => s.MachineId == machine.MachineId)) stationData.Machines.Add(machine);
                        if (part != null && !stationData.Parts.Any(s => s.PartId == part.PartId))
                        {
                            if (inventory != null && part.PartId == inventory.PartId) part.Inventory = inventory;
                            stationData.Parts.Add(part);
                        }

                        return productionLineData;
                    },
                    p,
                    splitOn: "GroupId, StationId, MachineId, PartId, InventoryId",
                    commandType: CommandType.StoredProcedure
                    );

                return productionLineDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return productionLineDict.Values;
            }
        }

        public async Task<IEnumerable<ProductionLine>> Select_productionLine_recipe(string taskId, string userId)
        {
            var sp_lineId = "sp_select_lineId_from_recipe";
            var p1 = new DynamicParameters();
            p1.Add("@TaskId", taskId);
            p1.Add("@UserId", userId);

            var lineId = await _db.QueryFirstAsync<Guid>(
                sp_lineId,
                p1,
                commandType: CommandType.StoredProcedure
            );

            return await Select_productionLines_with_details(lineId.ToString());
        }

        public async Task<IEnumerable<ProductionLineMachineHoursPerCut>> Select_productionLines_with_machineHoursPerCut(string lineId)
        {
            var sp = "sp_productionLines_with_machineHoursPerCut";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);

            return await _db.QueryAsync<ProductionLineMachineHoursPerCut>(
                sp,
                p,
                commandType: CommandType.StoredProcedure
            );
        }
        #endregion

        #region Mutations

        public async Task<string> Create_productionLine(ProductionLineDto plDto)
        {
            var sp = "sp_create_update_productionLine";
            var p = new DynamicParameters();
            p.Add("@Name", plDto.Name);
            p.Add("@CompanyId", plDto.CompanyId);
            p.Add("@CreatedBy", plDto.CreatedBy);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Update_productionLine(string lineId, ProductionLineDto plDto)
        {
            var sp = "sp_create_update_productionLine";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@Name", plDto.Name);
            p.Add("@CompanyId", plDto.CompanyId);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Create_productionLine_steps(ProductionLineConfigDto plConfigDto)
        {
            var sp = "sp_create_update_productionLine_steps";
            var p = new DynamicParameters();
            p.Add("@LineId", plConfigDto.LineId);
            p.Add("@GroupId", plConfigDto.Steps.GroupId);
            p.Add("@StationId", plConfigDto.Steps.StationId);
            p.Add("@MachineId", plConfigDto.Steps.MachineId);
            p.Add("@PartId", plConfigDto.Steps.PartId);
            p.Add("@PrevPartId", plConfigDto.Steps.PrevPartId);
            p.Add("@Quantity", plConfigDto.Steps.RequiredQuantity);
            p.Add("@StepOrder", plConfigDto.Steps.StepOrder);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Update_productionLine_steps(ProductionLineHandleStepDto plUpdateDto)
        {
            var sp = "sp_create_update_productionLine_steps";
            var p = new DynamicParameters();
            p.Add("@RecipeId", plUpdateDto.RecipeId);
            p.Add("@LineId", plUpdateDto.LineId);
            p.Add("@GroupId", plUpdateDto.Steps.GroupId);
            p.Add("@StationId", plUpdateDto.Steps.StationId);
            p.Add("@MachineId", plUpdateDto.Steps.MachineId);
            p.Add("@PartId", plUpdateDto.Steps.PartId);
            p.Add("@PrevPartId", plUpdateDto.Steps.PrevPartId);
            p.Add("@Quantity", plUpdateDto.Steps.RequiredQuantity);
             p.Add("@StepOrder", plUpdateDto.Steps.StepOrder);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Update_productionLine_stepOrder(List<RecipeStepOrderDto> stepOrderDto)
        {
            var sp = "sp_update_productionLineRecipe_stepOrder";
            var p = new DynamicParameters();
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {

                foreach (var stepOrder in stepOrderDto)
                {
                    p.Add("@RecipeId", stepOrder.RecipeId);
                    p.Add("@StepOrder", stepOrder.StepOrder);

                    await _db.ExecuteAsync(
                        sp,
                        p,
                        commandType: CommandType.StoredProcedure
                    );
                }

                return p.Get<string>("@Message");

            }
            catch (Exception ex)
            {
                return $"Error en orden de paso {ex.Message}";
            }
        }
        
        public async Task<string> Delete_productionLine_steps(ProductionLineHandleStepDto plDeleteDto)
        {
            var sp = "sp_delete_productionLine_step";
            var p = new DynamicParameters();
            p.Add("@RecipeId", plDeleteDto.RecipeId);
            p.Add("@LineId", plDeleteDto.LineId);
            p.Add("@GroupId", plDeleteDto.Steps.GroupId);
            p.Add("@StationId", plDeleteDto.Steps.StationId);
            p.Add("@MachineId", plDeleteDto.Steps.MachineId);
            p.Add("@PartId", plDeleteDto.Steps.PartId);
            p.Add("@PrevPartId", plDeleteDto.Steps.PrevPartId);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
