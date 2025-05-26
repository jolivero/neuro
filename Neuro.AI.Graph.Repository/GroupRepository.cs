using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;

namespace Neuro.AI.Graph.Repository
{
    public class GroupRepository
    {
        private readonly IDbConnection _db;
        public GroupRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }
        public async Task<IEnumerable<Group>> Select_groups()
        {
            var query = "SELECT * FROM Groups";
            return await _db.QueryAsync<Group>(query);
        }
    }
}
