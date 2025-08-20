using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class ProductionRecordRepository
    {
        private readonly IDbConnection _db;

        public ProductionRecordRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        #endregion


        #region Mutations

        async public Task<string> Create_Update_ProductionRecord(ProductionRecordDto prDto)
        {
            var sp = "sp_create_update_productionRecord";
            var p = new DynamicParameters();
            p.Add("@TaskId", prDto.TaskId);
            p.Add("@UserId", prDto.UserId);
            p.Add("@Status", prDto.Status);
            p.Add("@CurrentTime", TimeSpan.Parse(prDto.CurrentTime));
            //p.Add("@Duration", prDto.Duration);
            p.Add("@IsCut", prDto.IsCut ? 1 : 0);
            p.Add("@Total", prDto.ProducedPart?.Total);
            p.Add("@ProducedPartId", prDto.ProducedPart?.PartId);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            try
            {
                if (prDto.NcProducedPart?.Count > 0)
                {
                    foreach (var ncProducedPart in prDto.NcProducedPart)
                    {
                        p.Add("@NcTotal", ncProducedPart.Total);
                        p.Add("@NcProducedPartId", ncProducedPart.PartId);

                        await _db.ExecuteAsync(
                            sp,
                            p,
                            commandType: CommandType.StoredProcedure
                        );
                    }
                }
                else
                {
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