using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class ChangeRequestRepository
    {
        private readonly IDbConnection _db;

        public ChangeRequestRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        #endregion

        #region Mutations

        public async Task<string?> Select_requestId(string monthId, string requestType)
        {
            var sp = "sp_select_requestId";
            var p = new DynamicParameters();
            p.Add("@MonthId", monthId);
            p.Add("@RequestType", requestType);
            p.Add("@RequestId", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                await _db.QueryFirstOrDefaultAsync<string>(
                    sp,
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return p.Get<string>("@RequestId");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Create_monthly_request(MonthlyChangeRequestDto mRequestDto)
        {
            var sp = "sp_create_monthly_request";
            var p = new DynamicParameters();
            p.Add("@MonthId", mRequestDto.MonthId);
            p.Add("@RequestingUserId", mRequestDto.RequestingUserId);
            p.Add("@RequestType", mRequestDto.RequestType);
            p.Add("@Reason", mRequestDto.Reason);
            p.Add("@CurrentValue", mRequestDto.CurrentValue);
            p.Add("@NewValue", mRequestDto.NewValue);
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

        public async Task<string> Create_daily_request(DailyChangeRequestDto dRequestDto)
        {
            var sp = "sp_create_monthly_request";
            var p = new DynamicParameters();
            p.Add("@RequestingUserId", dRequestDto.RequestingUserId);
            p.Add("@Reason", dRequestDto.Reason);
            p.Add("@RequestType", dRequestDto.RequestType);
            p.Add("@CurrentValue", dRequestDto.CurrentValue);
            p.Add("@NewValue", dRequestDto.NewValue);
            p.Add("@CurrentUserId", dRequestDto.CurrentUserId);
            p.Add("@NewUserId", dRequestDto.NewUserId ?? null);
            p.Add("@CurrentStationId", dRequestDto.CurrentStationId);
            p.Add("@StationId", dRequestDto.StationId ?? null);
            p.Add("@CurrentMachineId", dRequestDto.CurrentMachineId);
            p.Add("@MachineId", dRequestDto.MachineId ?? null);
            p.Add("@CurrentTunId", dRequestDto.CurrentTurnId ?? null);
            p.Add("@NewTurnId", dRequestDto.NewTurnId ?? null);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                foreach (var day in dRequestDto.DayId)
                {
                    p.Add("@DayId", day);

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
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> Update_status_request(UpdateStatusRequestDto usRequestDto)
        {
            var sp = "sp_update_request";
            var p = new DynamicParameters();
            p.Add("@RequestId", usRequestDto.RequestId);
            p.Add("@ApprovalUserId", usRequestDto.ApprovalUserId);
            p.Add("@Response", usRequestDto.Response);
            p.Add("@Status", usRequestDto.Status ? "A" : "R");
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