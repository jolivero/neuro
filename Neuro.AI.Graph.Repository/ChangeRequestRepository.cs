using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class ChangeRequestRepository
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _config;

        public ChangeRequestRepository(ManufacturingConnector manufacturingConnector, IConfiguration config)
        {
            _db = manufacturingConnector.Connect();
            _config = config;
        }

        #region Queries

        public async Task<IEnumerable<OptionsResponse>> Select_specialMissions_options()
        {
            var sp = "sp_select_specialMissions_options";

            try
            {
                return await _db.QueryAsync<OptionsResponse>(
                    sp,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Data.ToString());
            }
        }

        public async Task<int> Select_requestId(int monthId, string requestType)
        {
            var sp = "sp_select_requestId";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@RequestType", requestType);
            p.Add("@RequestId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryFirstOrDefaultAsync<string>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<int>("@RequestId");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Mutations

        public async Task<string> Create_change_request(ChangeRequestDto crDto)
        {
            var sp = "sp_create_changeRequest";
            var p = new DynamicParameters();
            p.Add("@TaskId", crDto.TaskId ?? null);
            p.Add("@NcPartId", crDto.NcPartId ?? null);
            p.Add("@UserId", crDto.UserId);
            p.Add("@CreatedBy", crDto.CreatedBy);
            p.Add("@CategoryId", crDto.CategoryId);
            p.Add("@OriginRequest", crDto.OriginRequest);
            p.Add("@RequestType", crDto.RequestType);
            p.Add("@Reason", crDto.Reason ?? null);
            p.Add("@CurrentValue", crDto.CurrentValue);
            p.Add("@NewValue", crDto.NewValue);
            p.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

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


        public async Task<string> Create_monthly_request(MonthlyChangeRequestDto mRequestDto)
        {
            var sp = "sp_create_monthly_request";
            var p = new DynamicParameters();
            p.Add("@MonthId", mRequestDto.MonthId);
            p.Add("@RequestingUserId", mRequestDto.RequestingUserId);
            p.Add("@RequestType", mRequestDto.RequestType);
            p.Add("@Reason", mRequestDto.Reason);
            p.Add("@CurrentValue", mRequestDto.CurrentValue);
            p.Add("@NewValue", mRequestDto.NewValue);
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

        public async Task<string> Create_daily_request(DailyChangeRequestDto dRequestDto)
        {
            var sp = "sp_create_monthly_request";
            var p = new DynamicParameters();
            p.Add("@RequestingUserId", dRequestDto.RequestingUserId);
            p.Add("@Reason", dRequestDto.Reason);
            p.Add("@RequestType", dRequestDto.RequestType);
            p.Add("@CurrentValue", dRequestDto.CurrentValue);
            p.Add("@NewValue", dRequestDto.NewValue);
            p.Add("@CurrentUserId", dRequestDto.CurrentUserId);
            p.Add("@NewUserId", dRequestDto.NewUserId ?? null);
            p.Add("@CurrentStationId", dRequestDto.CurrentStationId);
            p.Add("@StationId", dRequestDto.StationId ?? null);
            p.Add("@CurrentMachineId", dRequestDto.CurrentMachineId);
            p.Add("@MachineId", dRequestDto.MachineId ?? null);
            p.Add("@CurrentTunId", dRequestDto.CurrentTurnId ?? null);
            p.Add("@NewTurnId", dRequestDto.NewTurnId ?? null);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var day in dRequestDto.DayId)
                {
                    p.Add("@DayId", day);

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
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> Create_changePlannification_request(ChangePlanificationRequestDto cpRequestDto)
        {
            var sp = "sp_create_changePlannification_request";
            var p = new DynamicParameters();
            p.Add("@TaskId", cpRequestDto.TaskId);
            p.Add("@RequestingUserId", cpRequestDto.RequestingUserId);
            p.Add("@RequestType", cpRequestDto.RequestType);
            p.Add("@LineId", cpRequestDto.LineId);
            p.Add("@GroupId", cpRequestDto.GroupId);
            p.Add("@StationId", cpRequestDto.StationId);
            p.Add("@MachineId", cpRequestDto.MachineId);
            p.Add("@PartId", cpRequestDto.PartId);
            p.Add("@BeginAt", TimeSpan.Parse(cpRequestDto.BeginAt));
            p.Add("@EndAt", TimeSpan.Parse(cpRequestDto.EndAt));
            p.Add("@NewValue", cpRequestDto.NewValue);
            p.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

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

        public async Task<string> Create_changeOperator_request(CommonRequestDto cRequestDto)
        {
            var changeRequestDto = new ChangeRequestDto()
            {
                TaskId = cRequestDto.TaskId,
                UserId = cRequestDto.UserId,
                CreatedBy = cRequestDto.RequestingUserId,
                CategoryId = 3, //_config["RequestCategories:ChangeOperator"]! -> Modificar,
                OriginRequest = "Planificacion diaria",
                RequestType = cRequestDto.RequestType,
                Reason = cRequestDto.Reason
            };

            try
            {
                return await Create_change_request(changeRequestDto);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Create_specialMission_request(CommonRequestDto cRequestDto)
        {

            var changeRequestDto = new ChangeRequestDto()
            {
                TaskId = cRequestDto.TaskId,
                UserId = cRequestDto.UserId,
                CreatedBy = cRequestDto.RequestingUserId,
                CategoryId = 5, ////_config["RequestCategories:ChangeOperator"]! -> Modificar,
                OriginRequest = "Control de estado",
                RequestType = cRequestDto.RequestType,
                Reason = cRequestDto.Reason
            };

            /*var sp = "sp_create_specialMission_request";
            var p = new DynamicParameters();
            p.Add("@TaskId", smRequestDto.TaskId);
            p.Add("@UserId", smRequestDto.UserId);
            p.Add("@RequestingUserId", smRequestDto.RequestingUserId);
            p.Add("@RequestType", smRequestDto.RequestType);
            p.Add("@Reason", smRequestDto.Reason);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);*/

            try
            {
                return await Create_change_request(changeRequestDto);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Create_extraTime_request(string currentDate, ExtraTimeRequestDto etRequestDto)
        {
            var sp = "sp_create_extraTime_request";
            var p = new DynamicParameters();
            p.Add("@RequestId", Guid.NewGuid());
            p.Add("@CurrentDate", currentDate);
            p.Add("@LineId", etRequestDto.LineId);
            p.Add("@GroupId", etRequestDto.GroupId);
            p.Add("@StationId", etRequestDto.StationId);
            p.Add("@MachineId", etRequestDto.MachineId);
            p.Add("@PartId", etRequestDto.PartId);
            p.Add("@RequestingUserId", etRequestDto.RequestingUserId);
            p.Add("@RequestType", etRequestDto.RequestType);
            p.Add("@HoursQuantity", etRequestDto.HoursQuantity);
            p.Add("@Reason", etRequestDto.Reason);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var userId in etRequestDto.UserIds)
                {
                    p.Add("@UserId", userId);
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
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Update_status_request(UpdateStatusRequestDto usRequestDto)
        {
            var sp = "sp_update_request";
            var p = new DynamicParameters();
            p.Add("@RequestId", usRequestDto.RequestId);
            p.Add("@ApprovalUserId", usRequestDto.ApprovalUserId);
            p.Add("@CategoryId", usRequestDto.CategoryId);
            p.Add("@TaskId", usRequestDto.TaskId ?? null);
            p.Add("@Response", usRequestDto.Response);
            p.Add("@Status", usRequestDto.Status ? "A" : "R");
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