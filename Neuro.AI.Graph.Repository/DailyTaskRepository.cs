using System.Data;
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

        public async Task<IEnumerable<DailyTaskOperator>> Select_dailyTask_by_userId(string currentDay, string userId)
        {
            var sp = "sp_select_dailyTask_by_userId";
            var p = new DynamicParameters();
            p.Add("@CurrentDay", currentDay);
            p.Add("@UserId", userId);

            var dailyScheduleDict = new Dictionary<string, DailyTaskOperator>();

            try
            {
                await _db.QueryAsync<DailySchedule, DailyTask, User, Station, Part, Machine, Part, DailySchedule>(
                    sp,
                    (dailySchedule, dailyTask, user, station, part, machine, prevPart) =>
                    {
                        if (!dailyScheduleDict.TryGetValue(dailySchedule.DayId.ToString(), out var dailyScheduleData))
                        {
                            dailyScheduleData = new()
                            {
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
                            dailyScheduleDict.Add(dailySchedule.DayId.ToString(), dailyScheduleData);
                        }

                        if (part != null && dailyScheduleData.Station.Part.PartId == part.PartId)  dailyScheduleData.Station.Part = part;
                        if (prevPart != null && dailyScheduleData.Station.StationId == prevPart.StationId)  dailyScheduleData.Station.PrevPart.Add(prevPart);

                        return dailySchedule;
                    },
                    p,
                    splitOn: "TaskId, UserId, StationId, PartId, MachineId, PartId",
                    commandType: CommandType.StoredProcedure
                );

                return dailyScheduleDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
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

        public async Task<string> Update_dailyTask()
        {
            //Implementar
            var sp = "sp_update_dailyTasks";
            var p = new DynamicParameters();
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