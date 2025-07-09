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

        public async Task<IEnumerable<Turn>> Select_turns_with_details()
        {
            var sp = "sp_select_turns_with_details";
            var turnDict = new Dictionary<string, Turn>();

            await _db.QueryAsync<Turn, TurnDetail, Turn>(
                sp,
                (turn, turnDetail) =>
                {
                    if (!turnDict.TryGetValue(turn.TurnId.ToString(), out var turnData))
                    {
                        turnData = turn;
                        turnData.TurnDetails = [];
                        turnDict.Add(turn.TurnId.ToString(), turnData);
                    }

                    if (turnDetail != null && !turnData.TurnDetails.Any(t => t.TurnId == turn.TurnId)) turnData.TurnDetails.Add(turnDetail);

                    return turnData;
                },
                splitOn: "TurnId, TurnDetailId",
                commandType: CommandType.StoredProcedure
            );

            return turnDict.Values;
        }

        #endregion

        #region Mutations

        public async Task<string> Create_turns(TurnDto turnDto)
        {
            var sp = "sp_create_update_turn_details";
            var p = new DynamicParameters();
            p.Add("@TurnId", Guid.NewGuid().ToString());
            p.Add("@Name", turnDto.Name);
            p.Add("@Duration", turnDto.Duration);
            p.Add("@ProductiveTime", turnDto.ProductiveTime);
            p.Add("@PauseTime", turnDto.PauseTime);
            p.Add("@CreatedBy", turnDto.CreatedBy);
            p.Add("@Action", "insertar");
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var detail in turnDto.Details)
                {
                    p.Add("@PeriodType", detail.PeriodType);
                    p.Add("@BeginAt", detail.BeginAt);
                    p.Add("@EndAt", detail.EndAt);
                    p.Add("@Quantity", detail.Quantity);
    
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

        public async Task<string> Update_turns(string turnId, TurnDto turnDto)
        {
            var sp = "sp_create_update_turn_details";
            var p = new DynamicParameters();
            p.Add("@TurnId", turnId);
            p.Add("@Name", turnDto.Name);
            p.Add("@Duration", turnDto.Duration);
            p.Add("@ProductiveTime", turnDto.ProductiveTime);
            p.Add("@PauseTime", turnDto.PauseTime);
            p.Add("@CreatedBy", null);
            p.Add("@Action", "actualizar");
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var detail in turnDto.Details)
                {
                    p.Add("@TurnDetailId", detail.Id);
                    p.Add("@PeriodType", detail.PeriodType);
                    p.Add("@BeginAt", detail.BeginAt);
                    p.Add("@EndAt", detail.EndAt);
                    p.Add("@Quantity", detail.Quantity);

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

        #endregion
    }
}