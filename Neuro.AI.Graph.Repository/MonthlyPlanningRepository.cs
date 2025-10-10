using System.Data;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class MonthlyPlanningRepository
    {
        private readonly IDbConnection _db;
        private readonly ChangeRequestRepository _changeRequestRepository;

        public MonthlyPlanningRepository(ManufacturingConnector manufacturingConnector, ChangeRequestRepository changeRequestRepository)
        {
            _db = manufacturingConnector.Connect();
            _changeRequestRepository = changeRequestRepository;
        }

        #region Queries

        public async Task<AnnualPlannigInfo> Select_annual_plannification_info(int year)
        {
            var sp = "sp_select_annual_planning_info";
            var p = new DynamicParameters();
            p.Add("@Year", year);

            try
            {
                return await _db.QueryFirstAsync<AnnualPlannigInfo>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MonthlyPlanningProductionLines>> Select_annual_planification(int? year, int? month, string? companyId)
        {
            var sp = "sp_select_plannificationByYear";
            var p = new DynamicParameters();
            p.Add("@Year", year ?? DateTime.Now.Year);
            p.Add("@Month", month ?? null);
            p.Add("@CompanyId", companyId ?? null);

            var planificationDict = new Dictionary<int, MonthlyPlanningProductionLines>();

            try
            {
                await _db.QueryAsync<MonthlyPlanning, AssignedProductionLines, MonthlyPlanning>(
                    sp,
                    (ms, apl) =>
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

                        var plData = planificationData.AssignedProductionLines.FirstOrDefault(mp => mp.LineId == apl.LineId);
                        if (plData == null)
                        {
                            plData = new()
                            {
                                LineId = apl.LineId,
                                Name = apl.Name,
                                MonthlyGoal = ms.MonthlyGoal,
                                CurrentProgress = apl.CurrentProgress,
                                Progress = apl.Progress
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

        public async Task<IEnumerable<MonthlyPlanningProgress>> Select_monthlyPlanning_Progress(string lineId, string currentDay)
        {
            var sp = "sp_select_monthlyPlanning_progress";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@CurrentDay", currentDay);

            try
            {
                return await _db.QueryAsync<MonthlyPlanningProgress>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MonthlyPlanning>> Select_station_with_machine_planification(string monthId, string stationId, string machineId)
        {
            var sp = "sp_select_station_machine_planification";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@StationId", stationId);
            p.Add("@MachineId", machineId);

            var stationMachinePlanificationDict = new Dictionary<string, MonthlyPlanning>();

            try
            {
                await _db.QueryAsync<MonthlyPlanning, DailyPlanning, DailyTask, User, MonthlyPlanning>(
                    sp,
                    (ms, ds, dt, u) =>
                    {
                        if (!stationMachinePlanificationDict.TryGetValue(ms.MonthId.ToString(), out var stationMachinePlanificationData))
                        {
                            stationMachinePlanificationData = ms;
                            stationMachinePlanificationData.DailyPlannings = [];
                            stationMachinePlanificationDict.Add(ms.MonthId.ToString(), stationMachinePlanificationData);
                        }

                        var dsData = stationMachinePlanificationData.DailyPlannings.FirstOrDefault(d => d.DayId == ds.DayId);
                        if (dsData == null)
                        {
                            dsData = ds;
                            dsData.DailyTasks = [];
                            stationMachinePlanificationData.DailyPlannings.Add(dsData);
                        }

                        if (dt != null && u != null)
                        {
                            var dtData = dsData.DailyTasks.FirstOrDefault(d => d.TaskId == dt.TaskId);
                            if (dtData == null)
                            {
                                dtData = dt;
                                dtData.User = new();

                                dsData.DailyTasks.Add(dtData);
                            }

                            if (u != null && dtData.UserId == u.UserId) dtData.User = u;
                        }

                        return stationMachinePlanificationData;

                    },
                    p,
                    splitOn: "DayId, TaskId, UserId",
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

        public async Task<IEnumerable<MonthlyPlanningStepStatus>> Select_planning_step_status(string monthId, string lineId)
        {
            var sp = "sp_select_planning_status";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@LineId", lineId);

            try
            {
                return await _db.QueryAsync<MonthlyPlanningStepStatus>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Estatus de planificación: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Mutations

        public async Task<string> Create_monthly_schedule(MonthlyPlanningDto msDto)
        {
            var sp = "sp_create_monthly_planning";
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

            var monthlyDaysPlanningTable = new DataTable();
            monthlyDaysPlanningTable.Columns.Add("DailyGoal", typeof(int));
            monthlyDaysPlanningTable.Columns.Add("ProductionDate", typeof(string));
            monthlyDaysPlanningTable.Columns.Add("DayType", typeof(string));

            foreach (var dailyPlanning in msDto.DailyPlanning)
            {
                monthlyDaysPlanningTable.Rows.Add(
                    dailyPlanning.DailyGoal,
                    dailyPlanning.ProductionDate,
                    dailyPlanning.DayType
                );
            }

            p.Add("@MonthlyDaysPlanningTable", monthlyDaysPlanningTable.AsTableValuedParameter("dbo.Manufacturing_MonthlyDaysPlanningTableType"));

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

        public async Task<string> Update_monthlyGoal_planning(UpdateMonthlyPlanningDto mgDto)
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

        public async Task<string> Update_monthlyDays_schedule(UpdateMonthlyPlanningDto mdDto)
        {
            var requestId = await _changeRequestRepository.Select_requestId(mdDto.MonthId, "Ajuste de días");

            if (string.IsNullOrEmpty(requestId)) return "No hay solicitud de cambio disponible para el mes indicado";

            var businessDays = mdDto.UpdateDailyPlanningDto!.Count(d => d.DayType.Equals("laboral", StringComparison.CurrentCultureIgnoreCase) && d.Available == 1);
            var extraDays = mdDto.UpdateDailyPlanningDto!.Count(d => d.DayType.Equals("extra", StringComparison.CurrentCultureIgnoreCase) && d.Available == 1);

            var previousDays = mdDto.UpdateDailyPlanningDto!.FindAll(d => DateOnly.Parse(d.ProductionDate) < DateOnly.FromDateTime(DateTime.Now) && d.Available == 1);
            var currentGoal = previousDays.Sum(p => p.DailyGoal);

            var daysToUpdate = mdDto.UpdateDailyPlanningDto!.FindAll(d => DateOnly.Parse(d.ProductionDate) >= DateOnly.FromDateTime(DateTime.Now) && d.Available == 1);
            var daysToRemove = mdDto.UpdateDailyPlanningDto!.FindAll(d => d.Available == 0);

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
                foreach (var item in daysToUpdate.Concat(daysToRemove))
                {
                    p.Add("@DayId", item.DayId);
                    p.Add("@DailyGoal", item.Available == 1 ? dailyGoal : item.DailyGoal);
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

        public async Task<string> Update_monthlyPlanning_operator(string monthId, string dayId, string userId)
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

        public async Task<string> Delete_monthlyDays_planning(string monthId)
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

        public async Task<string> Delete_monthlyPlanning_operator(string monthId, string dayId, string userId)
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