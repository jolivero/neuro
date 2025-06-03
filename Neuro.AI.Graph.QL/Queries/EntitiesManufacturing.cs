using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Queries
{
    public class EntitiesManufacturing
    {
        #region Compañias

        public async Task<IQueryable<Company>> repo_companies(CompanyRepository repository)
        {
            return (await repository.Select_companies()).AsQueryable();
        }

        public async Task<Company?> repo_companies_with_companyId(CompanyRepository repository, string companyId)
        {
            return await repository.Select_companies(companyId);
        }

        public async Task<IQueryable<Company>> repo_companies_with_users_roles(CompanyRepository repository)
        {
            return (await repository.Select_companies_with_users_roles()).AsQueryable();
        }

        public async Task<IQueryable<Company>> repo_companies_with_productionLines(CompanyRepository repository)
        {
            return (await repository.Select_companies_whith_productionLines()).AsQueryable();
        }

        #endregion

        #region Línea de producción

        public async Task<IQueryable<ProductionLine>> repo_productionLines(ProductionLineRepository repository)
        {
            return (await repository.Select_productionLines()).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> repo_productionLines_with_lineId(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines(lineId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> repo_productionLines_with_details(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines_With_Details(lineId)).AsQueryable();
        }

        #endregion

        #region Grupos [Líneas de producción]

        public async Task<IQueryable<Group>> repo_groups(GroupRepository repository)
        {
            return (await repository.Select_groups()).AsQueryable();
        }

        public async Task<Group?> repo_groups_with_groupId(GroupRepository repository, string groupId)
        {
            return await repository.Select_groups(groupId);
        }

        #endregion

        #region Estaciones [Asignadas a grupos]

        public async Task<IQueryable<Station>> repo_stations(StationRepository repository)
        {
            return (await repository.Select_stations()).AsQueryable();
        }

        #endregion

        #region Máquinas

        public async Task<IQueryable<Machine>> repo_machines(MachineRepository repository)
        {
            return (await repository.Select_machines()).AsQueryable();
        }

        public async Task<IQueryable<Machine>> repo_machines_with_machineId(MachineRepository repository, string machineId)
        {
            return (await repository.Select_machines(machineId)).AsQueryable();
        }

        public async Task<IQueryable<Machine>> repo_machines_with_reports(MachineRepository repository, string machineId)
        {
            return (await repository.Select_machine_with_reports(machineId)).AsQueryable();
        }

        #endregion

        #region Piezas

        public async Task<IQueryable<Part>> repo_parts(PartRepository repository)
        {
            return (await repository.Select_parts()).AsQueryable();
        }

        public async Task<Part?> repo_parts_with_partId(PartRepository repository, string partId)
        {
            return await repository.Select_parts(partId);
        }

        #endregion

    }
}
