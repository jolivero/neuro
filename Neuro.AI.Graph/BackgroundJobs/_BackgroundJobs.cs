using Hangfire;
using Neuro.AI.Graph.HangFire;
using Neuro.AI.Graph.HangFire.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neuro.AI.Graph.BackgroundJobs
{
	public class _BackgroundJobs
	{
		public static void Checker()
		{

			var servers = JobStorage.Current.GetMonitoringApi().Servers().ToList();

			servers.ForEach(s => JobStorage.Current.GetConnection().RemoveServer(s.Name));
		}

		public static void Loader()
		{
			Checker();

			RecurringJob.RemoveIfExists(nameof(TestJob));
			RecurringJob.AddOrUpdate<TestJob>(
				nameof(TestJob),
				"neurotask",
				job => job.RunAsync(),
				"*/5 * * * *",
				new RecurringJobOptions { TimeZone = TimeZoneInfo.Local }
			);

			//RecurringJob.RemoveIfExists(nameof(UserAccounts));
			//RecurringJob.AddOrUpdate<UserAccounts>(
			//    nameof(UserAccounts),
			//    job => job.RunAsync(),
			//    "10 * * * *",
			//    TimeZoneInfo.Local,
			//    "mmonlinetask"
			//    );
		}
	}
}
