using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Dapper;

namespace MsSqlWhoIsActiveStatistics
{
	public class WhoIsActiveDbWorker
	{
		private readonly string _connectionString;

		public WhoIsActiveDbWorker(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void CreateIfNotExist()
		{
			var sqlPath = Path.Combine(PathHelper.GetCurrentDirectory(),
				"sp_WhoIsActive.sql");
			var sql = File.ReadAllText(sqlPath);
			ExecuteNonQueryBatch(_connectionString
				,
				sql);
		}

		public IEnumerable<WhoIsActiveRecord> GetWhoIsActiveResults()
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				var result = connection.Query<WhoIsActiveRecord>("sp_WhoIsActive", new
				{
					// Uncomment for check on not high load db
					//show_sleeping_spids = 2,
					//show_system_spids = 1,
					//show_own_spid = 1,
					get_additional_info = 1,
					get_plans = 1,
					get_locks = 1,
					get_task_info = 2,
					get_full_inner_text = 1,
					format_output = 0
				}, commandType: CommandType.StoredProcedure);
				return result;
			}
		}

		private static void ExecuteNonQueryBatch(string connectionString, string sqlStatements)
		{
			if (sqlStatements == null) throw new ArgumentNullException("sqlStatements");
			if (sqlStatements == null) throw new ArgumentNullException("connectionString");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Regex r = new Regex(@"^(\s|\t)*go(\s\t)?.*", RegexOptions.Multiline | RegexOptions.IgnoreCase);
				connection.Open();
				var array= sqlStatements.Split("GO"+Environment.NewLine);
				foreach (string s in array)
				{
					//Skip empty statements, in case of a GO and trailing blanks or something
					string thisStatement = s.Trim();
					if (String.IsNullOrEmpty(thisStatement)) continue;

					using (SqlCommand cmd = new SqlCommand(thisStatement, connection))
					{
						cmd.CommandType = CommandType.Text;

						cmd.ExecuteNonQuery();
					}
				}
			}
		}
	}
}