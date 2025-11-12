using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;

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

        public async Task<MessageResponse> Create_update_branch(BranchDto branchDto)
        {
            var sp = "sp_create_update_branch";
            var p = new DynamicParameters();
            p.Add("@BranchId", branchDto.BranchId ?? null);
            p.Add("@BranchName", branchDto.BranchName);
            p.Add("@BranchAddress", branchDto.BranchAddress);
            p.Add("@CompanyId", branchDto.CompanyId);

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