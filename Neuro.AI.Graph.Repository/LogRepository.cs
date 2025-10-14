using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class LogRepository
    {
        private readonly IDbConnection _db;

        public LogRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries
        #endregion

        #region Mutations

        public async Task<string> Register_user_log(LogDto lgDto)
        {
            var sp = "sp_create_logs";
            var p = new DynamicParameters();
            p.Add("@UserIdRef", lgDto.UserIdRef);
            p.Add("@Area", lgDto.Area);
            p.Add("@Action", lgDto.Action);
            p.Add("@Payload", lgDto.Payload);
            p.Add("@Desc0", lgDto.Desc0);
            p.Add("@Desc1", lgDto.Desc1);
            p.Add("@Desc2", lgDto.Desc2);
            p.Add("@Desc3", lgDto.Desc3);
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
                return $"Error en registro de log: {ex.Message}";
            }
        }


        #endregion

    }
}