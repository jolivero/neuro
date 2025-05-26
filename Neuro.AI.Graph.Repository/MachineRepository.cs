using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class MachineRepository
    {
        private readonly IDbConnection _db;

        public MachineRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        public async Task<IEnumerable<Machine>> Select_machines()
        {
            var query = "SELECT * FROM Machines";
            var machines = await _db.QueryAsync<Machine>(query);
            return machines;
        }

        public async Task<IEnumerable<Machine>> Select_machines(string machineId)
        {
            var query = $"SELECT * FROM Machines WHERE MachineId = '{machineId}'";
            return await _db.QueryAsync<Machine>(query);
        }

        public async Task<IEnumerable<Machine>> Select_machine_with_reports(string machineId)
        {
            var query = $"SELECT * FROM Machines AS m " +
                $"JOIN MachineReports mr ON mr.MachineId = m.MachineId " +
                $"WHERE m.MachineId = '{machineId}'";

            var machineDict = new Dictionary<string, Machine>();

            await _db.QueryAsync<Machine, MachineReport, Machine>(query, (machine, machineReport) =>
            {
                if(!machineDict.TryGetValue(machine.MachineId.ToString(), out var machineData))
                {
                    machineData = machine;
                    machineData.MachineReports = new List<MachineReport>();
                    machineDict.Add(machine.MachineId.ToString(), machineData);
                }

                if(machineReport != null) machineData.MachineReports.Add(machineReport);

                return machineData;
            }, splitOn: "ReportId");

            return machineDict.Values;
        }
    }
}
