using System.Data;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Repository
{
    public class StatusControlRepository
    {
        private readonly IDbConnection _db;
        private readonly ProductionRecordRepository _productionRecordRepository;

        public StatusControlRepository(ManufacturingConnector manufacturingConnector, ProductionRecordRepository productionRecordRepository)
        {
            _db = manufacturingConnector.Connect();
            _productionRecordRepository = productionRecordRepository;
        }

        #region Queries

        public async Task<IEnumerable<string>> Select_statusControl_options()
        {
            var sp = "sp_select_status_options";

            try
            {
                return await _db.QueryAsync<string>(
                    sp,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);    
                throw new Exception(ex.Data.ToString());
            }
        }

        public async Task<IEnumerable<StatusControl>> Select_statusControl(string productionDate)
        {
            var sp = "sp_select_statusControl";
            var p = new DynamicParameters();
            p.Add("@ProductionDate", productionDate);

            try
            {
                return await _db.QueryAsync<StatusControl>(
                    sp,
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

        #region Mutations

        public async Task<string> Update_operator_status(string taskId, string userId, string status)
        {
            var prDto = new ProductionRecordDto()
            {
                TaskId = taskId,
                UserId = userId,
                Status = status,
                CurrentTime = DateTime.Now.TimeOfDay.ToString(),
                IsCut = false,
            };

            try
            {
                return await _productionRecordRepository.Create_Update_ProductionRecord(prDto);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}