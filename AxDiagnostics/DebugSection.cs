using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static System.Collections.Specialized.BitVector32;

namespace AxDiagnostics
{
	/// <summary>
	/// A collection of <see cref="LogGroup"/>s.<br></br>
	/// Generally used to group the different <see cref="LogGroup"/>s used in a specific system or module.
	/// </summary>
	public class DebugSection
	{
		/// <summary>
		/// The name of this <see cref="DebugSection"/>.
		/// </summary>
		[JsonProperty]
		public string Name { get; private set; }

		/// <summary>
		/// The <see cref="LogGroup"/>s conforming this <see cref="DebugSection"/>.
		/// </summary>
		[JsonProperty]
		public Dictionary<string, LogGroup> Groups { get; private set; } = [];

		/// <summary>
		/// Add a <see cref="LogGroup"/> to this <see cref="DebugSection"/>
		/// </summary>
		/// <param name="group">The <see cref="LogGroup"/> to add.</param>
		public void AddGroup(LogGroup group)
		{
			Groups.Add(group.Name, group);
		}

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		/// <summary>
		/// Returns an already exisitng or new instance of <see cref="LogGroup"/>
		/// based on the given <paramref name="name"/>
		/// </summary>
		/// <param name="name">The name of the group to get/create.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Returns an already exisitng or new instance of <see cref="LogGroup"/>
		/// from this <see cref="DebugSection"/> that's used for sharing logs created
		/// from the current thread
		/// </summary>
		/// <returns></returns>
		public LogGroup GetGroupForThread()
		{
			string name = $"[Thread] {Thread.CurrentThread.Name}";

			if (Groups.TryGetValue(name, out LogGroup? value)) return value;
			else
			{
				LogGroup group = new LogGroup(name);
				AddGroup(group);
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
