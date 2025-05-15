using Data.Entities.Telered;
using TropigasMobile.Backend.Data;

namespace Neuro.AI.Graph.QL.Mutations;
public class EntitiesMutations
{
	public async Task<OrdersSessions> CreateOrdersSessions(
		OrdersSessions input,
		ApplicationDbContext db)
	{
		db.OrdersSessions.Add(input);
		await db.SaveChangesAsync();
		return input;
	}
}
