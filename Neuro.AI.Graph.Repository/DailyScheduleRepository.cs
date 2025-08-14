using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;

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

        public async Task<string> Select_turn_validation(string monthId, string dayId, string userId, string? turnId, string beginAt, string endAt)
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

        }

        #endregion

        #region Mutations
        #endregion

    }
}