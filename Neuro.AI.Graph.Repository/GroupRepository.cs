using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Repository
{
    public class GroupRepository
    {
        private readonly IDbConnection _db;
        public GroupRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }
        #region Queries

        #endregion

        #region Mutations

        public async Task<MessageResponse> Create_groups(GroupDto groupDTo)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@FieldType", "Grupo");
            p.Add("@Name", groupDTo.Name);
            p.Add("@CreatedBy", groupDTo.CreatedBy);

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

        public async Task<MessageResponse> Update_groups(int groupId, GroupDto groupDTo)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@Id", groupId);
            p.Add("@FieldType", "Grupo");
            p.Add("@Name", groupDTo.Name);

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
