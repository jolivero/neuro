using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class DashboardRepository
    {

        private readonly IDbConnection _db;

        public DashboardRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        public async Task<IEnumerable<ProductionLineSummary>> Select_productionLine_summary(int month, int year)
        {

            var sp = "sp_select_productionLines_summary";
            var p = new DynamicParameters();
            p.Add("@Month", month);
            p.Add("@Year", year);

            try
            {
                return await _db.QueryAsync<ProductionLineSummary>(
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

        public async Task<IEnumerable<ProductionLineOperatorSummary>> Select_productionLine_operators_summary(int lineId, int month, int year)
        {

            var sp = "sp_select_productionLines_operators_summary";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@Month", month);
            p.Add("@Year", year);

            try
            {
                return await _db.QueryAsync<ProductionLineOperatorSummary>(
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

        #region Mutations
        #endregion

    }
}