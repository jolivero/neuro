using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class MonthlyScheduleRepository
    {
        private readonly IDbConnection _db;

        public MonthlyScheduleRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        public async Task<IEnumerable<OperatorMonthlySchedule>> Select_monthly_operatorSchedule(string monthId)
        {

            var sp = "sp_select_operatorScheduleInfoByMonth";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);

            var operatorScheduleDict = new Dictionary<string, OperatorMonthlySchedule>();

            try
            {
                await _db.QueryAsync<OperatorMonthlySchedule, User, DailySchedule, OperatorMonthlySchedule>(
                sp,
                (o, u, ds) =>
                {
                    if (!operatorScheduleDict.TryGetValue(u.UserId.ToString(), out var operatorData))
                    {
                        operatorData = o;
                        operatorData.OperatorId = u.UserId;
                        operatorData.OperatorName = string.Concat(u.FirstName, ' ', u.LastName);
                        operatorData.Days = [];
                        operatorScheduleDict.Add(o.OperatorId.ToString(), operatorData);
                    }

                    var dsData = operatorData.Days.FirstOrDefault(d => d.DayId == ds.DayId.ToString());
                    if (dsData == null)
                    {
                        dsData = new AssignedDays() { DayId = ds.DayId.ToString(), ProductiveDate = ds.ProductionDate };
                        operatorData.Days.Add(dsData);
                    }

                    return operatorData;
                },
                p,
                splitOn: "UserId, DayId",
                commandType: CommandType.StoredProcedure
            );

                return operatorScheduleDict.Values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
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
            var sp = "sp_update_monthlyGoal";
            var p = new DynamicParameters();
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
            var businessDays = mdDto.UpdateDailyScheduleDto!.Count(d => d.DayType.Equals("laboral", StringComparison.CurrentCultureIgnoreCase) && d.Available == 1);
            var extraDays = mdDto.UpdateDailyScheduleDto!.Count(d => d.DayType.Equals("extra", StringComparison.CurrentCultureIgnoreCase) && d.Available == 1);
            var dailyGoal = mdDto.MonthlyGoal / (businessDays + extraDays);

            var sp = "sp_update_monthlyDays";
            var p = new DynamicParameters();
            p.Add("@MonthId", mdDto.MonthId);
            p.Add("@BusinessDays", businessDays);
            p.Add("@ExtraDays", extraDays);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var item in mdDto.UpdateDailyScheduleDto!)
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

        #endregion
    }
}