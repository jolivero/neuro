using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
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

        public async Task<MessageResponse> Create_machines(MachineDto machineDto)
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
                return new MessageResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public async Task<MessageResponse> Update_machines(int machineId, MachineDto machineDto)
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
                return new MessageResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public async Task<MessageResponse> Create_machine_report(MachineReportDto machineReportDto)
        {
            var sp = "sp_create_update_machine_report";
            var p = new DynamicParameters();
            p.Add("@StationId", machineReportDto.StationId);
            p.Add("@MachineId", machineReportDto.MachineId);
            p.Add("@OperatorId", machineReportDto.OperatorId);
            p.Add("@Desc0", machineReportDto.Desc0);

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

        public async Task<MessageResponse> Update_machine_report(int reportId, MachineReportDto machineReportDto)
        {
            var sp = "sp_create_update_machine_report";
            var p = new DynamicParameters();
            p.Add("@ReportId", reportId);
            p.Add("@Type", machineReportDto.Type);
            p.Add("@Desc1", machineReportDto.Desc1);
            p.Add("@Status", machineReportDto.Status);
            p.Add("@TechnicalId", machineReportDto.TechnicalId);

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

        #endregion

    }
}
