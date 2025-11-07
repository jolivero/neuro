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

        public async Task<MessageResponse> Create_change_requestV2(ChangeRequestDtoV2 crDto)
        {
            var sp = "sp_create_changeRequestV2";
            var p = new DynamicParameters();
            p.Add("@MonthId", crDto.MonthId);
            p.Add("@DayId", crDto.DayId);
            p.Add("@TaskId", crDto.TaskId);
            p.Add("@LineId", crDto.LineId);
            p.Add("@GroupId", crDto.GroupId);
            p.Add("@StationId", crDto.StationId);
            p.Add("@MachineId", crDto.MachineId);
            p.Add("@PartId", crDto.PartId);
            p.Add("@TurnId", crDto.TurnId);
            p.Add("@CurrentUserId", crDto.CurrentUserId);
            p.Add("@NewUserId", crDto.NewUserId ?? null);
            p.Add("@CreatedBy", crDto.RequestingUserId);
            p.Add("@CurrentValue", crDto.CurrentValue);
            p.Add("@NewValue", crDto.NewValue);
            p.Add("@CategoryId", crDto.CategoryId);
            p.Add("@OriginRequest", crDto.OriginRequest);
            p.Add("@RequestType", crDto.RequestType);
            p.Add("@Reason", crDto.Reason);

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

        public async Task<MessageResponse> Create_changeMonthlyPlanning_request(ChangeMonthlyPlanningRequestDto mpRequestDto)
        {
            var sp = "sp_create_changeDailyPlanning_request";
            var p = new DynamicParameters();
            p.Add("@MonthId", mpRequestDto.MonthId);
            p.Add("@LineId", mpRequestDto.LineId);
            p.Add("@RequestingUserId", mpRequestDto.RequestingUserId);
            p.Add("@RequestType", mpRequestDto.UpdateDailyPlanningDto == null ? "Ajuste de meta" : "Ajuste de días");
            p.Add("@Reason", mpRequestDto.Reason);
            p.Add("@CurrentValue", mpRequestDto.CurrentValue);
            p.Add("@NewValue", mpRequestDto.NewValue);

            if (mpRequestDto.UpdateDailyPlanningDto != null)
            {
                var changePlanningDaysTable = new DataTable();
                changePlanningDaysTable.Columns.Add("DayId", typeof(int));
                changePlanningDaysTable.Columns.Add("DailyGoal", typeof(int));
                changePlanningDaysTable.Columns.Add("ProductionDate", typeof(DateOnly));
                changePlanningDaysTable.Columns.Add("Available", typeof(int));
                changePlanningDaysTable.Columns.Add("DayType", typeof(string));

                foreach (var day in mpRequestDto.UpdateDailyPlanningDto)
                {
                    changePlanningDaysTable.Rows.Add(
                        day.DayId,
                        day.DailyGoal,
                        DateOnly.Parse(day.ProductionDate),
                        day.Available,
                        day.DayType
                    );
                }

                p.Add("@ChangePlanningDaysTable", changePlanningDaysTable.AsTableValuedParameter("dbo.Manufacturing_ChangePlanningDaysTableType"));
            }

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

        public async Task<ChangePlanningRequestMessageResponse> Create_changePlanning_request(ChangePlanificationRequestDto cpRequestDto)
        {
            var sp = "sp_create_changePlanning_request";
            var p = new DynamicParameters();
            p.Add("@TaskId", cpRequestDto.TaskId);
            p.Add("@RequestingUserId", cpRequestDto.RequestingUserId);
            p.Add("@RequestType", cpRequestDto.RequestType);
            p.Add("@LineId", cpRequestDto.LineId);
            p.Add("@GroupId", cpRequestDto.GroupId);
            p.Add("@StationId", cpRequestDto.StationId);
            p.Add("@MachineId", cpRequestDto.MachineId);
            p.Add("@PartId", cpRequestDto.PartId);
            // p.Add("@BeginAt", TimeSpan.Parse(cpRequestDto.BeginAt));
            // p.Add("@EndAt", TimeSpan.Parse(cpRequestDto.EndAt));
            // p.Add("@NewValue", cpRequestDto.NewValue);

            try
            {
                return await _db.QueryFirstAsync<ChangePlanningRequestMessageResponse>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                return new ChangePlanningRequestMessageResponse
                {
                    Status = "Error",
                    Message = ex.Message,
                    RequestId = 0
                };
            }
        }

        public async Task<MessageResponse> Create_changeOperator_request(CommonChangeRequestDto cRequestDto)
        {
            var changeRequestDto = new ChangeRequestDtoV2()
            {
                MonthId = cRequestDto.MonthId,
                DayId = cRequestDto.DayId,
                TaskId = cRequestDto.TaskId,
                LineId = cRequestDto.LineId,
                GroupId = cRequestDto.GroupId,
                StationId = cRequestDto.StationId,
                MachineId = cRequestDto.MachineId,
                PartId = cRequestDto.PartId,
                TurnId = cRequestDto.TurnId ?? null,
                CurrentUserId = cRequestDto.CurrentUserId,
                NewUserId = cRequestDto.NewUserId ?? null,
                RequestingUserId = cRequestDto.RequestingUserId,
                CategoryId = Convert.ToInt32(_config["RequestCategories:ChangeOperator"]), //-> Modificar,
                OriginRequest = "Planificación diaria",
                RequestType = cRequestDto.RequestType,
                Reason = cRequestDto.Reason,
            };

            try
            {
                return await Create_change_requestV2(changeRequestDto);
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

        public async Task<string> Create_specialMission_request(CommonRequestDto cRequestDto)
        {

            var changeRequestDto = new ChangeRequestDto()
            {
                TaskId = cRequestDto.TaskId,
                UserId = cRequestDto.UserId,
                CreatedBy = cRequestDto.RequestingUserId,
                CategoryId = Convert.ToInt32(_config["RequestCategories:SpecialMissions"]),
                OriginRequest = "Control de estado",
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

        public async Task<MessageResponse> Create_extraTime_request(ExtraTimeRequestDto etRequestDto)
        {
            var sp = "sp_create_extraTime_request";
            var p = new DynamicParameters();
            p.Add("@MonthId", etRequestDto.MonthId);
            p.Add("@DayId", etRequestDto.DayId);
            p.Add("@TaskId", etRequestDto.TaskId);
            p.Add("@LineId", etRequestDto.LineId);
            p.Add("@GroupId", etRequestDto.GroupId);
            p.Add("@StationId", etRequestDto.StationId);
            p.Add("@MachineId", etRequestDto.MachineId);
            p.Add("@PartId", etRequestDto.PartId);
            p.Add("@CategoryId", Convert.ToInt32(_config["RequestCategories:ExtraTime"]));
            p.Add("@RequestingUserId", etRequestDto.RequestingUserId);
            p.Add("@RequestType", etRequestDto.RequestType);
            p.Add("@CurrentValue", etRequestDto.CurrentValue);
            p.Add("@NewValue", etRequestDto.NewValue);
            p.Add("@HoursQuantity", TimeSpan.Parse(etRequestDto.HoursQuantity));
            p.Add("@Reason", etRequestDto.Reason);

            var userIdListTable = new DataTable();
            userIdListTable.Columns.Add("UserId", typeof(Guid));

            foreach (var userId in etRequestDto.UserIds)
            {
                userIdListTable.Rows.Add(
                    userId
                );
            }

            p.Add("@UserIdListTable", userIdListTable.AsTableValuedParameter("dbo.Manufacturing_UserIdListTableType"));

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

        public async Task<string> Update_changeRequest_processedAt(int requestId)
        {
            var sp = "sp_update_changeRequest_processed";
            var p = new DynamicParameters();
            p.Add("@RequestId", requestId);
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