using System.Data;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class MonthlyScheduleRepository
    {
        private readonly IDbConnection _db;
        private readonly ChangeRequestRepository _changeRequestRepository;

        public MonthlyScheduleRepository(ManufacturingConnector manufacturingConnector, ChangeRequestRepository changeRequestRepository)
        {
            _db = manufacturingConnector.Connect();
            _changeRequestRepository = changeRequestRepository;
        }

        #region Queries

        public async Task<IEnumerable<MonthlyScheduleProductionLines>> Select_annual_planification(int? year)
        {
            var sp = "sp_select_plannificationByYear";
            var p = new DynamicParameters();
            p.Add("@Year", year ?? DateTime.Now.Year);

            var planificationDict = new Dictionary<int, MonthlyScheduleProductionLines>();

            try
            {
                await _db.QueryAsync<MonthlySchedule, ProductionLine, MonthlySchedule>(
                    sp,
                    (ms, pl) =>
                    {
                        if (!planificationDict.TryGetValue(ms.Month, out var planificationData))
                        {
                            planificationData = new()
                            {
                                MonthId = ms.MonthId,
                                Month = ms.Month,
                                Year = ms.Year,
                                BusinessDays = ms.BusinessDays,
                                ExtraDays = ms.ExtraDays,
                                LineId = ms.LineId,
                                AssignedProductionLines = []
                            };

                            planificationDict.Add(ms.Month, planificationData);
                        }

                        var plData = planificationData.AssignedProductionLines.FirstOrDefault(mp => mp.LineId == pl.LineId);
                        if (plData == null)
                        {
                            plData = new()
                            {
                                LineId = pl.LineId,
                                Name = pl.Name
                            };

                            planificationData.AssignedProductionLines.Add(plData);
                        }

                        return planificationData;
                    },
                    p,
                    splitOn: "LineId",
                    commandType: CommandType.StoredProcedure
                );

                return planificationDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.StackTrace);
            }

        }

        public async Task<IEnumerable<OperatorSelectList>> Select_available_operators(string monthId, string beginAt, string endAt)
        {
            var sp = "sp_select_available_operators";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@BeginAt", beginAt);
            p.Add("@EndAt", endAt);

            try
            {
                return await _db.QueryAsync<OperatorSelectList>(
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

        public async Task<IEnumerable<MonthlySchedule>> Select_station_with_machine_planification(string monthId, string stationId, string machineId)
        {
            var sp = "sp_select_station_machine_planification";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@StationId", stationId);
            p.Add("@MachineId", machineId);

            var stationMachinePlanificationDict = new Dictionary<string, MonthlySchedule>();

            try
            {
                await _db.QueryAsync<MonthlySchedule, DailySchedule, DailyTask, User, Station, Machine, Turn, MonthlySchedule>(
                    sp,
                    (ms, ds, dt, u, s, m, t) =>
                    {
                        if (!stationMachinePlanificationDict.TryGetValue(ms.MonthId.ToString(), out var stationMachinePlanificationData))
                        {
                            stationMachinePlanificationData = ms;
                            stationMachinePlanificationData.DailySchedules = [];
                            stationMachinePlanificationDict.Add(ms.MonthId.ToString(), stationMachinePlanificationData);
                        }

                        var dsData = stationMachinePlanificationData.DailySchedules.FirstOrDefault(d => d.DayId == ds.DayId);
                        if (dsData == null)
                        {
                            dsData = ds;
                            dsData.DailyTasks = [];
                            stationMachinePlanificationData.DailySchedules.Add(dsData);
                        }

                        var dtData = dsData.DailyTasks.FirstOrDefault(d => d.TaskId == dt.TaskId);
                        if (dtData == null)
                        {
                            dtData = dt;
                            dtData.User = new();
                            dtData.Station = new();
                            dtData.Machine = new();
                            dtData.Turn = new();

                            dsData.DailyTasks.Add(dtData);
                        }

                        if (u != null && dtData.UserId == u.UserId) dtData.User = u;
                        if (s != null && dtData.StationId == s.StationId) dtData.Station = s;
                        if (m != null && dtData.MachineId == m.MachineId) dtData.Machine = m;
                        if (t != null && dtData.TurnId == t.TurnId) dtData.Turn = t;

                        return stationMachinePlanificationData;

                    },
                    p,
                    splitOn: "DayId, TaskId, UserId, StationId, MachineId, TurnId",
                    commandType: CommandType.StoredProcedure
                );

                return stationMachinePlanificationDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Data.ToString());
            }
        }

        #endregion

        #region Mutations

        public async Task<string> Create_monthly_schedule(MonthlyScheduleDto msDto)
        {
            var sp = "sp_create_monthly_schedule";
            var p = new DynamicParameters();
            p.Add("@MonthId", Guid.NewGuid().ToString());
            p.Add("@Month", msDto.Month);
            p.Add("@Year", msDto.Year);
            p.Add("@MonthlyGoal", msDto.MonthlyGoal);
            p.Add("@BusinessDays", msDto.BusinessDays);
            p.Add("@ExtraDays", msDto.ExtraDays);
            p.Add("@LineId", msDto.LineId);
            p.Add("@PlannedBy", msDto.PlannedBy);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var item in msDto.DailySchedule)
                {
                    p.Add("@DailyGoal", item.DailyGoal);
                    p.Add("@ProductionDate", item.ProductionDate);
                    p.Add("@DayType", item.DayType);

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
                return ex.Message;
            }
        }

        public async Task<string> Update_monthlyGoal_schedule(UpdateMonthlyScheduleDto mgDto)
        {
            var requestId = await _changeRequestRepository.Select_requestId(mgDto.MonthId, "Ajuste de meta");
            if (string.IsNullOrEmpty(requestId)) return "No hay solicitud de cambio disponible para el mes indicado";

            var sp = "sp_update_monthlyGoal";
            var p = new DynamicParameters();
            p.Add("@RequestId", requestId);
            p.Add("@MonthId", mgDto.MonthId);
            p.Add("@MonthlyGoal", mgDto.MonthlyGoal);
            p.Add("@Reason", mgDto.Reason);
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

        public async Task<string> Update_monthlyDays_schedule(UpdateMonthlyScheduleDto mdDto)
        {
            var requestId = await _changeRequestRepository.Select_requestId(mdDto.MonthId, "Ajuste de días");

            if (string.IsNullOrEmpty(requestId)) return "No hay solicitud de cambio disponible para el mes indicado";

            var businessDays = mdDto.UpdateDailyScheduleDto!.Count(d => d.DayType.Equals("laboral", StringComparison.CurrentCultureIgnoreCase) && d.Available == 1);
            var extraDays = mdDto.UpdateDailyScheduleDto!.Count(d => d.DayType.Equals("extra", StringComparison.CurrentCultureIgnoreCase) && d.Available == 1);

            var previousDays = mdDto.UpdateDailyScheduleDto!.FindAll(d => DateOnly.Parse(d.ProductionDate) < DateOnly.FromDateTime(DateTime.Now) && d.Available == 1);
            var currentGoal = previousDays.Sum(p => p.DailyGoal);

            var daysToUpdate = mdDto.UpdateDailyScheduleDto!.FindAll(d => DateOnly.Parse(d.ProductionDate) >= DateOnly.FromDateTime(DateTime.Now) && d.Available == 1);
            var currentAndNewdays = daysToUpdate.Count(d => DateOnly.Parse(d.ProductionDate) >= DateOnly.FromDateTime(DateTime.Now));
            var dailyGoal = (mdDto.MonthlyGoal - currentGoal) / currentAndNewdays;

            var sp = "sp_update_monthlyDays";
            var p = new DynamicParameters();
            p.Add("@RequestId", requestId);
            p.Add("@MonthId", mdDto.MonthId);
            p.Add("@BusinessDays", businessDays);
            p.Add("@ExtraDays", extraDays);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var item in daysToUpdate)
                {
                    p.Add("@DayId", item.DayId);
                    p.Add("@DailyGoal", dailyGoal);
                    p.Add("@ProductionDate", item.ProductionDate);
                    p.Add("@DayType", item.DayType);
                    p.Add("@Available", item.Available);
                    p.Add("@Reason", mdDto.Reason);

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
                return ex.Message;
            }
        }

        public async Task<string> Update_monthlySchedule_operator(string monthId, string dayId, string userId)
        {
            var sp = "sp_update_monthlySchedule_operator";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@DayId", dayId);
            p.Add("@UserId", userId);
            p.Add("@Action", "actualizar");
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

        public async Task<string> Delete_monthlyDays_schedule(string monthId)
        {
            var sp = "sp_delete_monthlyDays";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
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

        public async Task<string> Delete_monthlySchedule_operator(string monthId, string dayId, string userId)
        {
            var sp = "sp_update_monthlySchedule_operator";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@DayId", dayId);
            p.Add("@UserId", userId);
            p.Add("@Action", "remover");
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