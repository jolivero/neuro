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

        public async Task<IEnumerable<ProductionLineBasicInfo>> Select_productionLines_basic(int lineId)
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

        public async Task<IEnumerable<ProductionLineWithMaterial>> Select_productionLines_with_materials(int lineId)
        {
            var sp = "sp_select_productionLine_with_material";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);

            var productionLineDict = new Dictionary<int, ProductionLineWithMaterial>();

            try
            {
                await _db.QueryAsync<ProductionLineWithMaterial, ProductionLineMaterials, Part, Inventory, ProductionLineWithMaterial>(
                    sp,
                    (plMaterial, material, part, inventory) =>
                    {
                        if (!productionLineDict.TryGetValue(plMaterial.RecipeId, out var productionLineData))
                        {
                            productionLineData = plMaterial;
                            productionLineData.ProductionLineMaterials = [];
                            productionLineDict.Add(plMaterial.RecipeId, productionLineData);
                        }

                        var plMaterialData = productionLineData.ProductionLineMaterials.FirstOrDefault(m => m.MaterialId == material.MaterialId);
                        if (plMaterialData == null)
                        {
                            plMaterialData = material;
                            plMaterialData.MaterialInfo = part;
                            plMaterialData.MaterialInfo.Inventory = inventory;
                            productionLineData.ProductionLineMaterials.Add(plMaterialData);
                        }

                        return productionLineData;
                    },
                    p,
                    splitOn: "MaterialId, PartId, InventoryId",
                    commandType: CommandType.StoredProcedure
                );

                return productionLineDict.Values;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting production line with materials: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductionLine>> Select_productionLines_with_details(int lineId)
        {
            var sp = "sp_select_productionLine_details";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);

            var productionLineDict = new Dictionary<int, ProductionLine>();

            try
            {
                await _db.QueryAsync<ProductionLine, Group, Station, Machine, Part, Inventory, ProductionLine>(
                    sp,
                    (line, group, station, machine, part, inventory) =>
                    {
                        if (!productionLineDict.TryGetValue(line.LineId, out var productionLineData))
                        {
                            productionLineData = line;
                            productionLineData.Groups = [];
                            productionLineDict.Add(line.LineId, productionLineData);
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

        public async Task<IEnumerable<ProductionLine>> Select_productionLine_recipe(int taskId, Guid userId)
        {
            var sp_lineId = "sp_select_lineId_from_recipe";
            var p1 = new DynamicParameters();
            p1.Add("@TaskId", taskId);
            p1.Add("@UserId", userId);

            var lineId = await _db.QueryFirstAsync<int>(
                sp_lineId,
                p1,
                commandType: CommandType.StoredProcedure
            );

            return await Select_productionLines_with_details(lineId);
        }

        public async Task<IEnumerable<ProductionLineMachineHoursPerCut>> Select_productionLines_with_machineHoursPerCut(int lineId)
        {
            var sp = "sp_select_productionLines_with_machineHoursPerCut";
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

        public async Task<string> Update_productionLine(int lineId, ProductionLineDto plDto)
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

        public async Task<MessageResponse> Create_productionLine_steps(ProductionLineConfigDto plConfigDto)
        {
            var sp = "sp_create_update_productionLine_steps";
            var p = new DynamicParameters();
            p.Add("@LineId", plConfigDto.LineId);
            p.Add("@GroupId", plConfigDto.Steps.GroupId);
            p.Add("@StationId", plConfigDto.Steps.StationId);
            p.Add("@MachineId", plConfigDto.Steps.MachineId);
            p.Add("@PartId", plConfigDto.Steps.PartId);
            p.Add("@StepOrder", plConfigDto.Steps.StepOrder);

            var materialsTable = new DataTable();
            materialsTable.Columns.Add("PreviousPartId", typeof(int));
            materialsTable.Columns.Add("RequiredQuantity", typeof(decimal));
            materialsTable.Columns.Add("MaterialOrder", typeof(int));

            foreach (var material in plConfigDto.Steps.Materials)
            {
                materialsTable.Rows.Add(
                    material.PreviousPartId,
                    material.RequiredQuantity,
                    material.MaterialOrder
                );
            }

            p.Add("@MaterialsTable", materialsTable.AsTableValuedParameter("dbo.Manufacturing_ProductionLineRecipeMaterialsTableType"));

            try
            {
                return await _db.QueryFirstAsync<MessageResponse>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        /*public async Task<string> Update_productionLine_steps(ProductionLineHandleStepDto plUpdateDto)
        {
            var sp = "sp_create_update_productionLine_steps";
            var p = new DynamicParameters();
            p.Add("@RecipeId", plUpdateDto.RecipeId);
            p.Add("@LineId", plUpdateDto.LineId);
            p.Add("@GroupId", plUpdateDto.Steps.GroupId);
            p.Add("@StationId", plUpdateDto.Steps.StationId);
            p.Add("@MachineId", plUpdateDto.Steps.MachineId);
            p.Add("@PartId", plUpdateDto.Steps.PartId);
            // p.Add("@PrevPartId", plUpdateDto.Steps.PrevPartId);
            // p.Add("@Quantity", plUpdateDto.Steps.RequiredQuantity);
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
        }*/

        public async Task<MessageResponse> Update_productionLine_order(bool steps, List<OrderStepDto> orderStepsDto)
        {
            var sp = "sp_update_stepOrder";
            var p = new DynamicParameters();
            p.Add("@ContentType", steps ? 1 : 2);

            var orderStepTable = new DataTable();
            orderStepTable.Columns.Add("Id", typeof(int));
            orderStepTable.Columns.Add("Step", typeof(int));

            foreach (var orderStep in orderStepsDto)
            {
                orderStepTable.Rows.Add(
                    orderStep.Id,
                    orderStep.Step
                );
            }

            p.Add("@OrderStepsTable", orderStepTable.AsTableValuedParameter("dbo.Manufacturing_OrderStepTableType"));

            try
            {

                return await _db.QueryFirstAsync<MessageResponse>(
                        sp,
                        p,
                        commandType: CommandType.StoredProcedure
                    );

            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    Status = "Error",
                    Message = ex.Message,
                };
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
            // p.Add("@PrevPartId", plDeleteDto.Steps.PrevPartId);
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
