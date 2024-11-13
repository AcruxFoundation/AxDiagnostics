using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	public static class LogSeverityColors
	{
		public static ConsoleColor GetColorBySeverity(LogSeverity severity)
		{
			switch(severity)
			{
				case LogSeverity.Message : return ConsoleColor.White;
				case LogSeverity.Info: return ConsoleColor.Cyan;
				case LogSeverity.Warning: return ConsoleColor.Yellow;
				case LogSeverity.Error: return ConsoleColor.Red;
				case LogSeverity.Fatal: return ConsoleColor.DarkRed;
				case LogSeverity.ProcessStart: return ConsoleColor.DarkGray;
				case LogSeverity.ProcessFinish: return ConsoleColor.DarkGray;
				default: return ConsoleColor.White;
			}
		}
	}
}
