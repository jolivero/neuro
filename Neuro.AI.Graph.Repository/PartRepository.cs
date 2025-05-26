using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;

namespace Neuro.AI.Graph.Repository
{
    public class PartRepository
    {
        private readonly IDbConnection _db;
        public PartRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        public async Task<IEnumerable<Part>> Select_parts()
        {
            var query = "SELECT * FROM Parts";
            return await _db.QueryAsync<Part>(query);
        }
    }
}
