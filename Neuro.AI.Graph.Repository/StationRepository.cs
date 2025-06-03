using System;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class StationRepository
    {
        private readonly IDbConnection _db;
        public StationRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        public async Task<IEnumerable<Station>> Select_stations()
        {
            var query = "SELECT * FROM Stations";
            return await _db.QueryAsync<Station>(query);
        }

        public async Task<Station?> Select_stations(string stationId)
        {
            var query = $"SELECT * FROM Stations WHERE StationId = '{stationId}';";
            return await _db.QueryFirstOrDefaultAsync<Station>(query);
        }

        #endregion

        #region Mutations

        public async Task<string> Create_stations(StationDto stationDto)
        {
            var p = new DynamicParameters();
            p.Add("@Name", stationDto.Name);
            p.Add("@CreatedBy", stationDto.CreatedBy);

            var query = "INSERT INTO Stations" +
                "(Name, CreatedBy) " +
                "VALUES (@Name, @CreatedBy);";

            await _db.ExecuteAsync(query, p);

            return "New station added";
        }

        public async Task<string> Update_stations(string stationId, StationDto stationDto)
        {
            if (await Select_stations(stationId) == null) return "Station not found";

            var p = new DynamicParameters();
            p.Add("@Name", stationDto.Name);

            var query = $"UPDATE Stations SET Name = @Name WHERE StationId = '{stationId}';";

            await _db.ExecuteAsync(query, p);

            return $"Station {stationDto.Name} updated";
        }

        #endregion
    }
}
