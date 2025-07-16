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

        public async Task<StationConfigInfo?> Select_station_with_configInfo(string recipeId)
        {
            var sp = "sp_select_station_config";
            var p = new DynamicParameters();
            p.Add("@RecipeId", recipeId);

            var response = await _db.QueryAsync<StationConfigInfo, MachineDto, PartDto, InventoryDto, StationConfigInfo>(
                sp,
                (s, m, p, i) =>
                {
                    s.Machine = m;
                    s.Part = p;
                    s.Part.Inventory = i;

                    return s;
                },
                p,
                splitOn: "MachineId, PartId, InventoryId",
                commandType: CommandType.StoredProcedure
            );

            return response.FirstOrDefault();
        }
    
        #endregion

        #region Mutations

        public async Task<string> Create_stations(StationDto stationDto)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@FieldType", "Estación");
            p.Add("@Name", stationDto.Name);
            p.Add("@CreatedBy", stationDto.CreatedBy);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
    
                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Update_stations(string stationId, StationDto stationDto)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@Id", stationId);
            p.Add("@FieldType", "Estación");
            p.Add("@Name", stationDto.Name);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.ExecuteAsync(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
