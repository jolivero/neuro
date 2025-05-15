using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Neuro.AI.Graph.Shield.Solutions;

public class BearerPropagationService(IHttpContextAccessor http)
{
	public string? Token =>
		http.HttpContext?.Request.Headers.Authorization
			.FirstOrDefault()?.Replace("Bearer ", "");
}

public static class BearerPropagationSolutions
{
    public static IServiceCollection AddBearerPropagationService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<BearerPropagationService>();
        return services;
    }
}
