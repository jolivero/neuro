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

        #endregion

        #region Mutations

        public async Task<string> Create_stations(StationDto stationDto)
        {
            var p = new DynamicParameters();
            p.Add("@FieldType", "Estación");
            p.Add("@Name", stationDto.Name);
            p.Add("@CreatedBy", stationDto.CreatedBy);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            var sp = "sp_create_update_fields";

            await _db.ExecuteAsync(
                sp,
                p,
                commandType: CommandType.StoredProcedure
            );

            return p.Get<string>("@Message");
        }

        public async Task<string> Update_stations(string stationId, StationDto stationDto)
        {
            var p = new DynamicParameters();
            p.Add("@Id", stationId);
            p.Add("@FieldType", "Estación");
            p.Add("@Name", stationDto.Name);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            var sp = "sp_create_update_fields";

            await _db.ExecuteAsync(
                sp,
                p,
                commandType: CommandType.StoredProcedure
            );

            return p.Get<string>("@Message");
        }

        #endregion
    }
}
