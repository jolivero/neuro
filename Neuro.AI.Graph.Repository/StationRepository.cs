using System;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;

namespace Neuro.AI.Graph.Repository
{
    public class StationRepository
    {
        private readonly IDbConnection _db;
        public StationRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        public async Task<IEnumerable<Station>> Select_stations()
        {
            var query = "SELECT * FROM Stations";
            return await _db.QueryAsync<Station>(query);
        }
    }
}
