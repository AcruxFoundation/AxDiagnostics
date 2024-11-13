namespace AxDiagnostics
{
	public class DebugSection
	{
		public string Name { get; }
		public HashSet<LogGroup> Groups { get; } = [];

		public void AddGroup(LogGroup group)
		{
			Groups.Add(group);
		}

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		public override string ToString()
		{
			string message = $"[GROUP SECTION : {Name}]\n";
			foreach(LogGroup group in Groups)
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
