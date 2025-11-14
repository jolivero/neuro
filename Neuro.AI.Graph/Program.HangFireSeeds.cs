using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neuro.AI.Graph.HangFireSeeds
{
	public class HangFireAuthorizeFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize(DashboardContext context)
		{
			var httpContext = context.GetHttpContext();

			// return httpContext.User.Identity.IsAuthenticated;

			return true;
		}
	}

	public class HangFireSeeds
	{
		public static string? cnn_hangfire;

		public static void GlobalHangFireServer(IGlobalConfiguration globalConfiguration)
		{
			globalConfiguration
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseSqlServerStorage(cnn_hangfire, new SqlServerStorageOptions
			{
				SlidingInvisibilityTimeout = TimeSpan.FromMinutes(30),
				QueuePollInterval = TimeSpan.Zero,
				UseRecommendedIsolationLevel = true,
				DisableGlobalLocks = true
			});
		}

		public static void SetBackJobServer(BackgroundJobServerOptions options)
		{
			options.WorkerCount = 1;
			options.ServerName = "neuro";
			options.Queues = ["neurotask"];
			options.ShutdownTimeout = TimeSpan.FromMinutes(10);
		}
	}
}
