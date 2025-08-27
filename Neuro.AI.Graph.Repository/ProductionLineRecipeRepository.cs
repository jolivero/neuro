using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class ProductionLineRecipeRepository
    {
        private readonly IDbConnection _db;
        private Guid lineId;

        public ProductionLineRecipeRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        public async Task<IEnumerable<Group>> Select_groups(string taskId, string userId)
        {
            var sp_lineId = "sp_select_lineId_from_recipe";
            var p1 = new DynamicParameters();
            p1.Add("@TaskId", taskId);
            p1.Add("@UserId", userId);

            lineId = await _db.QueryFirstAsync<Guid>(
                sp_lineId,
                p1,
                commandType: CommandType.StoredProcedure
            );

            var sp_groupList = "sp_select_groups_from_recipe";
            var p2 = new DynamicParameters();
            p2.Add("@LineId", lineId);

            try
            {
                return await _db.QueryAsync<Group>(
                    sp_groupList,
                    p2,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Station>> Select_stations(string lineId, string groupId)
        {
            var sp_stations = "sp_select_stations_from_recipe";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@GroupId", groupId);

            try
            {
                return await _db.QueryAsync<Station>(
                    sp_stations,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Machine>> Select_machines(string lineId, string groupId, string stationId)
        {
            var sp_stations = "sp_select_machines_from_recipe";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@GroupId", groupId);
            p.Add("@StationId", stationId);

            try
            {
                return await _db.QueryAsync<Machine>(
                    sp_stations,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Part>> Select_parts(string lineId, string groupId, string stationId, string machineId)
        {
            var sp_stations = "sp_select_parts_from_recipe";
            var p = new DynamicParameters();
            p.Add("@LineId", lineId);
            p.Add("@GroupId", groupId);
            p.Add("@StationId", stationId);
            p.Add("@MachineId", machineId);

            try
            {
                return await _db.QueryAsync<Part>(
                    sp_stations,
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}