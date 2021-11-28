using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSqlWhoIsActiveStatistics
{
	public class RepeatService
	{
		private readonly LiteDbRepository _liteDbRepository;
		private readonly WhoIsActiveDbWorker _whoIsActiveDbWorker;
		private readonly TimeSpan _delayTimespan;

		public RepeatService(LiteDbRepository liteDbRepository,WhoIsActiveDbWorker whoIsActiveDbWorker, TimeSpan delayTimespan)
		{
			_liteDbRepository = liteDbRepository;
			_whoIsActiveDbWorker = whoIsActiveDbWorker;
			_delayTimespan = delayTimespan;
		}

		public void Start()
		{
			while (true)
			{
				try
				{
					var results = _whoIsActiveDbWorker.GetWhoIsActiveResults();
					Console.WriteLine($"Read {results.Count()} items");
					_liteDbRepository.InsertData(results);
					Task.Delay(_delayTimespan).Wait();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}