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

        public DailyTaskRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
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

            var dailyScheduleDict = new Dictionary<string, DailyTaskOperator>();

            try
            {
                await _db.QueryAsync<DailySchedule, DailyTask, User, Station, Part, Machine, Part, DailySchedule>(
                    sp_tasks,
                    (dailySchedule, dailyTask, user, station, part, machine, prevPart) =>
                    {
                        if (!dailyScheduleDict.TryGetValue(dailyTask.TaskId.ToString(), out var dailyScheduleData))
                        {
                            dailyScheduleData = new()
                            {
                                MonthId = dailySchedule.MonthId,
                                DayId = dailySchedule.DayId,
                                ProductionDate = dailySchedule.ProductionDate,
                                DailyGoal = dailySchedule.DailyGoal,
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
                            dailyScheduleDict.Add(dailyTask.TaskId.ToString(), dailyScheduleData);
                        }

                        if (part != null && dailyScheduleData.Station.Part.PartId == part.PartId) dailyScheduleData.Station.Part = part;
                        if (prevPart != null && dailyScheduleData.Station.StationId == prevPart.StationId) dailyScheduleData.Station.PrevPart.Add(prevPart);

                        return dailySchedule;
                    },
                    p,
                    splitOn: "TaskId, UserId, StationId, PartId, MachineId, PartId",
                    commandType: CommandType.StoredProcedure
                );

                foreach (var dailySchedule in dailyScheduleDict)
                {
                    var compliance = await _db.QueryFirstAsync<Compliance>(
                       sp_progress,
                       new
                       {
                           dailySchedule.Value.TaskId,
                           UserId = userId
                       },
                       commandType: CommandType.StoredProcedure
                   );

                    dailySchedule.Value.Compliance = compliance;
                }

                return dailyScheduleDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Mutations

        public async Task<string> Create_dailyTask(DailyTaskDto dtDto)
        {
            var sp = "sp_create_dailyTasks";
            var p = new DynamicParameters();
            p.Add("@MonthId", dtDto.MonthId);
            p.Add("@TurnId", dtDto.TurnId);
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

        #endregion
    }
}