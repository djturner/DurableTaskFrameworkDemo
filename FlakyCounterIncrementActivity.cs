using DurableTask.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurableTaskFrameworkDemo
{
	public class FlakyCounterIncrementActivity : TaskActivity<int, int>
	{
		static Random _random = new Random();

		protected override int Execute(TaskContext context, int input)
		{
			var rand = _random.NextDouble();
			if (rand > 0.3)
			{
				input++;
				Console.WriteLine($"New increment: {input}");
				return input;
			}

			Console.WriteLine($"Failed to increment: {input}");
			throw new ApplicationException();
		}
	}
}
