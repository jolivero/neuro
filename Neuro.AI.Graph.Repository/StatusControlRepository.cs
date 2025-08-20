using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Repository
{
    public class StatusControlRepository
    {
        private readonly IDbConnection _db;

        public StatusControlRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        public async Task<IEnumerable<StatusControl>> Select_statusControl(string productionDate)
        {
            var sp = "sp_select_statusControl";
            var p = new DynamicParameters();
            p.Add("@ProductionDate", productionDate);

            try
            {
                return await _db.QueryAsync<StatusControl>(
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