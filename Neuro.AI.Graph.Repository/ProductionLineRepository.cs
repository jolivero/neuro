using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
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

        public async Task<IEnumerable<ProductionLine>> Select_productionLines_With_Details(string lineId)
        {
            var sp = "sp_select_productionLine_details";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);

            var productionLineDict = new Dictionary<string, ProductionLine>();

            await _db.QueryAsync<ProductionLine, Group, Station, Machine, Part, ProductionLine>(
                sp,
                (line, group, station, machine, part) =>
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
                    if (part != null && !stationData.Parts.Any(s => s.PartId == part.PartId)) stationData.Parts.Add(part);

                    return productionLineData;
                },
                p,
                splitOn: "GroupId, StationId, MachineId, PartId",
                commandType: CommandType.StoredProcedure
                );

            return productionLineDict.Values;
        }



        public async Task<IEnumerable<ProductionLineMachineHoursPerCut>> Select_productionLines_With_MachineHoursPerCut(string lineId)
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

            await _db.QueryAsync(
                sp,
                p,
                commandType: CommandType.StoredProcedure
            );

            return p.Get<string>("@Message");
        }

        public async Task<string> Update_productionLine(string lineId, ProductionLineDto plDto)
        {
            var sp = "sp_create_update_productionLine";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@Name", plDto.Name);
            p.Add("@CompanyId", plDto.CompanyId);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            await _db.QueryAsync(
                sp,
                p,
                commandType: CommandType.StoredProcedure
            );

            return p.Get<string>("@Message");
        }

        public async Task<string> Create_productionLine_steps(ProductionLineConfigDto plConfigDto)
        {
            var sp = "sp_create_productionLine_steps";
            var p = new DynamicParameters();
            p.Add("@LineId", plConfigDto.LineId);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            foreach (var step in plConfigDto.Steps)
            {
                p.Add("@GroupId", step.GroupId);
                p.Add("@StationId", step.StationId);
                p.Add("@MachineId", step.MachineId);
                p.Add("@PrevMachineId", step.PrevMachineId);
                p.Add("@PartId", step.PartId);
                p.Add("@PrevPartId", step.PrevPartId);
                p.Add("@Quantity", step.RequiredQuantity);
                p.Add("@Action", string.IsNullOrEmpty(step.PrevMachineId) ? "insertar" : "actualizar");

                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }

            return p.Get<string>("@Message");
        }
        #endregion
    }
}
