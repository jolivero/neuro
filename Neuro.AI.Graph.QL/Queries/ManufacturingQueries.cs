using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Queries
{
    public class ManufacturingQueries
    {
        public async Task<IQueryable<Company>> GetCompaniesAsync(ManufacturingRepository repository)
        {
            return (await repository.GetCompanies()).AsQueryable();
        }

        public async Task<IQueryable<User>> GetAllUsersAsync(ManufacturingRepository repository)
        {
            return (await repository.GetUsersInfo()).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> GetProductionLinebyIdAsync(ManufacturingRepository repository, string lineId)
        {
            return (await repository.GetProductionLineById(lineId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> GetAllProductionLinesAsync(ManufacturingRepository repository)
        {
            return (await repository.GetProductionLines()).AsQueryable();
        }

        public async Task<IQueryable<ProductionLine>> GetProductionLineDetailByIdAsync(ManufacturingRepository repository, string lineId)
        {
            return (await repository.GetProductionLineDetail(lineId)).AsQueryable();
        }

        public async Task<IQueryable<Group>> GetAllGroupsAsync(ManufacturingRepository repository)
        {
            return (await repository.GetGroups()).AsQueryable();
        }

        public async Task<IQueryable<Station>> GetAllStationsAsync(ManufacturingRepository repository)
        {
            return (await repository.Getstation()).AsQueryable();
        }

        public async Task<IQueryable<Machine>> GetMachinesAsync(ManufacturingRepository repository)
        {
            return (await repository.GetMachines()).AsQueryable();
        }

        public async Task<IQueryable<Machine>> GetMachineByIdAsync(ManufacturingRepository repository, string machineId)
        {
            return (await repository.GetMachineById(machineId)).AsQueryable();
        }

        public async Task<IQueryable<Machine>> GetMachineWithReportsByMachineIdAsync(ManufacturingRepository repository, string machineId)
        {
            return (await repository.GetMachineWithReportsByMachineId(machineId)).AsQueryable();
        }

        public async Task<IQueryable<Part>> GetAllPartsAsync(ManufacturingRepository repository)
        {
            return (await repository.GetParts()).AsQueryable();
        }
    }
}
