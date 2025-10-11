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
            p.Add("@TurnId", turnDto.TurnId ?? null);
            p.Add("@Name", turnDto.Name);
            p.Add("@Duration", TimeSpan.Parse(turnDto.Duration));
            p.Add("@ProductiveTime", TimeSpan.Parse(turnDto.ProductiveTime));
            p.Add("@PauseTime", TimeSpan.Parse(turnDto.PauseTime));
            p.Add("@CreatedBy", turnDto.CreatedBy);
            p.Add("@Action", turnDto.TurnId  == null ? "insertar" : "actualizar");
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            var turnDetailsTable = new DataTable();
            turnDetailsTable.Columns.Add("TurnDetailId", typeof(int));
            turnDetailsTable.Columns.Add("PeriodType", typeof(string));
            turnDetailsTable.Columns.Add("BeginAt", typeof(string));
            turnDetailsTable.Columns.Add("EndAt", typeof(string));
            turnDetailsTable.Columns.Add("DurationDetail", typeof(string));
            turnDetailsTable.Columns.Add("Available", typeof(int));

            foreach (var detail in turnDto.Details)
            {
                turnDetailsTable.Rows.Add(
                    detail.Id,
                    detail.PeriodType,
                    TimeSpan.Parse(detail.BeginAt),
                    TimeSpan.Parse(detail.EndAt),
                    detail.DurationDetail,
                    detail.Available
                );
            }

            p.Add("@TurnDetailsTable", turnDetailsTable.AsTableValuedParameter("dbo.Manufacturing_TurnDetailsTableType"));

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