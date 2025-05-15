using Neuro.AI.Graph.Shield;
using Microsoft.Extensions.DependencyInjection;
using Keycloak.AuthServices.Authorization;

namespace Neuro.AI.Graph.Shield;
public static class AuthorizationPolicies
{
    public static void AddPermissionPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var permission in Enum.GetValues<Permissions>())
            {
                options.AddPolicy(permission.ToString(), policy => policy.RequireRealmRoles(permission.ToString()));
            }
        });
    }
}
