using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
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

        public async Task<IEnumerable<DailySchedule>> Select_dailyTask_by_userId(string currentDay, string userId)
        {
            var sp = "sp_select_dailyTask_by_userId";
            var p = new DynamicParameters();
            p.Add("@CurrentDay", currentDay);
            p.Add("@UserId", userId);

            var dailyScheduleDict = new Dictionary<string, DailySchedule>();

            try
            {
                await _db.QueryAsync<DailySchedule, DailyTask, User, Station, Part, Inventory, Machine, DailySchedule>(
                    sp,
                    (dailySchedule, task, user, station, part, inventory, machine) =>
                    {
                        if (!dailyScheduleDict.TryGetValue(dailySchedule.DayId.ToString(), out var dailyScheduleData))
                        {
                            dailyScheduleData = dailySchedule;
                            dailyScheduleData.DailyTasks = [];
                            dailyScheduleDict.Add(dailySchedule.DayId.ToString(), dailyScheduleData);
                        }

                        var taskData = dailyScheduleData.DailyTasks.FirstOrDefault(dt => dt.TaskId == task.TaskId);
                        if (taskData == null)
                        {
                            taskData = task;
                            taskData.User = new();
                            taskData.Station = new();
                            taskData.Machine = new();

                            dailyScheduleData.DailyTasks.Add(taskData);
                        }

                        if (user != null && taskData.UserId == user.UserId) taskData.User = user;
                        if (station != null && taskData.StationId == station.StationId)
                        {
                            taskData.Station = station;
                            taskData.Station.Parts = [];
                        }

                        if (part != null && !taskData.Station.Parts.Any(p => p.PartId == part.PartId))
                        {
                            if (inventory != null && part.PartId == inventory.PartId) part.Inventory = inventory;
                            taskData.Station.Parts.Add(part);
                        }

                        if (machine != null && taskData.MachineId == machine.MachineId) taskData.Machine = machine;

                        return dailyScheduleData;
                    },
                    p,
                    splitOn: "TaskId, UserId, StationId, PartId, InventoryId, MachineId",
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