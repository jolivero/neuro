using System;
using System.Threading.Tasks;
using Hangfire;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.HangFire.BackgroundJobs
{
	public class TestJob
	{
		private readonly DailyTaskRepository _repo;
		private readonly ILogger<TestJob> _logger;

		public TestJob(DailyTaskRepository repo, ILogger<TestJob> logger)
		{
			_repo = repo;
			_logger = logger;
		}

		[Queue("neurotask")]
		public async Task RunAsync()
		{
			_logger.LogInformation("▶️ Running daily tasks close...");

			try
			{
				var result = await _repo.UpdateCloseDailyTasksAsync();

				_logger.LogInformation("✔️ Completed: {result}", result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ Failed to close daily tasks");
				throw;
			}
		}
	}

}


