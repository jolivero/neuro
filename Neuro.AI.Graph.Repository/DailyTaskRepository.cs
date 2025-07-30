using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class DailyTaskRepository
    {
        private readonly IDbConnection _db;

        public DailyTaskRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        #endregion

        #region Mutations

        public async Task<string> Create_dailyTask(DailyTaskDto dtDto)
        {
            var sp = "sp_create_dailyTasks";
            var p = new DynamicParameters();
            p.Add("@MonthId", dtDto.MonthId);
            p.Add("@TurnId", dtDto.TurnId);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var assigment in dtDto.Assigments)
                {
                    p.Add("@DayId", assigment.DayId);
                    p.Add("@BeginAt", TimeSpan.Parse(assigment.BeginAt));
                    p.Add("@UserId", assigment.UserId);
                    p.Add("@StationId", assigment.StationId);
                    p.Add("@MachineId", assigment.MachineId);

                    await _db.ExecuteAsync(
                        sp,
                        p,
                        commandType: CommandType.StoredProcedure
                    );

                }

                return p.Get<string>("@Message");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Update_dailyTask()
        {
            //Implementar
            var sp = "sp_update_dailyTasks";
            var p = new DynamicParameters();
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