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

        public async Task<IEnumerable<Group>> Select_groups()
        {
            var query = "SELECT * FROM Groups;";
            return await _db.QueryAsync<Group>(query);
        }

        public async Task<Group?> Select_groups(string groupId)
        {
            var query = $"SELECT * FROM Groups WHERE GroupId = '{groupId}';";
            return await _db.QueryFirstOrDefaultAsync<Group>(query);
        }

        #endregion

        #region Mutations

        public async Task<string> Create_groups(GroupDto groupDTo)
        {
            var p = new DynamicParameters();
            p.Add("@Name", groupDTo.Name);
            p.Add("@CreatedBy", groupDTo.CreatedBy);

            var query = $"INSERT INTO Groups" +
                $"(Name, CreatedBy) " +
                $"VALUES(@Name, @CreatedBy);";

            await _db.ExecuteAsync(query, p);

            return $"Group {groupDTo.Name} added";
        }

        public async Task<string> Update_groups(string groupId, GroupDto groupDTo)
        {
            if (await Select_groups(groupId) == null) return "Group not found";

            var p = new DynamicParameters();
            p.Add("@Name", groupDTo.Name);

            var query = $"UPDATE Groups SET Name = @Name WHERE GroupId = '{groupId}';";

            await _db.ExecuteAsync(query, p);

            return $"Group {groupId} Updated";
        }

        #endregion
    }
}
