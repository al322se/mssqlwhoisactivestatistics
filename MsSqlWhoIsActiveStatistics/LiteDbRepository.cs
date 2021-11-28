using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;

namespace MsSqlWhoIsActiveStatistics
{
	public class LiteDbRepository
	{
		private readonly string _settingsDbName;
		private const string CollectionName = "WhoIsActiveRecords";

		public LiteDbRepository(string settingsDbName)
		{
			_settingsDbName = settingsDbName;
		}

		public void InsertData(IEnumerable<WhoIsActiveRecord> workerStates)
		{
			using (var db = new LiteDatabase(GetDbConnectionInner()))
			{
				var dbStates = db.GetCollection<WhoIsActiveRecord>(CollectionName);
				dbStates.InsertBulk(workerStates);
			}
		}

		private ConnectionString GetDbConnectionInner()
		{
			return new ConnectionString()
			{
				Filename = GetDbPath(), Connection = ConnectionType.Shared
			};
		}

		private string GetDbPath()
		{
			return Path.Combine(PathHelper.GetCurrentDirectory(),
				_settingsDbName);
		}
	}
}