using Dapper;
using System.Data;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Models.Dtos;

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

        public async Task<string> Create_parts(PartDto partDto)
        {

            var sp = "sp_create_update_part_inventory";
            var p = new DynamicParameters();
            p.Add("@PName", partDto.Name);
            p.Add("@Code", partDto.Code);
            p.Add("@CreatedBy", partDto.CreatedBy);
            p.Add("@IName", partDto.Inventory.Name);
            p.Add("@Quantity", partDto.Inventory.Quantity);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryAsync(
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

        public async Task<string> Update_parts(string partId, PartDto partDto)
        {
            var sp = "sp_create_update_part_inventory";
            var p = new DynamicParameters();
            p.Add("@PartId", partId);
            p.Add("@PName", partDto.Name);
            p.Add("@Code", partDto.Code);
            p.Add("@CreatedBy", null);
            p.Add("@IName", partDto.Inventory.Name);
            p.Add("@Quantity", partDto.Inventory.Quantity);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryAsync(
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
