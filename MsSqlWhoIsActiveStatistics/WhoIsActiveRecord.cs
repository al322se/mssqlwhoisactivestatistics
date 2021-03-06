using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable InconsistentNaming

namespace MsSqlWhoIsActiveStatistics
{
	public class WhoIsActiveRecord
	{
		public string session_id { get; set; }
		public string sql_text { get; set; }
		public string login_name { get; set; }
		public string wait_info { get; set; }
		public string cpu { get; set; }
		public string tempdb_allocations { get; set; }
		public string tempdb_current { get; set; }
		public string blocking_session_id { get; set; }
		public string reads { get; set; }
		public string writes { get; set; }
		public string physical_reads { get; set; }
		public string used_memory { get; set; }
		public string status { get; set; }
		public string open_tran_count { get; set; }
		public string percent_complete { get; set; }
		public string host_name { get; set; }
		public string database_name { get; set; }
		public string program_name { get; set; }
		public string start_time { get; set; }
		public string login_time { get; set; }
		public string request_id { get; set; }
		public string collection_time { get; set; }
		public string query_plan { get; set; }
		public string locks { get; set; }
		public string additional_info { get; set; }
	}
}