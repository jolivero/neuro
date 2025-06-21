using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Mutations
{
    public class ManufacturingMutations
    {
        #region Compañias

        public async Task<string> repo_create_companies(CompanyRepository repository, CompanyDto companyDto)
        {
            return await repository.Create_companies(companyDto);
        }

        public async Task<string> repo_update_companies(CompanyRepository repository, string companyId, CompanyDto companyDto)
        {
            return await repository.Update_companies(companyId, companyDto);
        }

        #endregion

        #region Usuarios

        public async Task<string> repo_create_update_user(UserRepository repository, UserIpcDto userIpcDto)
        {
            return await repository.Create_Update_user(userIpcDto);
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
    }
}
