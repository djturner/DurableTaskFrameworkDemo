using DurableTask.Core;
using DurableTask.Emulator;
using System;
using System.Threading.Tasks;

namespace DurableTaskFrameworkDemo
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var orchestrationServiceAndClient =
				new LocalOrchestrationService();

			Console.WriteLine("Creating worker");
			TaskHubWorker hubWorker = new TaskHubWorker(orchestrationServiceAndClient)
				.AddTaskOrchestrations(typeof(FlakyCounterOrchestration))
				.AddTaskActivities(typeof(FlakyCounterIncrementActivity));
			Console.WriteLine("Starting worker");
			hubWorker.StartAsync();

			Console.WriteLine("Creating Client");
			TaskHubClient client = new TaskHubClient(orchestrationServiceAndClient);
			Console.WriteLine("Awaiting Client");
			var instance = await client.CreateOrchestrationInstanceAsync(typeof(FlakyCounterOrchestration), 0);
			var result = await client.WaitForOrchestrationAsync(instance, new TimeSpan(0, 0, 60));
			Console.WriteLine($"Result: {result.Output}");
			Console.WriteLine("Done!");
		}
	}
}
