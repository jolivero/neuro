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

        public async Task<IQueryable<Group>> GetAllGroupsAsync(ManufacturingRepository repository)
        {
            return (await repository.GetGroups()).AsQueryable();
        }
    }
}
