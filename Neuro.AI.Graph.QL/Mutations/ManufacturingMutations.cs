using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Mutations
{
    public class ManufacturingMutations
    {

        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public ManufacturingMutations(IAzureBlobStorageService azureBlobStorageService)
        {
            _azureBlobStorageService = azureBlobStorageService;
        }

        #region Compañias

        public async Task<MessageResponse> repo_create_companies(CompanyRepository repository, CompanyDto companyDto, IFile? companyLogo)
        {
            string companyLogoUrl = string.Empty;

            if (companyLogo != null)
            {
                companyLogoUrl = await _azureBlobStorageService.UploadFile(companyLogo.OpenReadStream(), companyLogo.Name, companyLogo.ContentType);
            }

            companyDto.CompanyLogo = companyLogo != null ? companyLogoUrl : companyDto.CompanyLogo;

            return await repository.Create_Update_companies(companyDto);
        }

        #endregion

        #region Usuarios

        public async Task<MessageResponse> repo_create_update_user(UserRepository repository, UsersDto usersDto)
        {
            return await repository.Create_update_user(usersDto);
        }

        public async Task<MessageResponse> repo_create_update_userSkills(UserRepository repository, UserSkillsDto userSkillsDto)
        {
            return await repository.Update_user_skills(userSkillsDto);
        }

        public async Task<MessageResponse> repo_delete_user(UserRepository repository, Guid userId)
        {
            return await repository.Delete_user(userId);
        }

        #endregion

        #region Línea de producción

        public async Task<string> repo_create_productionLine(ProductionLineRepository repository, ProductionLineDto plDto)
        {
            return await repository.Create_productionLine(plDto);
        }

        public async Task<string> repo_update_productionLine(ProductionLineRepository repository, int lineId, ProductionLineDto plDto)
        {
            return await repository.Update_productionLine(lineId, plDto);
        }

        public async Task<MessageResponse> repo_create_productionLine_steps(ProductionLineRepository repository, ProductionLineConfigDto plConfigDto)
        {
            return await repository.Create_productionLine_steps(plConfigDto);
        }

        // public async Task<string> repo_update_productionLine_steps(ProductionLineRepository repository, ProductionLineHandleStepDto plUpdateDto)
        // {
        //     return await repository.Update_productionLine_steps(plUpdateDto);
        // }

        public async Task<MessageResponse> repo_update_productionLine_order(ProductionLineRepository repository, bool steps, List<OrderStepDto> osDto)
        {
            return await repository.Update_productionLine_order(steps, osDto);
        }

        public async Task<string> repo_delete_productionLine_steps(ProductionLineRepository repository, ProductionLineHandleStepDto plDeleteDto)
        {
            return await repository.Delete_productionLine_steps(plDeleteDto);
        }

        #endregion

        #region Grupos

        public async Task<MessageResponse> repo_create_groups(GroupRepository repository, GroupDto groupDto)
        {
            return await repository.Create_groups(groupDto);
        }

        public async Task<MessageResponse> repo_update_groups(GroupRepository repository, int groupId, GroupDto groupDto)
        {
            return await repository.Update_groups(groupId, groupDto);
        }

        #endregion

        #region Estaciones

        public async Task<MessageResponse> repo_create_stations(StationRepository repository, StationDto stationDto)
        {
            return await repository.Create_stations(stationDto);
        }

        public async Task<MessageResponse> repo_update_stations(StationRepository repository, int stationId, StationDto stationDto)
        {
            return await repository.Update_stations(stationId, stationDto);
        }

        #endregion

        #region Máquinas

        public async Task<MessageResponse> repo_create_machines(MachineRepository repository, MachineDto machineDto)
        {
            return await repository.Create_machines(machineDto);
        }

        public async Task<MessageResponse> repo_update_machines(MachineRepository repository, int machineId, MachineDto machineDto)
        {
            return await repository.Update_machines(machineId, machineDto);
        }

        public async Task<string> repo_create_machine_report(MachineRepository repository, MachineReportDto machineReportDto)
        {
            return await repository.Create_machine_report(machineReportDto);
        }

        public async Task<string> repo_update_machine_report(MachineRepository repository, int reportId, MachineReportDto machineReportDto)
        {
            return await repository.Update_machine_report(reportId, machineReportDto);
        }

        #endregion

        #region Piezas

        public async Task<MessageResponse> repo_create_parts(PartRepository repository, PartDto partDto)
        {
            return await repository.Create_parts(partDto);
        }

        public async Task<MessageResponse> repo_update_parts(PartRepository repository, int partId, PartDto partDto)
        {
            return await repository.Update_parts(partId, partDto);
        }

        #endregion

        #region Turnos-Detalles

        public async Task<MessageResponse> repo_create_update_turn_with_details(TurnRepository repository, TurnDto turnDto)
        {
            return await repository.Create_Update_turns(turnDto);
        }

        public async Task<MessageResponse> repo_delete_turn_with_details(TurnRepository repository, int turnId)
        {
            return await repository.Delete_turn_details(turnId);
        }

        public async Task<MessageResponse> repo_delete_turnDetail(TurnRepository repository, int turnDetailId)
        {
            return await repository.Delete_turnDetail_Id(turnDetailId);
        }

        #endregion

        #region Planeación mensual

        public async Task<string> repo_create_monthlyPlanning(MonthlyPlanningRepository repository, MonthlyPlanningDto mpDto)
        {
            return await repository.Create_monthly_schedule(mpDto);
        }

        public async Task<string> repo_update_monthlyGoalPlanning(MonthlyPlanningRepository repository, UpdateMonthlyPlanningDto mgDto)
        {
            return await repository.Update_monthlyGoal_planning(mgDto);
        }

        public async Task<string> repo_update_monthlyDaysPlanning(MonthlyPlanningRepository repository, UpdateMonthlyPlanningDto mdDto)
        {
            return await repository.Update_monthlyDays_schedule(mdDto);
        }

        public async Task<string> repo_update_monthlyPlanning_operator(MonthlyPlanningRepository repository, int monthId, int dayId, int userId)
        {
            return await repository.Update_monthlyPlanning_operator(monthId, dayId, userId);
        }

        public async Task<string> repo_delete_monthlyDaysSchedule(MonthlyPlanningRepository repository, int monthId)
        {
            return await repository.Delete_monthlyDays_planning(monthId);
        }

        public async Task<string> repo_delete_monthlyPlannin_operator(MonthlyPlanningRepository repository, int monthId, int dayId, int userId)
        {
            return await repository.Delete_monthlyPlanning_operator(monthId, dayId, userId);
        }

        #endregion

        #region Tareas diarias

        public async Task<string> repo_create_dailyTasks(DailyTaskRepository repository, DailyTaskDto dtTaskDto)
        {
            return await repository.Create_dailyTask(dtTaskDto);
        }

        public async Task<string> repo_update_dailyTasks(DailyTaskRepository repository, DailyTaskDto dtTaskDto)
        {
            return await repository.Update_dailyTask(dtTaskDto);
        }

        public async Task<string> repo_update_revert_dailyTask_planning(DailyTaskRepository repository, int requestId, int taskId, Guid userId)
        {
            return await repository.Revert_dailyTask_Planning(requestId, taskId, userId);
        }

        #endregion

        #region Solicitud de cambios

        public async Task<MessageResponse> repo_create_changeMonthlyPlanning_request(ChangeRequestRepository repository, ChangeMonthlyPlanningRequestDto mpRequestDto)
        {
            return await repository.Create_changeMonthlyPlanning_request(mpRequestDto);
        }

        public async Task<ChangePlanningRequestMessageResponse> repo_create_changePlanning_request(ChangeRequestRepository repository, ChangePlanificationRequestDto cpRequestDto)
        {
            return await repository.Create_changePlanning_request(cpRequestDto);
        }

        public async Task<MessageResponse> repo_create_changeOperator_request(ChangeRequestRepository repository, CommonChangeRequestDto cRequestDto)
        {
            return await repository.Create_changeOperator_request(cRequestDto);
        }

        public async Task<string> repo_create_specialMission_request(ChangeRequestRepository repository, CommonRequestDto cRequestDto)
        {
            return await repository.Create_specialMission_request(cRequestDto);
        }

        public async Task<string> repo_create_extraTime_request(ChangeRequestRepository repository, string currentDate, ExtraTimeRequestDto etRequestDto)
        {
            return await repository.Create_extraTime_request(currentDate, etRequestDto);
        }

        public async Task<string> repo_update_status_request(ChangeRequestRepository repository, UpdateStatusRequestDto usRequestDto)
        {
            return await repository.Update_status_request(usRequestDto);
        }

        public async Task<string> repo_update_changeRequest_processed(ChangeRequestRepository repository, int requestId)
        {
            return await repository.Update_changeRequest_processedAt(requestId);
        }

        #endregion

        #region Registro de producción

        public async Task<string> repo_create_update_productionRecord(ProductionRecordRepository repository, ProductionRecordDto prDto)
        {
            return await repository.Create_Update_ProductionRecord(prDto);
        }

        #endregion

        #region Control de estado

        public async Task<string> repo_update_operator_status(StatusControlRepository repository, int taskId, Guid userId, string status)
        {
            return await repository.Update_operator_status(taskId, userId, status);
        }

        #endregion

        #region Registro de calidad

        public async Task<string> repo_create_qualityRecord(QualityRecordRepository repository, QualityRecordDto qrDto)
        {
            return await repository.Create_qualityRecord(qrDto);
        }

        #endregion

        #region Logs

        public async Task<string> repo_create_log(LogRepository repository, LogDto lgDto)
        {
            return await repository.Register_user_log(lgDto);
        }

        #endregion

    }
}
