using System.IO;

namespace MsSqlWhoIsActiveStatistics
{
	public static class PathHelper
	{
		public static string GetCurrentDirectory()
		{
			return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
		}
	}
}