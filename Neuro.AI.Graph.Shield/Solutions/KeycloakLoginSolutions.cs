using System.Net.Http.Json;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.DependencyInjection;
using Neuro.AI.Graph.Shield.Settings;

namespace Neuro.AI.Graph.Shield.Solutions;

public static class KeycloakLoginSolutions
{
	private record AutRequest(string Username, string Password);

    public static IEndpointRouteBuilder UseKeycloakLoginAPI(this IEndpointRouteBuilder app)
    {
        app.MapPost("/shield/api/login", async (AutRequest request, KeycloakLoginService keycloak) =>
        {
            var token = await keycloak.GetAccessTokenAsync(request.Username, request.Password);

            return string.IsNullOrEmpty(token)
                ? Results.Unauthorized()
                : Results.Ok(new { access_token = token }); 
        });

        return app;
    }

    public static IServiceCollection AddKeycloakLoginService(this IServiceCollection services)
    {
        services.AddHttpClient<KeycloakLoginService>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(60);
        });

        return services;
    }

}

public class KeycloakLoginService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakSettings _settings;

    public KeycloakLoginService(HttpClient httpClient, KeycloakSettings settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<string> GetAccessTokenAsync(string username, string password)
    {
        var url = $"{_settings.AuthServerUrl.TrimEnd('/')}/realms/{_settings.Realm}/protocol/openid-connect/token";

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", _settings.Resource },
            { "client_secret", _settings.Credentials.Secret },
            { "username", username },
            { "password", password }
        });

        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode) throw new Exception($"Keycloak login failed: {await response.Content.ReadAsStringAsync()}");

        var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();

        return json?["access_token"]?.ToString() ?? throw new Exception("Access token not found.");
    }
}