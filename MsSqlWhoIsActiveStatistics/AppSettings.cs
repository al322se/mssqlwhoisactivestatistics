using System;

namespace MsSqlWhoIsActiveStatistics
{
	public class AppSettings
	{
		public string ConnectionString { get; set; }
		public string DbName { get; set; }
		public TimeSpan DelayTimespan { get; set; }
	}
}