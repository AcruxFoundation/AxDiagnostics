using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AxDiagnostics
{
	public static class Debug
	{
		internal static Dictionary<string, DebugSection> Sections { get; private set; } = [];
		public static string? DiagnosticsReportFileDestination { get; set; }

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

		public static string Serialize()
		{
			return JsonConvert.SerializeObject(Sections);
		}

		public static void Deserialize(string json)
		{
			Sections = JsonConvert.DeserializeObject<Dictionary<string, DebugSection>>(json) ?? [];
		}

		public static void SaveToFile()
		{
			if (DiagnosticsReportFileDestination == null) throw new InvalidOperationException($"Acrux's {nameof(Debug)} was unable to execute the {nameof(SaveToFile)} method since the property {nameof(DiagnosticsReportFileDestination)} was {null}.");
			if (!File.Exists(DiagnosticsReportFileDestination)) File.Create(DiagnosticsReportFileDestination).Dispose();
			File.WriteAllText(DiagnosticsReportFileDestination, Serialize());
		}
	}
}
