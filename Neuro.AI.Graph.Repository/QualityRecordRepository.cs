using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class QualityRecordRepository
    {

        private readonly IDbConnection _db;

        public QualityRecordRepository(ManufacturingConnector manufacturingConnector)
        {
            _db = manufacturingConnector.Connect();
        }

        #region Queries

        #endregion

        #region Mutations

        public async Task<string> Create_qualityRecord(QualityRecordDto qrDto)
        {
            var sp = "sp_create_qualityRecord";
            var p = new DynamicParameters();
            p.Add("@QualityId", Guid.NewGuid());
            p.Add("@NcPartId", qrDto.NcPartId);
            p.Add("@CreatedBy", qrDto.CreatedBy);
            p.Add("@MinParts", qrDto.QualityClasificationDto.MinParts);
            p.Add("@Reprocess", qrDto.QualityClasificationDto.Reprocess);
            p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            int scrapTotal = qrDto.QualityClasificationDto.ScrapDto.Sum(s => s.Scrap);
            int total = qrDto.QualityClasificationDto.Reprocess + scrapTotal;

            string status = qrDto.QualityClasificationDto.MinParts switch
            {   
                int minParts when minParts > total => "Pendiente",
                int minParts when minParts < total => "En revisión",
                _ => "Completo"
            };

            p.Add("@Status", status);
            p.Add("@Total", total);

            try
            {
                foreach (var scrap in qrDto.QualityClasificationDto.ScrapDto)
                {
                    p.Add("@Scrap", scrap.Scrap);
                    p.Add("@Reason", scrap.Reason);

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