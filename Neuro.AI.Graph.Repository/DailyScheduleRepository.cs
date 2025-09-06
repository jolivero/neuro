using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Repository
{
    public class DailyScheduleRepository
    {
        private readonly IDbConnection _db;

        public DailyScheduleRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        /*public async Task<string> Select_turn_validation(string monthId, string dayId, string userId, string? turnId, string beginAt, string endAt)
        {
            var sp = "sp_validate_turn_change";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@DayId", dayId);
            p.Add("@UserId", userId);
            p.Add("@TurnId", turnId ?? null);
            p.Add("@BeginAt", beginAt);
            p.Add("@EndAt", endAt);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }*/

        public async Task<IEnumerable<OperatorSelectList>> Select_available_operators(List<string> days, string beginAt, string endAt)
        {
            var sp = "sp_select_available_operators";
            var operatorSelectList = new List<OperatorSelectList>();
            var p = new DynamicParameters();
            p.Add("@BeginAt", beginAt);
            p.Add("@EndAt", endAt);

            try
            {
                foreach (var day in days)
                {
                    p.Add("@ProductionDate", day);
                    var operators = await _db.QueryAsync<OperatorSelectList>(
                        sp,
                        p,
                        commandType: CommandType.StoredProcedure
                    );

                    operatorSelectList.AddRange(operators);
                }

                return operatorSelectList.GroupBy(op => op.UserId).Select(g => g.First());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Mutations
        #endregion

    }
}