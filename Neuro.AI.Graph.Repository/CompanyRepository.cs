

using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
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

        public async Task<IEnumerable<Company>> Select_companies()
        {
            var query = "SELECT * FROM Companies";
            return await _db.QueryAsync<Company>(query);
        }

        public async Task<IEnumerable<Company>> Select_companies_with_users_roles()
        {
            var query = "SELECT * FROM Companies AS c " +
                "JOIN Users AS u ON u.CompanyId = c.CompanyId " +
                "JOIN Roles AS r on r.RolId  = u.RolId;";

            var companyDict = new Dictionary<string, Company>();

            await _db.QueryAsync<Company, User, Role, Company>(query, (company, user, role) =>
            {
                if(!companyDict.TryGetValue(company.CompanyId.ToString(), out var companyData))
                {
                    companyData = company;
                    companyData.Users = new List<User>();
                    companyDict.Add(company.CompanyId.ToString(), companyData);
                }

                if(user != null)
                {
                    user.Rol = role;
                    companyData.Users.Add(user);
                }

                return companyData;
            }, splitOn: "UserId, RolId");

            return companyDict.Values;
        }

        public async Task<IEnumerable<Company>> Select_companies_whith_productionLines()
        {
            var query = "SELECT * FROM Companies AS c " +
                "JOIN ProductionLines AS pl ON pl.CompanyId = c.CompanyId;";

            var companyDict = new Dictionary<string, Company>();

            await _db.QueryAsync<Company, ProductionLine, Company>(query, (company, line) =>
            {
                if(!companyDict.TryGetValue(company.CompanyId.ToString(), out var companyLineInfo))
                {
                    companyLineInfo = company;
                    companyLineInfo.ProductionLines = new List<ProductionLine>();
                    companyDict.Add(company.CompanyId.ToString(), companyLineInfo);
                }

                if (line != null) companyLineInfo.ProductionLines.Add(line);

                return companyLineInfo;
            }, splitOn: "LineId");

            return companyDict.Values;
        }
    }
}
