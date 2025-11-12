using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Repository
{
    public class BranchRepository
    {
        private readonly IDbConnection _db;

        public BranchRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries
        #endregion


        #region Mutations

        public async Task<MessageResponse> Create_Update_Branch()
        {
            var sp = "sp_create_update_branch";
            var p = new DynamicParameters();

            try
            {
                return await _db.QueryFirstAsync<MessageResponse>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }

        }

        #endregion
    }
}