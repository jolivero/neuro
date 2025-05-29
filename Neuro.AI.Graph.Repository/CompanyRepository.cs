using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
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

        public async Task<IEnumerable<Company>> Select_companies()
        {
            var query = "SELECT * FROM Companies;";
            return await _db.QueryAsync<Company>(query);
        }

        public async Task<Company?> Select_companies(string companyId)
        {
            var query = $"SELECT * FROM Companies WHERE CompanyId = '{companyId}';";
            var response = await _db.QueryFirstOrDefaultAsync<Company>(query);
            return response;
        }

        public async Task<IEnumerable<Company>> Select_companies_with_users_roles()
        {
            var query = "SELECT * FROM Companies AS c " +
                "JOIN Users AS u ON u.CompanyId = c.CompanyId " +
                "JOIN Roles AS r on r.RolId  = u.RolId;";

            var companyDict = new Dictionary<string, Company>();

            await _db.QueryAsync<Company, User, Role, Company>(query, (company, user, role) =>
            {
                if (!companyDict.TryGetValue(company.CompanyId.ToString(), out var companyData))
                {
                    companyData = company;
                    companyData.Users = new List<User>();
                    companyDict.Add(company.CompanyId.ToString(), companyData);
                }

                if (user != null)
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
                if (!companyDict.TryGetValue(company.CompanyId.ToString(), out var companyLineInfo))
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

        #endregion

        #region Mutations

        public async Task<string> Create_companies(CompanyDto company)
        {
            var p = new DynamicParameters();
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

            var query = $"INSERT INTO Companies " +
                $"(CompanyName, CompanyRuc, CompanyAddress, CompanyPhone, CompanyWeb, CompanyLogo, CompanyColors, ContactName, ContactPhone, ContactEmail, CreatedBy) " +
                $"VALUES (@CompanyName, @CompanyRuc, @CompanyAddress, @CompanyPhone, @CompanyWeb, @CompanyLogo, @CompanyColors, @ContactName, @ContactPhone, @ContactEmail, @CreatedBy);";


            await _db.ExecuteAsync(query, p);

            return "New company added";
        }

        public async Task<string> Update_companies(string companyId, CompanyDto company)
        {

            if (await Select_companies(companyId) == null) return "Company not found";

            var p = new DynamicParameters();
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

            var query = $"UPDATE Companies SET CompanyName = @CompanyName, CompanyRuc = @CompanyRuc, CompanyAddress = @CompanyAddress, CompanyPhone = @CompanyPhone," +
                $"CompanyWeb = @CompanyWeb, CompanyLogo = @CompanyLogo, CompanyColors = @CompanyColors, ContactName = @ContactName," +
                $"ContactPhone = @ContactPhone, ContactEmail = @ContactEmail WHERE CompanyId = '{companyId}';";


            await _db.ExecuteAsync(query, p);

            return $"Company {companyId} updated";
        }

        #endregion

    }

}
