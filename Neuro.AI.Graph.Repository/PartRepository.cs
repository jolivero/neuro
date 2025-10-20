using Dapper;
using System.Data;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Repository
{
    public class PartRepository
    {
        private readonly IDbConnection _db;
        public PartRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        #endregion

        #region Mutations

        public async Task<MessageResponse> Create_parts(PartDto partDto)
        {

            var sp = "sp_create_update_part_inventory";
            var p = new DynamicParameters();
            p.Add("@PName", partDto.Name);
            p.Add("@Code", partDto.Code);
            p.Add("@IsPreviousPart", partDto.IsPreviousPart);
            p.Add("@CreatedBy", partDto.CreatedBy);
            p.Add("@IName", partDto.Inventory?.Name);
            p.Add("@Quantity", partDto.Inventory?.Quantity);

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

        public async Task<MessageResponse> Update_parts(int partId, PartDto partDto)
        {
            var sp = "sp_create_update_part_inventory";
            var p = new DynamicParameters();
            p.Add("@PartId", partId);
            p.Add("@PName", partDto.Name);
            p.Add("@Code", partDto.Code);
            p.Add("@IsPreviousPart", partDto.IsPreviousPart);
            p.Add("@CreatedBy", partDto.CreatedBy);
            p.Add("@IName", partDto.Inventory?.Name);
            p.Add("@Quantity", partDto.Inventory?.Quantity);

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
