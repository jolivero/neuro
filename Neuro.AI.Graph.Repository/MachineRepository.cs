using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
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

        #region Queries

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
                if (!machineDict.TryGetValue(machine.MachineId.ToString(), out var machineData))
                {
                    machineData = machine;
                    machineData.MachineReports = new List<MachineReport>();
                    machineDict.Add(machine.MachineId.ToString(), machineData);
                }

                if (machineReport != null) machineData.MachineReports.Add(machineReport);

                return machineData;
            }, splitOn: "ReportId");

            return machineDict.Values;
        }

        #endregion

        #region Mutations

        public async Task<string> Create_machines(MachineDto machineDto)
        {
            var p = new DynamicParameters();
            p.Add("@Name", machineDto.Name);
            p.Add("@Type", machineDto.Type);
            p.Add("@EnergyConsumption", machineDto.EnergyConsumption);
            p.Add("@MaintenancePeriod", machineDto.MaintenancePeriod);
            p.Add("@Velocity", machineDto.Velocity);
            p.Add("@MinOperator", machineDto.MinOperator);
            p.Add("@MaxOperator", machineDto.MaxOperator);
            p.Add("@HoursPerCut", machineDto.HoursPerCut);
            p.Add("@Status", machineDto.Status);
            p.Add("@CreatedBy", machineDto.CreatedBy);

            var query = @"INSERT INTO Machines
                (Name, Type, EnergyConsumption, MaintenancePeriod, Velocity, 
                MinOperator, MaxOperator, HoursPerCut, Status, CreatedBy) 
                VALUES (@Name, @Type, @EnergyConsumption, @MaintenancePeriod, 
                @Velocity, @MinOperator, @MaxOperator, @HoursPerCut, @Status, @CreatedBy);";

            await _db.ExecuteAsync(query, p);

            return "New machine added";
        }

        public async Task<string> Update_machines(string machineId, MachineDto machineDto)
        {
            if (await Select_machines(machineId) == null) return "Machine not found";

            var p = new DynamicParameters();
            p.Add("@Name", machineDto.Name);
            p.Add("@Type", machineDto.Type);
            p.Add("@EnergyConsumption", machineDto.EnergyConsumption);
            p.Add("@MaintenancePeriod", machineDto.MaintenancePeriod);
            p.Add("@Velocity", machineDto.Velocity);
            p.Add("@MinOperator", machineDto.MinOperator);
            p.Add("@MaxOperator", machineDto.MaxOperator);
            p.Add("@HoursPerCut", machineDto.HoursPerCut);
            p.Add("@Status", machineDto.Status);

            var query = @$"UPDATE Machines
                SET Name = @Name, Type = @Type, EnergyConsumption = @EnergyConsumption, MaintenancePeriod = @MaintenancePeriod, 
                Velocity = @Velocity, MinOperator = @MinOperator, MaxOperator = @MaxOperator, HoursPerCut = @HoursPerCut, 
                Status = @Status WHERE MachineId = '{machineId}';";

            await _db.ExecuteAsync(query, p);
            return $"Machine {machineId} updated";
        }

        #endregion

    }
}
