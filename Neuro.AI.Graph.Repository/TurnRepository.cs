using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Repository
{
    public class TurnRepository
    {
        private readonly IDbConnection _db;

        public TurnRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        // public async Task<IEnumerable<Turn>> Select_turns_with_details(string? turnId = null)
        // {
        //     var sp = "sp_select_turns_with_details";
        //     var p = new DynamicParameters();
        //     p.Add("@TurnId", turnId ?? null);

        //     var turnDict = new Dictionary<string, Turn>();

        //     await _db.QueryAsync<Turn, TurnDetail, Turn>(
        //         sp,
        //         (turn, turnDetail) =>
        //         {
        //             if (!turnDict.TryGetValue(turn.TurnId.ToString(), out var turnData))
        //             {
        //                 turnData = turn;
        //                 turnData.TurnDetails = [];
        //                 turnDict.Add(turn.TurnId.ToString(), turnData);
        //             }

        //             if (turnDetail != null && !turnData.TurnDetails.Any(t => t.TurnId == turn.TurnId)) turnData.TurnDetails.Add(turnDetail);

        //             return turnData;
        //         },
        //         splitOn: "TurnId, TurnDetailId",
        //         commandType: CommandType.StoredProcedure
        //     );

        //     return turnDict.Values;
        // }

        #endregion

        #region Mutations

        public async Task<string> Create_Update_turns(TurnDto turnDto)
        {
            var sp = "sp_create_update_turn_details";
            var p = new DynamicParameters();
            p.Add("@TurnId", turnDto.TurnId ?? Guid.NewGuid().ToString());
            p.Add("@Name", turnDto.Name);
            p.Add("@Duration", TimeSpan.Parse(turnDto.Duration));
            p.Add("@ProductiveTime", TimeSpan.Parse(turnDto.ProductiveTime));
            p.Add("@PauseTime", TimeSpan.Parse(turnDto.PauseTime));
            p.Add("@CreatedBy", turnDto.CreatedBy);
            p.Add("@Action", string.IsNullOrEmpty(turnDto.TurnId) ? "insertar" : "actualizar");
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var detail in turnDto.Details)
                {
                    p.Add("@TurnDetailId", detail.TurnDetailId ?? null);
                    p.Add("@PeriodType", detail.PeriodType);
                    p.Add("@BeginAt", TimeSpan.Parse(detail.BeginAt));
                    p.Add("@EndAt", TimeSpan.Parse(detail.EndAt));
                    p.Add("@DurationDetail", TimeSpan.Parse(detail.DurationDetail));

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
        
        public async Task<string> Delete_turn_details(string turnId)
        {
            var sp = "sp_delete_turn_details";
            var p = new DynamicParameters();
            p.Add("@TurnId", turnId);
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

        public async Task<string> Delete_turnDetail_Id(string turnDetailId)
        {
            var sp = "sp_delete_turnDetail_id";
            var p = new DynamicParameters();
            p.Add("@TurnDetailId", turnDetailId);
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