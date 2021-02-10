using DurableTask.Core;
using System;
using System.Threading.Tasks;

namespace DurableTaskFrameworkDemo
{
	public class FlakyCounterOrchestration : TaskOrchestration<int, int>
	{
		const int endNum = 10;
		public override async Task<int> RunTask(OrchestrationContext context, int inputCount)
		{
			var options = new RetryOptions(new TimeSpan(1), 10);
			while (inputCount < endNum)
			{
				inputCount = await context.ScheduleWithRetry<int>(typeof(FlakyCounterIncrementActivity), options, inputCount);
			}
			return inputCount;
		}
	}
}
