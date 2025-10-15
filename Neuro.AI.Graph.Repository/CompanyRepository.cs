using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class CompanyRepository
    {
        private readonly IDbConnection _db;

        public CompanyRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        #endregion

        #region Mutations

        public async Task<MessageResponse> Create_Update_companies(CompanyDto company)
        {
            var sp = "sp_create_update_company";
            var p = new DynamicParameters();
            p.Add("@CompanyId", company.CompanyId ?? null);
            p.Add("@CompanyName", company.CompanyName);
            p.Add("@CompanyRuc", company.CompanyRuc);
            p.Add("@CompanyAddress", company.CompanyAddress);
            p.Add("@CompanyPhone", company.CompanyPhone);
            p.Add("@CompanyWeb", company.CompanyWeb);
            p.Add("@CompanyLogo", company.CompanyLogo);
            p.Add("@CompanyColors", company.CompanyColors);
            p.Add("@ContactName", company.ContactName);
            p.Add("@ContactPhone", company.ContactPhone);
            p.Add("@ContactEmail", company.ContactEmail);
            p.Add("@CreatedBy", company.CreatedBy);

            try
            {
                return await _db.QueryFirstAsync<MessageResponse>(sp, p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return new MessageResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        

        #endregion

    }

}
