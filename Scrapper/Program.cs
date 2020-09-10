using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scrapper
{
	class Program
	{
		static void Main(string[] args)
		{
			var driver = new ChromeDriver();

			var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

			driver.Navigate().GoToUrl("https://www.flalottery.com/exptkt/p3.htm");

			var elements = driver
				.FindElements(By.TagName("tr"))
				.Select((el) =>
					el.FindElements(By.TagName("td"))
					  .Where(td => td.Text.Length > 0 && td.Text != "-")
				 );
			var dailyResults = new ConcurrentBag<DailyResult>();
			var stopwatch = Stopwatch.StartNew();
			object dailyResultsLock = new { };
			object temp = new { };
			Console.WriteLine("Starting Formatting data...");
			Parallel.ForEach(elements, parallelOptions, (tds) =>
			{

				var arr = tds.ToArray();
				Parallel.For(0, arr.Length, (index) =>
				{
					if (index + 4 >= arr.Length)
					{
						return;
					}
					var td = arr[index];
					DateTime date;
					var parsed = DateTime.TryParseExact(td.Text, "MM/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
					var dayMoment = arr[index + 1].Text.Trim();
					if (parsed && (dayMoment == "M" || dayMoment == "E"))
					{
						var number = int.Parse($"{arr[index + 2].Text}{arr[index + 3].Text}{arr[index + 4].Text}");
						var dailyResult = new DailyResult { Date = date, DayMoment = dayMoment, Number = number };
						dailyResults.Add(dailyResult);
						lock (dailyResultsLock)
						{

							Console.WriteLine($"Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
							Console.WriteLine($"DailyResult");
							Console.WriteLine("{");
							Console.WriteLine($"\t Date: { dailyResult.Date }");
							Console.WriteLine($"\t DayMoment: { dailyResult.DayMoment }");
							Console.WriteLine($"\t Number: { dailyResult.Number }");
							Console.WriteLine("}");
						}

					}
				});
			});
			stopwatch.Stop();
			Console.WriteLine($"Data successfully formatted. Time needed: {stopwatch.Elapsed.Minutes }");
			Console.WriteLine("Print Data?: Y/N");
			char response = Console.ReadKey().KeyChar;
			if (response == 'Y')
			{
				foreach (var r in dailyResults)
				{
					Console.WriteLine($"{r.Date} {r.Number} {r.DayMoment}");
				}
			}
		}
	}

	class DailyResult
	{
		public DateTime Date { get; set; }
		public string DayMoment { get; set; }
		public int Number { get; set; }
	}
}
