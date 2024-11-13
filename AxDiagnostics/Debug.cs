using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	public static class Debug
	{
		public static Dictionary<string, DebugSection> Sections { get; } = [];

		public static void AddSection(DebugSection section)
		{
			Sections.Add(section.Name, section);
		}

		public static DebugSection GetOrCreateSection(string name)
		{
			if (Sections.TryGetValue(name, out DebugSection? value)) return value;
			else
			{
				DebugSection section = new DebugSection(name);
				Sections.Add(name, section);
				return section;
			}
		}

		public static void Display()
		{
			string message = "[DEBUG]\n";
			foreach(DebugSection section in Sections.Values)
			{
				message += $"{section}\n";
			}
			System.Diagnostics.Debug.WriteLine(message);
		}
	}
}
