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

        public async Task<string> repo_create_companies(CompanyRepository repository, CompanyDto companyDto, IFile companyLogo)
        {
            string companyLogoUrl = string.Empty;

            if (companyLogo != null)
            {
                using var fileStream = companyLogo.OpenReadStream();
                companyLogoUrl = await _azureBlobStorageService.UploadFile(fileStream, companyLogo.Name, companyLogo.ContentType);
            }

            companyDto.CompanyLogo = companyLogoUrl;

            return await repository.Create_companies(companyDto);
        }

        public async Task<string> repo_update_companies(CompanyRepository repository, string companyId, CompanyDto companyDto, IFile? companyLogo)
        {
            string companyLogoUrl = string.Empty;

            if (companyLogo != null)
            {
                using var fileStream = companyLogo.OpenReadStream();
                companyLogoUrl = await _azureBlobStorageService.UploadFile(fileStream, companyLogo.Name, companyLogo.ContentType);
            }

            companyDto.CompanyLogo = companyLogo != null ? companyLogoUrl : companyDto.CompanyLogo;

            return await repository.Update_companies(companyId, companyDto);
        }

        #endregion

        #region Usuarios

        public async Task<string> repo_create_update_user(UserRepository repository, UserIpcDto userIpcDto)
        {
            return await repository.Create_update_user(userIpcDto);
        }

        public async Task<string> repo_create_update_userSkills(UserRepository repository, UserSkillsDto userSkillsDto)
        {
            return await repository.Update_user_skills(userSkillsDto);
        }

        #endregion

        #region Línea de producción

        public async Task<string> repo_create_productionLine(ProductionLineRepository repository, ProductionLineDto plDto)
        {
            return await repository.Create_productionLine(plDto);
        }

        public async Task<string> repo_update_productionLine(ProductionLineRepository repository, string lineId, ProductionLineDto plDto)
        {
            return await repository.Update_productionLine(lineId, plDto);
        }

        public async Task<string> repo_create_productionLine_steps(ProductionLineRepository repository, ProductionLineConfigDto plConfigDto)
        {
            return await repository.Create_productionLine_steps(plConfigDto);
        }

        public async Task<string> repo_update_productionLine_steps(ProductionLineRepository repository, ProductionLineHandleStepDto plUpdateDto)
        {
            return await repository.Update_productionLine_steps(plUpdateDto);
        }

        public async Task<string> repo_delete_productionLine_steps(ProductionLineRepository repository, ProductionLineHandleStepDto plDeleteDto)
        {
            return await repository.Delete_productionLine_steps(plDeleteDto);
        }

        #endregion

        #region Grupos

        public async Task<string> repo_create_groups(GroupRepository repository, GroupDto groupDto)
        {
            return await repository.Create_groups(groupDto);
        }

        public async Task<string> repo_update_groups(GroupRepository repository, string groupId, GroupDto groupDto)
        {
            return await repository.Update_groups(groupId, groupDto);
        }

        #endregion

        #region Estaciones

        public async Task<string> repo_create_stations(StationRepository repository, StationDto stationDto)
        {
            return await repository.Create_stations(stationDto);
        }

        public async Task<string> repo_update_stations(StationRepository repository, string stationId, StationDto stationDto)
        {
            return await repository.Update_stations(stationId, stationDto);
        }

        #endregion

        #region Máquinas

        public async Task<string> repo_create_machines(MachineRepository repository, MachineDto machineDto)
        {
            return await repository.Create_machines(machineDto);
        }

        public async Task<string> repo_update_machines(MachineRepository repository, string machineId, MachineDto machineDto)
        {
            return await repository.Update_machines(machineId, machineDto);
        }

        public async Task<string> repo_create_machine_report(MachineRepository repository, MachineReportDto machineReportDto)
        {
            return await repository.Create_machine_report(machineReportDto);
        }

        public async Task<string> repo_update_machine_report(MachineRepository repository, string reportId, MachineReportDto machineReportDto)
        {
            return await repository.Update_machine_report(reportId, machineReportDto);
        }

        #endregion

        #region Piezas

        public async Task<string> repo_create_parts(PartRepository repository, PartDto partDto)
        {
            return await repository.Create_parts(partDto);
        }

        public async Task<string> repo_update_parts(PartRepository repository, string partId, PartDto partDto)
        {
            return await repository.Update_parts(partId, partDto);
        }

        #endregion

        #region Turnos-Detalles

        public async Task<string> repo_create_turn_with_details(TurnRepository repository, TurnDto turnDto)
        {
            return await repository.Create_Update_turns(turnDto);
        }

        public async Task<string> repo_update_turn_with_details(TurnRepository repository, TurnDto turnDto, string turnId)
        {
            return await repository.Create_Update_turns(turnDto, turnId);
        }

        public async Task<string> repo_delete_turn_with_details(TurnRepository repository, string turnId)
        {
            return await repository.Delete_turn_details(turnId);
        }

        public async Task<string> repo_delete_turnDetail(TurnRepository repository, string turnDetailId)
        {
            return await repository.Delete_turnDetail_Id(turnDetailId);
        }

        #endregion

        #region Planeación mensual

        public async Task<string> repo_create_monthlyPlanning(MonthlyPlanningRepository repository, MonthlyPlanningDto msDto)
        {
            return await repository.Create_monthly_schedule(msDto);
        }

        public async Task<string> repo_update_monthlyGoalPlanning(MonthlyPlanningRepository repository, UpdateMonthlyPlanningDto mgDto)
        {
            return await repository.Update_monthlyGoal_planning(mgDto);
        }

        public async Task<string> repo_update_monthlyDaysPlanning(MonthlyPlanningRepository repository, UpdateMonthlyPlanningDto mdDto)
        {
            return await repository.Update_monthlyDays_schedule(mdDto);
        }

        public async Task<string> repo_update_monthlyPlanning_operator(MonthlyPlanningRepository repository, string monthId, string dayId, string userId)
        {
            return await repository.Update_monthlyPlanning_operator(monthId, dayId, userId);
        }

        public async Task<string> repo_delete_monthlyDaysSchedule(MonthlyPlanningRepository repository, string monthId)
        {
            return await repository.Delete_monthlyDays_planning(monthId);
        }

        public async Task<string> repo_delete_monthlyPlannin_operator(MonthlyPlanningRepository repository, string monthId, string dayId, string userId)
        {
            return await repository.Delete_monthlyPlanning_operator(monthId, dayId, userId);
        }

        #endregion

        #region Tareas diarias

        public async Task<string> repo_create_dailyTasks(DailyTaskRepository repository, DailyTaskDto dtTaskDto)
        {
            return await repository.Create_dailyTask(dtTaskDto);
        }

        #endregion

        #region Solicitud de cambios

        public async Task<string> repo_create_monthly_request(ChangeRequestRepository repository, MonthlyChangeRequestDto mRequestDto)
        {
            return await repository.Create_monthly_request(mRequestDto);
        }

        public async Task<string> repo_create_daily_request(ChangeRequestRepository repository, DailyChangeRequestDto dRequestDto)
        {
            return await repository.Create_daily_request(dRequestDto);
        }

        public async Task<string> repo_create_changePlannification_request(ChangeRequestRepository repository, ChangePlanificationRequestDto cpRequestDto)
        {
            return await repository.Create_changePlannification_request(cpRequestDto);
        }

        public async Task<string> repo_create_changeOperator_request(ChangeRequestRepository repository, CommonRequestDto cRequestDto)
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

        #endregion

        #region Registro de producción

        public async Task<string> repo_create_update_productionRecord(ProductionRecordRepository repository, ProductionRecordDto prDto)
        {
            return await repository.Create_Update_ProductionRecord(prDto);
        }

        #endregion

        #region Control de estado

        public async Task<string> repo_update_operator_status(StatusControlRepository repository, string taskId, string userId, string status)
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

    }
}
