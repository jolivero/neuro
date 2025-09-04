using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class QualityRecordRepository
    {

        private readonly IDbConnection _db;
        private readonly ChangeRequestRepository _changeRequestRepository;

        public QualityRecordRepository(ManufacturingConnector manufacturingConnector, ChangeRequestRepository changeRequestRepository)
        {
            _db = manufacturingConnector.Connect();
            _changeRequestRepository = changeRequestRepository;
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
            p.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

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

                if (status == "En revisión")
                {
                    var changeRequestDto = new ChangeRequestDto()
                    {
                        NcPartId = qrDto.NcPartId,
                        UserId = qrDto.CreatedBy,
                        CreatedBy = qrDto.CreatedBy,
                        CategoryId = "679B65D1-0521-4776-AA94-FC8369680DF3",
                        OriginRequest = "Registro de calidad",
                        RequestType = "Registro de pieza no conforme",
                        CurrentValue = qrDto.QualityClasificationDto.MinParts.ToString(),
                        NewValue = total.ToString()
                    };

                    return await _changeRequestRepository.Create_change_request(changeRequestDto);
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