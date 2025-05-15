using Data.Entities.Telered;
using TropigasMobile.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Neuro.AI.Graph.Repository;
using Neuro.AI.Graph.Shield.Solutions;


namespace Neuro.AI.Graph.QL.Queries;

public class AuthQueries
{
    public string? GetBearerPropagation(BearerPropagationService service) => service.Token;
}
