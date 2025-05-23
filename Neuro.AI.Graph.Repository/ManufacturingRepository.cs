using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class ManufacturingRepository
    {
        private readonly IDbConnection _db;
        public ManufacturingRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";
            return await _db.QueryAsync<Company>(query);
        }

        public async Task<IEnumerable<User>> GetUsersInfo()
        {
            var query = "SELECT * FROM Users AS u JOIN Companies AS c ON u.CompanyId = c.CompanyId";
            var result = await _db.QueryAsync<User, Company, User>(query, (user, company) =>
            {
                user.Company = company;
                return user;
            },
            splitOn: "CompanyId");

            return result;
        }

        public async Task<IEnumerable<ProductionLine>> GetProductionLineById(string lineId)
        {
            var query = $"SELECT * FROM ProductionLines WHERE LineId = '{lineId}'";
            return await _db.QueryAsync<ProductionLine>(query);
        }

        public async Task<IEnumerable<ProductionLine>> GetProductionLines()
        {
            var query = "SELECT * FROM ProductionLines AS pl JOIN Groups AS g ON g.LineId = pl.LineId JOIN Stations AS s ON s.GroupId = g.GroupId;";
            var productionLinesSpecs = await _db.QueryAsync<ProductionLine, Group, Station, ProductionLine>(query, (line, group, station) =>
            {
                line.Groups.Add(group);
                group.Stations.Add(station);
                return line;
            }, splitOn: "GroupId, StationId");

            return productionLinesSpecs;
        }

        public async Task<IEnumerable<ProductionLine>> GetProductionLineDetail(string lineId)
        {
            var query = $"SELECT * FROM ProductionLines AS pl " +
                $"JOIN Groups AS g ON g.LineId = pl.LineId " +
                $"JOIN Stations AS s ON s.GroupId = g.GroupId " +
                $"JOIN Machines m ON m.StationId = s.StationId " +
                $"JOIN Parts p ON p.StationId = s.StationId " +
                $"WHERE pl.LineId = '{lineId}';";

            var productionLinesSpecs = await _db.QueryAsync<ProductionLine, Group, Station, Machine, Part, ProductionLine>(query, (line, group, station, machine, part) =>
            {
                line.Groups.Add(group);
                group.Stations.Add(station);
                station.Machines.Add(machine);
                station.Parts.Add(part);
                return line;
            }, splitOn: "GroupId, StationId, MachineId, PartId");

            return productionLinesSpecs;
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            var query = "SELECT * FROM Groups";
            return await _db.QueryAsync<Group>(query);
        }

        public async Task<IEnumerable<Station>> Getstation()
        {
            var query = "SELECT * FROM Stations";
            return await _db.QueryAsync<Station>(query);
        }

        public async Task<IEnumerable<Machine>> GetMachines()
        {
            var query = "SELECT * FROM Machines";
            var machines = await _db.QueryAsync<Machine>(query);
            return machines;
        }

        public async Task<IEnumerable<Machine>> GetMachineById(string machineId) 
        {
            var query = $"SELECT * FROM Machines WHERE MachineId = '{machineId}'";
            return await _db.QueryAsync<Machine>(query);
        }

        public async Task<IEnumerable<Machine>> GetMachineWithReportsByMachineId(string machineId)
        {
            var query = $"SELECT * FROM Machines AS m " +
                $"JOIN MachineReports mr ON mr.MachineId = m.MachineId " +
                $"WHERE m.MachineId = '{machineId}'";

            var response = await _db.QueryAsync<Machine, MachineReport, Machine>(query, (machine, machineReport) =>
            {
                machineReport.MachineId = machine.MachineId;
                machine.MachineReports.Add(machineReport);
                return machine;
            }, splitOn: "ReportId");

            return response;
        }

        public async Task<IEnumerable<Part>> GetParts()
        {
            var query = "SELECT * FROM Parts";
            return await _db.QueryAsync<Part>(query);
        }
    }
}
