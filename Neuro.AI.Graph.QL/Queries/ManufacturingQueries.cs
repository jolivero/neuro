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

        public async Task<IQueryable<OperatorProfile>> repo_user_with_skills(UserRepository repository, string userId)
        {
            return (await repository.Select_user_with_skills(userId)).AsQueryable();
        }

        public async Task<IQueryable<User>> repo_users_with_monthlySchedule(UserRepository repository, int month, int year, string? userId)
        {
            return (await repository.Select_users_with_monthlySchedule(month, year, userId)).AsQueryable();
        }

        #endregion

        #region Habilidades

        public async Task<IQueryable<string>> repo_skills_options(UserRepository repository)
        {
            return (await repository.Select_skill_options()).AsQueryable();
        }

        #endregion

        #region Línea de producción

        public async Task<IQueryable<ProductionLineBasicInfo>> repo_productionLines_basic(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines_basic(lineId)).AsQueryable();
        }
        public async Task<IQueryable<ProductionLine>> repo_productionLines_with_details(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines_with_details(lineId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> repo_productionLine_recipe(ProductionLineRepository repository, string taskId, string userId)
        {
            return (await repository.Select_productionLine_recipe(taskId, userId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLineMachineHoursPerCut>> repo_productionLines_with_machineHoursCut(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines_with_machineHoursPerCut(lineId)).AsQueryable();
        }

        #endregion

        #region Grupos [Líneas de producción]


        #endregion

        #region Estaciones [Asignadas a grupos]

        public async Task<StationConfigInfo?> repo_station_with_configInfo(StationRepository repository, string recipeId)
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

        #region Planificación anual

        public async Task<IQueryable<MonthlyPlanningProductionLines>> repo_annual_planification(MonthlyPlanningRepository repository, int? year, int? month)
        {
            return (await repository.Select_annual_planification(year, month)).AsQueryable();
        }

        #endregion

        #region Planificación mensual

        public async Task<IQueryable<MonthlyPlanning>> repo_station_with_machine_planification(MonthlyPlanningRepository repository, string monthId, string stationId, string machineId)
        {
            return (await repository.Select_station_with_machine_planification(monthId, stationId, machineId)).AsQueryable();
        }

        #endregion

        #region Planificación diaria

        public async Task<IQueryable<OperatorSelectList>> repo_available_operators(DailyPlanningRepository repository, List<string> days, string beginAt, string endAt)
        {
            return (await repository.Select_available_operators(days, beginAt, endAt)).AsQueryable();
        }

        public async Task<IQueryable<DailyPlanningSummary>> repo_dailyPlanning_Summary(DailyPlanningRepository repository, string lineId, string productionDate)
        {
            return (await repository.Select_dailyPlanning_Summary(lineId, productionDate)).AsQueryable();
        }

        public async Task<IQueryable<DailyPlanningProductionLine>> repo_dailyPlanning_productionLine(DailyPlanningRepository repository, string lineId, string productionDate)
        {
            return (await repository.Select_dailyPlanning_productionLine(lineId, productionDate)).AsQueryable();
        }

        #endregion

        #region Tareas diarias

        public async Task<IQueryable<ExtraTimeResponse>> repo_extraTime_operator(DailyTaskRepository repository, CheckOperatorExtraTimeDto operatorExtraTimeDto)
        {
            return (await repository.Select_extraTime_operator(operatorExtraTimeDto)).AsQueryable();
        }

        public async Task<IQueryable<DailyTaskOperator>> repo_dailyTasks_by_userId(DailyTaskRepository repository, string currentDate, string userId, string? taskId = null)
        {
            return (await repository.Select_dailyTask_by_userId(currentDate, userId, taskId)).AsQueryable();
        }

        #endregion

        #region Control de estado

        public async Task<IQueryable<string>> repo_statusControl_options(StatusControlRepository repository)
        {
            return (await repository.Select_statusControl_options()).AsQueryable();
        }

        public async Task<IQueryable<StatusControl>> repo_statusControl(StatusControlRepository repository, string productionDate)
        {
            return (await repository.Select_statusControl(productionDate)).AsQueryable();
        }

        #endregion

        #region Control de cambios

        public async Task<IQueryable<string>> repo_specialMissions_options(ChangeRequestRepository repository)
        {
            return (await repository.Select_specialMissions_options()).AsQueryable();
        }

        #endregion

    }
}
