using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;
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

        public async Task<IEnumerable<Part>> Select_parts()
        {
            var query = "SELECT * FROM Parts;";
            return await _db.QueryAsync<Part>(query);
        }

        public async Task<Part?> Select_parts(string partId)
        {
            var query = $"SELECT * FROM Parts WHERE PartId = '{partId}';";
            return await _db.QueryFirstOrDefaultAsync<Part>(query);
        }

        #endregion

        #region Mutations

        public async Task<string> Create_parts(PartDto partDto)
        {
            var partParams = new DynamicParameters();
            partParams.Add("@Name", partDto.Name);
            partParams.Add("@Code", partDto.Code);
            partParams.Add("@CreatedBy", partDto.CreatedBy);

            var partQuery = @"INSERT INTO Parts
                (Name, Code, CreatedBy) 
                OUTPUT INSERTED.PartId 
                VALUES(@Name, @Code, @CreatedBy);";

            var partId = await _db.QuerySingleAsync<Guid>(partQuery, partParams);

            var inventoryParams = new DynamicParameters();
            inventoryParams.Add("@Name", partDto.Inventory.Name);
            inventoryParams.Add("@Quantity", partDto.Inventory.Quantity);
            inventoryParams.Add("@PartId", partId);

            var inventoryQuery = @"INSERT INTO Inventory 
                (Name, Quantity, PartId) 
                VALUES(@Name, @Quantity, @PartId)";

            await _db.ExecuteAsync(inventoryQuery, inventoryParams);

            return "New part added";
        }

        public async Task<string> Update_parts(string partId, PartDto partDto)
        {
            if (await Select_parts(partId) == null) return "Part not found";

            var partParams = new DynamicParameters();
            partParams.Add("@Name", partDto.Name);
            partParams.Add("@Code", partDto.Code);

            var inventoryParams = new DynamicParameters();
            inventoryParams.Add("@Name", partDto.Inventory.Name);
            inventoryParams.Add("@Quantity", partDto.Inventory.Quantity);

            var partQuery = $"UPDATE Parts Set Name = @Name, Code = @Code WHERE PartId = '{partId}';";
            var inventoryQuery = $"UPDATE Inventory Set Name = @Name, Quantity = @Quantity WHERE PartId = '{partId}';";

            await _db.ExecuteAsync(partQuery, partParams);
            await _db.ExecuteAsync(inventoryQuery, inventoryParams);

            return $"Part updated";
        }

        #endregion
    }
}
