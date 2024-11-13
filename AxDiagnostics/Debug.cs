using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	public static class Debug
	{
		public static List<DebugSection> Sections { get; } = [];

		public static void Display()
		{
			string message = "[DEBUG]\n";
			foreach(DebugSection section in Sections)
			{
				message += $"{section}\n";
			}
		}
	}
}
