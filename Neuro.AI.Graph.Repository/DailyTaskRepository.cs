using System.Data;
using System.Security.Cryptography;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class DailyTaskRepository
    {
        private readonly IDbConnection _db;
        private readonly ChangeRequestRepository _changeRequestRepository;

        public DailyTaskRepository(ManufacturingConnector manufacturingConnector, ChangeRequestRepository changeRequestRepository)
        {
            _db = manufacturingConnector.Connect();
            _changeRequestRepository = changeRequestRepository;
        }

        #region Queries

        public async Task<IEnumerable<DailyTaskOperator>> Select_dailyTask_by_userId(string currentDay, string userId, string? taskId = null)
        {
            var sp_tasks = "sp_select_dailyTask_by_userId";
            var sp_progress = "sp_select_operator_progress";
            var p = new DynamicParameters();
            p.Add("@CurrentDay", currentDay);
            p.Add("@TaskId", taskId);
            p.Add("@UserId", userId);

            var dailyPlanningDict = new Dictionary<string, DailyTaskOperator>();

            try
            {
                await _db.QueryAsync<DailyPlanning, DailyTask, User, Station, Part, Machine, Part, DailyPlanning>(
                    sp_tasks,
                    (dailyPlanning, dailyTask, user, station, part, machine, prevPart) =>
                    {
                        if (!dailyPlanningDict.TryGetValue(dailyTask.TaskId.ToString(), out var dailyPlanningData))
                        {
                            dailyPlanningData = new()
                            {
                                MonthId = dailyPlanning.MonthId,
                                DayId = dailyPlanning.DayId,
                                ProductionDate = dailyPlanning.ProductionDate,
                                DailyGoal = dailyPlanning.DailyGoal,
                                TaskId = dailyTask.TaskId,
                                OperatorStatus = dailyTask.OperatorStatus!,
                                User = user,
                                Station = new()
                                {
                                    StationId = station.StationId,
                                    Name = station.Name!,
                                    Machine = machine,
                                    Part = part,
                                    PrevPart = [],
                                },
                            };
                            dailyPlanningDict.Add(dailyTask.TaskId.ToString(), dailyPlanningData);
                        }

                        if (part != null && dailyPlanningData.Station.Part.PartId == part.PartId) dailyPlanningData.Station.Part = part;
                        if (prevPart != null && dailyPlanningData.Station.StationId == prevPart.StationId) dailyPlanningData.Station.PrevPart.Add(prevPart);

                        return dailyPlanning;
                    },
                    p,
                    splitOn: "TaskId, UserId, StationId, PartId, MachineId, PartId",
                    commandType: CommandType.StoredProcedure
                );

                foreach (var dailyPlanning in dailyPlanningDict)
                {
                    var compliance = await _db.QueryFirstAsync<Compliance>(
                       sp_progress,
                       new
                       {
                           dailyPlanning.Value.TaskId,
                           UserId = userId
                       },
                       commandType: CommandType.StoredProcedure
                   );

                    dailyPlanning.Value.Compliance = compliance;
                }

                return dailyPlanningDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DailyTask>> Select_dailyTask_operator_history(string taskId, string userId)
        {
            var sp = "sp_select_dailyTask_operator_history";
            var p = new DynamicParameters();
            p.Add("@TaskId", taskId);
            p.Add("@UserId", userId);

            var dailyTaskDict = new Dictionary<string, DailyTask>();

            try
            {
                await _db.QueryAsync<DailyTask, DailyPlanning, User, ProductionRecord, DailyTask>(
                    sp,
                    (dt, dp, u, pr) =>
                    {
                        if (!dailyTaskDict.TryGetValue(dt.TaskId.ToString(), out var dailyTaskdata))
                        {
                            dailyTaskdata = dt;
                            dailyTaskdata.Day = dp;
                            dailyTaskdata.User = u;
                            dailyTaskdata.ProductionRecords = [];

                            dailyTaskDict.Add(dt.TaskId.ToString(), dailyTaskdata);
                        }

                        var prData = dailyTaskdata.ProductionRecords.FirstOrDefault(p => p.ProductionId == pr.ProductionId);
                        if (prData == null)
                        {
                            prData = pr;
                            dailyTaskdata.ProductionRecords.Add(prData);
                        }

                        return dailyTaskdata;
                    },
                    p,
                    splitOn: "DayId, UserId, ProductionId",
                    commandType: CommandType.StoredProcedure
                );

                return dailyTaskDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        #endregion

        #region Mutations

        public async Task<IEnumerable<ExtraTimeResponse>> Select_extraTime_operator(CheckOperatorExtraTimeDto operatorExtraTimeDto)
        {
            var sp = "sp_select_operator_extraTime";
            var p = new DynamicParameters();
            p.Add("@UserId", operatorExtraTimeDto.UserId);
            p.Add("@ProductiveDate", operatorExtraTimeDto.ProductiveDate);
            p.Add("@TaskId", operatorExtraTimeDto.TaskId ?? null);
            p.Add("@TurnId", operatorExtraTimeDto.TurnId ?? null);
            p.Add("@BeginAt", TimeSpan.Parse(operatorExtraTimeDto.BeginAt));
            p.Add("@EndAt", TimeSpan.Parse(operatorExtraTimeDto.EndAt));

            try
            {

                return await _db.QueryAsync<ExtraTimeResponse>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Create_dailyTask(DailyTaskDto dtDto)
        {
            var sp = "sp_create_dailyTasks";
            var p = new DynamicParameters();
            p.Add("@MonthId", dtDto.MonthId);
            p.Add("@TurnId", dtDto.TurnId ?? null);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var assigment in dtDto.Assigments)
                {
                    p.Add("@DayId", assigment.DayId);
                    p.Add("@BeginAt", TimeSpan.Parse(assigment.BeginAt));
                    p.Add("@EndAt", TimeSpan.Parse(assigment.EndAt));
                    p.Add("@UserId", assigment.UserId);
                    p.Add("@StationId", assigment.StationId);
                    p.Add("@MachineId", assigment.MachineId);

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

        public async Task<string> Update_dailyTask(DailyTaskDto dtDto)
        {
            var sp = "sp_Update_dailyTask";
            var p = new DynamicParameters();
            p.Add("@MonthId", dtDto.MonthId);
            p.Add("@TurnId", dtDto.TurnId ?? null);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var assigment in dtDto.Assigments)
                {
                    p.Add("@DayId", assigment.DayId);
                    p.Add("@BeginAt", TimeSpan.Parse(assigment.BeginAt));
                    p.Add("@EndAt", TimeSpan.Parse(assigment.EndAt));
                    p.Add("@UserId", assigment.UserId);
                    p.Add("@RemoveUserId", assigment.RemoveUserId ?? null);
                    p.Add("@StationId", assigment.StationId);
                    p.Add("@MachineId", assigment.MachineId);

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

        public async Task Revert_dailyTask_Plannification(string requestId)
        {
            var sp = "sp_update_revertPlannificationChange";
            var p = new DynamicParameters();
            p.Add("@RequestId", requestId);

            try
            {
                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}