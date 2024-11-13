using System.Text.RegularExpressions;
using static System.Collections.Specialized.BitVector32;

namespace AxDiagnostics
{
	public class DebugSection
	{
		public string Name { get; }
		public Dictionary<string, LogGroup> Groups { get; } = [];

		public void AddGroup(LogGroup group)
		{
			Groups.Add(group.Name, group);
		}

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		public LogGroup GetOrCreateGroup(string name)
		{
			if (Groups.TryGetValue(name, out LogGroup? value)) return value;
			else
			{
				LogGroup group = new LogGroup(name);
				Groups.Add(name, group);
				return group;
			}
		}

		public override string ToString()
		{
			string message = $"[GROUP SECTION : {Name}]\n";
			foreach(LogGroup group in Groups.Values)
			{
				message += $"{group}\n";
			}
			return message;
		}

		public DebugSection(string name)
		{
			Name = name;
		}
	}
}
