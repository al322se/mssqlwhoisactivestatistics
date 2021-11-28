using System.IO;
using Microsoft.Extensions.Configuration;

namespace MsSqlWhoIsActiveStatistics
{
	class Program
	{
		static void Main(string[] args)
		{
			var configurationBuilder = new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(PathHelper.GetCurrentDirectory(),
					"config.json"));

			var configuration = configurationBuilder.Build();
			var settings = configuration.GetSection("AppSettings").Get<AppSettings>();
			var connectionString = settings.ConnectionString;
			var whoIsActiveWorker = new WhoIsActiveDbWorker(connectionString);

			var liteDbRepository = new LiteDbRepository(settings.DbName);
			var repeatService = new RepeatService(liteDbRepository, whoIsActiveWorker, settings.DelayTimespan);
			repeatService.Start();
		}
	}
}