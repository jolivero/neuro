using System;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.CustomModels;

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

        public async Task<StationConfigInfo?> Select_station_with_configInfo(int recipeId)
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

        public async Task<MessageResponse> Create_stations(StationDto stationDto)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@FieldType", "Estación");
            p.Add("@Name", stationDto.Name);
            p.Add("@CreatedBy", stationDto.CreatedBy);

            try
            {
                return await _db.QueryFirstAsync<MessageResponse>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
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

        public async Task<MessageResponse> Update_stations(int stationId, StationDto stationDto)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@Id", stationId);
            p.Add("@FieldType", "Estación");
            p.Add("@Name", stationDto.Name);

            try
            {
                return await _db.QueryFirstAsync<MessageResponse>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );
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
