using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class ManufacturingRepository
    {
        private readonly IDbConnection _db;
        public ManufacturingRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var companies = await _db.QueryAsync<Company>("SELECT * FROM Companies");
            return companies;
        }

        public async Task<IEnumerable<User>> GetUsersInfo()
        {
            var query = "SELECT * FROM Users AS u JOIN Companies AS c ON u.CompanyId = c.CompanyId";
            var result = await _db.QueryAsync<User, Company, User>(query, (user, company) =>
            {
                user.Company = company;
                return user;
            },
            splitOn: "CompanyId");

            return result;
        }

        public async Task<IEnumerable<ProductionLine>> GetProductionLineById(string lineId)
        {
            var line = await _db.QueryAsync<ProductionLine>($"SELECT * FROM ProductionLines WHERE LineId = '{lineId}'");
            return line;
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            var groups = await _db.QueryAsync<Group>("SELECT * FROM Groups");
            return groups;
        }
    }
}
