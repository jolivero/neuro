using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Neuro.AI.Graph.Connectors;
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

        public async Task<IEnumerable<ProductionLine>> Select_productionLines()
        {
            var query = "SELECT * FROM ProductionLines";
            return await _db.QueryAsync<ProductionLine>(query);
        }

        public async Task<IEnumerable<ProductionLine>> Select_productionLines(string lineId)
        {
            var query = $"SELECT * FROM ProductionLines WHERE LineId = '{lineId}'";
            return await _db.QueryAsync<ProductionLine>(query);
        }

        public async Task<IEnumerable<ProductionLine>> Select_productionLines_With_Details(string lineId)
        {
            var query = $"SELECT * FROM ProductionLines AS pl " +
                $"JOIN Groups AS g ON g.LineId = pl.LineId " +
                $"JOIN Stations AS s ON s.GroupId = g.GroupId " +
                $"JOIN Machines m ON m.StationId = s.StationId " +
                $"JOIN Parts p ON p.StationId = s.StationId " +
                $"WHERE pl.LineId = '{lineId}';";

            var productionLineDict = new Dictionary<string, ProductionLine>();

            await _db.QueryAsync<ProductionLine, Group, Station, Machine, Part, ProductionLine>(query, (line, group, station, machine, part) =>
            {
                if (!productionLineDict.TryGetValue(line.LineId.ToString(), out var productionLineData))
                {
                    productionLineData = line;
                    productionLineData.Groups = [];
                    productionLineDict.Add(line.LineId.ToString(), productionLineData);
                }

                var groupData = productionLineData.Groups.FirstOrDefault(ln => ln.GroupId == group.GroupId);
                if(groupData == null)
                {
                    groupData = group;
                    groupData.Stations = new List<Station>();
                    productionLineData.Groups.Add(groupData);
                }

                var stationData = groupData.Stations.FirstOrDefault(g => g.StationId == station.StationId);
                if (stationData == null)
                {
                    stationData = station;
                    stationData.Machines = new List<Machine>();
                    stationData.Parts = new List<Part>();
                    groupData.Stations.Add(stationData);
                }

                if(machine != null && !stationData.Machines.Any(s => s.MachineId == machine.MachineId)) stationData.Machines.Add(machine);
                if(part != null && !stationData.Parts.Any(s => s.PartId == part.PartId)) stationData.Parts.Add(part);

                return productionLineData;
            }, splitOn: "GroupId, StationId, MachineId, PartId");

            return productionLineDict.Values;
        }
    }
}
