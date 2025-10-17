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

        public async Task<AnnualPlannigInfo> Select_annual_planning_info(int year, int? month)
        {
            var sp = "sp_select_annual_planning_info";
            var p = new DynamicParameters();
            p.Add("@Year", year);
            p.Add("@Month", month ?? null);

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

        public async Task<IEnumerable<MonthlyPlanningProductionLines>> Select_annual_planning(int? year, int? month, int? companyId)
        {
            var sp = "sp_select_planningByYear";
            var p = new DynamicParameters();
            p.Add("@Year", year ?? DateTime.Now.Year);
            p.Add("@Month", month ?? null);
            p.Add("@CompanyId", companyId ?? null);

            var planningDict = new Dictionary<int, MonthlyPlanningProductionLines>();

            try
            {
                await _db.QueryAsync<MonthlyPlanning, AssignedProductionLines, DailyPlanning, DailyTask, MonthlyPlanning>(
                    sp,
                    (mp, apl, dp, dt) =>
                    {
                        if (!planningDict.TryGetValue(mp.Month, out var planningData))
                        {
                            planningData = new()
                            {
                                MonthId = mp.MonthId,
                                Month = mp.Month,
                                Year = mp.Year,
                                BusinessDays = mp.BusinessDays,
                                ExtraDays = mp.ExtraDays,
                                LineId = mp.LineId,
                                //DailyPlannings = [],
                                AssignedProductionLines = []
                            };

                            planningDict.Add(mp.Month, planningData);
                        }

                        var dpData = planningData.DailyPlannings.FirstOrDefault(d => d.DayId == dp.DayId);
                        if (dpData == null)
                        {
                            dpData = dp;
                            dpData.DailyTasks = [];
                            planningData.DailyPlannings.Add(dpData);
                        }

                        var dtData = dpData.DailyTasks.FirstOrDefault(t => t.TaskId == dt.TaskId);
                        if(dtData == null)
                        {
                            dtData = dt;
                            dpData.DailyTasks.Add(dtData);
                        }

                        var plData = planningData.AssignedProductionLines.FirstOrDefault(mp => mp.LineId == apl.LineId);
                        if (plData == null)
                        {
                            plData = new()
                            {
                                LineId = apl.LineId,
                                Name = apl.Name,
                                MonthlyGoal = mp.MonthlyGoal,
                                CurrentProgress = apl.CurrentProgress,
                                Progress = apl.Progress
                            };

                            planningData.AssignedProductionLines.Add(plData);
                        }

                        return planningData;
                    },
                    p,
                    splitOn: "LineId, DayId, TaskId",
                    commandType: CommandType.StoredProcedure
                );

                return planningDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.StackTrace);
            }

        }

        public async Task<IEnumerable<MonthlyPlanningProgress>> Select_monthlyPlanning_Progress(int lineId, string currentDay)
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

        public async Task<IEnumerable<MonthlyPlanning>> Select_station_with_machine_planning(int monthId, int stationId, int machineId)
        {
            var sp = "sp_select_station_machine_planning";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@StationId", stationId);
            p.Add("@MachineId", machineId);

            var stationMachinePlanificationDict = new Dictionary<int, MonthlyPlanning>();

            try
            {
                await _db.QueryAsync<MonthlyPlanning, DailyPlanning, DailyTask, User, MonthlyPlanning>(
                    sp,
                    (ms, ds, dt, u) =>
                    {
                        if (!stationMachinePlanificationDict.TryGetValue(ms.MonthId, out var stationMachinePlanificationData))
                        {
                            stationMachinePlanificationData = ms;
                            stationMachinePlanificationData.DailyPlannings = [];
                            stationMachinePlanificationDict.Add(ms.MonthId, stationMachinePlanificationData);
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

        public async Task<IEnumerable<MonthlyPlanningStepStatus>> Select_planning_step_status(int monthId, int lineId)
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

        public async Task<string> Create_monthly_schedule(MonthlyPlanningDto pmDto)
        {
            var sp = "sp_create_monthly_planning";
            var p = new DynamicParameters();
            p.Add("@Month", pmDto.Month);
            p.Add("@Year", pmDto.Year);
            p.Add("@MonthlyGoal", pmDto.MonthlyGoal);
            p.Add("@BusinessDays", pmDto.BusinessDays);
            p.Add("@ExtraDays", pmDto.ExtraDays);
            p.Add("@LineId", pmDto.LineId);
            p.Add("@PlannedBy", pmDto.PlannedBy);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            var monthlyDaysPlanningTable = new DataTable();
            monthlyDaysPlanningTable.Columns.Add("DailyGoal", typeof(int));
            monthlyDaysPlanningTable.Columns.Add("ProductionDate", typeof(string));
            monthlyDaysPlanningTable.Columns.Add("DayType", typeof(string));

            foreach (var dailyPlanning in pmDto.DailyPlanning)
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
            if (requestId == 0) return "No request";

            var sp = "sp_update_monthlyGoal";
            var p = new DynamicParameters();
            p.Add("@RequestId", requestId);
            p.Add("@MonthId", mgDto.MonthId);
            p.Add("@LineId", mgDto.LineId);
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
            if (requestId == 0) return "No request";

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

        public async Task<string> Update_monthlyPlanning_operator(int monthId, int dayId, int userId)
        {
            var sp = "sp_update_monthlyPlanning_operator";
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

        public async Task<string> Delete_monthlyDays_planning(int monthId)
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

        public async Task<string> Delete_monthlyPlanning_operator(int monthId, int dayId, int userId)
        {
            var sp = "sp_update_monthlyPlanning_operator";
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