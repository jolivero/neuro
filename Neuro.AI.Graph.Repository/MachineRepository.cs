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

        #endregion

        #region Mutations

        public async Task<string> Create_machines(MachineDto machineDto)
        {
            var sp = "sp_create_update_machine";
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

        public async Task<string> Update_machines(string machineId, MachineDto machineDto)
        {
            var sp = "sp_create_update_machine";
            var p = new DynamicParameters();
            p.Add("@MachineId", machineId);
            p.Add("@Name", machineDto.Name);
            p.Add("@Type", machineDto.Type);
            p.Add("@EnergyConsumption", machineDto.EnergyConsumption);
            p.Add("@MaintenancePeriod", machineDto.MaintenancePeriod);
            p.Add("@Velocity", machineDto.Velocity);
            p.Add("@MinOperator", machineDto.MinOperator);
            p.Add("@MaxOperator", machineDto.MaxOperator);
            p.Add("@HoursPerCut", machineDto.HoursPerCut);
            p.Add("@Status", machineDto.Status);
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

        public async Task<string> Create_machine_report(MachineReportDto machineReportDto)
        {
            var sp = "sp_create_update_machine_report";
            var p = new DynamicParameters();
            p.Add("@StationId", machineReportDto.StationId);
            p.Add("@MachineId", machineReportDto.MachineId);
            p.Add("@OperatorId", machineReportDto.OperatorId);
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

        public async Task<string> Update_machine_report(string reportId, MachineReportDto machineReportDto)
        {
            var sp = "sp_create_update_machine_report";
            var p = new DynamicParameters();
            p.Add("@ReportId", reportId);
            p.Add("@Type", machineReportDto.Type);
            p.Add("@Description", machineReportDto.Description);
            p.Add("@Status", machineReportDto.Status);
            p.Add("@TechnicalId", machineReportDto.TechnicalId);
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
