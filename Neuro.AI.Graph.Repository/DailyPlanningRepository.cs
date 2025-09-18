using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Repository
{
    public class DailyPlanningRepository
    {
        private readonly IDbConnection _db;

        public DailyPlanningRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

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

        public async Task<IEnumerable<DailyPlanningSummary>> Select_dailyPlanning_Summary(string lineId, string productionDate)
        {
            var sp = "sp_select_dailyPlanning_Summary";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@ProductionDate", productionDate);

            try
            {
                return await _db.QueryAsync<DailyPlanningSummary>(
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

        public async Task<IEnumerable<DailyPlanningProductionLine>> Select_dailyPlanning_productionLine(string lineId, string productionDate)
        {
            var sp = "sp_select_dailyPlanning";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@ProductionDate", productionDate);

            try
            {
                return await _db.QueryAsync<DailyPlanningProductionLine>(
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

        #endregion

        #region Mutations
        #endregion

    }
}