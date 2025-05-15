using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Neuro.AI.Graph.Shield.Settings
{
	public class KeycloakSettings
	{
		[JsonPropertyName("realm")]
		public string Realm { get; set; } = default!;

		[JsonPropertyName("auth-server-url")]
		public string AuthServerUrl { get; set; } = default!;

		[JsonPropertyName("ssl-required")]
		public string SslRequired { get; set; } = "external";

		[JsonPropertyName("resource")]
		public string Resource { get; set; } = default!;

		[JsonPropertyName("verify-token-audience")]
		public bool VerifyTokenAudience { get; set; }

		[JsonPropertyName("credentials")]
		public KeycloakCredentials Credentials { get; set; } = new();

		[JsonPropertyName("confidential-port")]
		public int ConfidentialPort { get; set; }

		[JsonPropertyName("policy-enforcer")]
		public KeycloakPolicyEnforcer PolicyEnforcer { get; set; } = new();
	}

	public class KeycloakCredentials
	
	{
		[JsonPropertyName("secret")]
		public string Secret { get; set; } = default!;
	}

	public class KeycloakPolicyEnforcer
	{
		[JsonPropertyName("credentials")]
		public Dictionary<string, object> Credentials { get; set; } = new();
	}

}
