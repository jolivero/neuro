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

        #region Planificación mensual

        #endregion
    }
}
