using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	public class LogGroup
	{
		public string Name { get; }
		private List<Log> Logs { get; } = [];
		public IOrderedEnumerable<Log> OrderedLogs => Logs.OrderBy(x => x.CreationDate);
		public DateTime FirstTimeLogged => OrderedLogs.First().CreationDate;
		public DateTime LastTimeLogged => OrderedLogs.Last().CreationDate;
		public byte Indentation { get; private set; }

		public void Log(Log log)
		{
			AddLog(log);
			log.Display();
		}

		public void AddLog(Log log)
		{
			Logs.Add(log);
		}

		public byte Indent() => Indentation++;
		public byte Unindent() => Indentation--;

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		public override string ToString()
		{
			string message = $"[DEBUG GROUP : {Name}]\n";
			foreach(Log log in OrderedLogs)
			{
				message += $"{log}\n";
			}
			return message;
		}

		public LogGroup(string name)
		{
			Name = name;
		}
	}
}
