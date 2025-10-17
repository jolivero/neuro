using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Queries
{
    public class ManufacturingQueries
    {
        #region Compañias

        #endregion

        #region Usuarios

        public async Task<IQueryable<OperatorProfile>> repo_user_with_skills(UserRepository repository, Guid userId)
        {
            return (await repository.Select_user_with_skills(userId)).AsQueryable();
        }

        public async Task<IQueryable<OperatorMonthlyPlanning>> repo_operators_with_monthlyPlanning(UserRepository repository, int month, int year, Guid? userId)
        {
            return (await repository.Select_operators_with_monthlyPlanning(month, year, userId)).AsQueryable();
        }

        #endregion

        #region Habilidades

        public async Task<IQueryable<string>> repo_skills_options(UserRepository repository)
        {
            return (await repository.Select_skill_options()).AsQueryable();
        }

        #endregion

        #region Línea de producción

        public async Task<IQueryable<ProductionLineBasicInfo>> repo_productionLines_basic(ProductionLineRepository repository, int lineId)
        {
            return (await repository.Select_productionLines_basic(lineId)).AsQueryable();
        }
        public async Task<IQueryable<ProductionLine>> repo_productionLines_with_details(ProductionLineRepository repository, int lineId)
        {
            return (await repository.Select_productionLines_with_details(lineId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> repo_productionLine_recipe(ProductionLineRepository repository, int taskId, Guid userId)
        {
            return (await repository.Select_productionLine_recipe(taskId, userId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLineMachineHoursPerCut>> repo_productionLines_with_machineHoursCut(ProductionLineRepository repository, int lineId)
        {
            return (await repository.Select_productionLines_with_machineHoursPerCut(lineId)).AsQueryable();
        }

        #endregion

        #region Grupos [Líneas de producción]


        #endregion

        #region Estaciones [Asignadas a grupos]

        public async Task<StationConfigInfo?> repo_station_with_configInfo(StationRepository repository, int recipeId)
        {
            return await repository.Select_station_with_configInfo(recipeId);
        }

        #endregion

        #region Máquinas

        #endregion

        #region Piezas

        #endregion

        #region Turnos/Detalles

        #endregion

        #region Dashborad

        public async Task<IQueryable<ProductionLineSummary>> repo_dashboard_productionLine_summary(DashboardRepository repository, int month, int year)
        {
            return (await repository.Select_productionLine_summary(month, year)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLineOperatorSummary>> repo_dashboard_productionLine_operators_summary(DashboardRepository repository, int lineId, int month, int year)
        {
            return (await repository.Select_productionLine_operators_summary(lineId, month, year)).AsQueryable();
        }

        #endregion

        #region Planificación anual

        public async Task<AnnualPlannigInfo> repo_annual_planning_info(MonthlyPlanningRepository repository, int year, int? month)
        {
            return await repository.Select_annual_planning_info(year, month);
        }

        public async Task<IEnumerable<MonthlyPlanningProgress>> repo_monthlyPlanning_progress(MonthlyPlanningRepository repository, int lineId, string currentDay)
        {
            return (await repository.Select_monthlyPlanning_Progress(lineId, currentDay)).AsQueryable();
        }

        public async Task<IQueryable<MonthlyPlanningProductionLines>> repo_annual_planning(MonthlyPlanningRepository repository, int? year, int? month, int? companyId)
        {
            return (await repository.Select_annual_planning(year, month, companyId)).AsQueryable();
        }

        #endregion

        #region Planificación mensual

        public async Task<IQueryable<MonthlyPlanning>> repo_station_with_machine_planning(MonthlyPlanningRepository repository, int monthId, int stationId, int machineId)
        {
            return (await repository.Select_station_with_machine_planning(monthId, stationId, machineId)).AsQueryable();
        }

        public async Task<IQueryable<MonthlyPlanningStepStatus>> repo_planning_step_status(MonthlyPlanningRepository repository, int monthId, int lineId)
        {
            return (await repository.Select_planning_step_status(monthId, lineId)).AsQueryable();
        }

        #endregion

        #region Planificación diaria

        public async Task<IQueryable<OperatorSelectList>> repo_available_operators(DailyPlanningRepository repository, List<string> days, string beginAt, string endAt)
        {
            return (await repository.Select_available_operators(days, beginAt, endAt)).AsQueryable();
        }

        public async Task<IQueryable<DailyPlanningSummary>> repo_dailyPlanning_Summary(DailyPlanningRepository repository, int lineId, string productionDate)
        {
            return (await repository.Select_dailyPlanning_Summary(lineId, productionDate)).AsQueryable();
        }

        public async Task<IQueryable<DailyPlanningProductionLine>> repo_dailyPlanning_productionLine(DailyPlanningRepository repository, int lineId, string productionDate)
        {
            return (await repository.Select_dailyPlanning_productionLine(lineId, productionDate)).AsQueryable();
        }

        #endregion

        #region Tareas diarias

        public async Task<IQueryable<ExtraTimeResponse>> repo_extraTime_operator(DailyTaskRepository repository, CheckOperatorExtraTimeDto operatorExtraTimeDto)
        {
            return (await repository.Select_extraTime_operator(operatorExtraTimeDto)).AsQueryable();
        }

        public async Task<IQueryable<DailyTaskOperator>> repo_dailyTasks_by_userId(DailyTaskRepository repository, string currentDate, Guid userId, int? taskId = null)
        {
            return (await repository.Select_dailyTask_by_userId(currentDate, userId, taskId)).AsQueryable();
        }

        public async Task<IEnumerable<DailyTask>> repo_dailyTask_operator_history(DailyTaskRepository repository, int taskId, Guid userId)
        {
            return (await repository.Select_dailyTask_operator_history(taskId, userId)).AsQueryable();
        }

        #endregion

        #region Control de estado

        public async Task<IQueryable<OptionsResponse>> repo_statusControl_options(StatusControlRepository repository)
        {
            return (await repository.Select_statusControl_options()).AsQueryable();
        }

        public async Task<IQueryable<StatusControl>> repo_statusControl(StatusControlRepository repository, string productionDate, int lineId)
        {
            return (await repository.Select_statusControl(productionDate, lineId)).AsQueryable();
        }

        #endregion

        #region Control de cambios

        public async Task<IQueryable<OptionsResponse>> repo_specialMissions_options(ChangeRequestRepository repository)
        {
            return (await repository.Select_specialMissions_options()).AsQueryable();
        }

        #endregion

    }
}
