using Data.Entities.Telered;
using TropigasMobile.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Neuro.AI.Graph.Repository;
using Neuro.AI.Graph.Models.Manufacturing;


namespace Neuro.AI.Graph.QL.Queries;

public class CustomQueries
{
	public IQueryable<OrdersSessions> GetCustomOrdersSessions(ApplicationDbContext context) => context.OrdersSessions.AsNoTracking();

	public async Task<IQueryable<UserDto>> GetAllUsersAsync(UserRepository repository, int? filters)
	{
		var users = await repository.GetAllUsersAsync(DateTime.Now.AddYears(-1), filters);

		return users.AsQueryable(); 
	}

	public async Task<IQueryable<Group>> GetAllGroupsAsync(ManufacturingRepository repository)
	{
		var groups = await repository.GetGroups();

		return groups.AsQueryable();
	}

}
