using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;
using System.Data;
using Dapper;
using Neuro.AI.Graph.Models.Dtos;

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

        public async Task<string> Create_groups(GroupDto groupDTo)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@FieldType", "Grupo");
            p.Add("@Name", groupDTo.Name);
            p.Add("@CreatedBy", groupDTo.CreatedBy);
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

        public async Task<string> Update_groups(string groupId, GroupDto groupDTo)
        {
            var sp = "sp_create_update_fields";
            var p = new DynamicParameters();
            p.Add("@Id", groupId);
            p.Add("@FieldType", "Grupo");
            p.Add("@Name", groupDTo.Name);
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
