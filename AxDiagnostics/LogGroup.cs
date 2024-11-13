using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	/// <summary>
	/// A specialized collection of <see cref="AxDiagnostics.Log"/>s.<br></br>
	/// Generally is used for grouping <see cref="AxDiagnostics.Log"/>s created in a common <see cref="Thread"/>.
	/// </summary>
	public class LogGroup
	{
		/// <summary>
		/// The name of this <see cref="LogGroup"/>
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// All the logs present in this <see cref="LogGroup"/> (order by log creation is not guaranteed).
		/// </summary>
		private List<Log> Logs { get; } = [];

		/// <summary>
		/// All the logs present in this collection ordered by log creation date in ascending order.
		/// </summary>
		public IOrderedEnumerable<Log> OrderedLogs => Logs.OrderBy(x => x.CreationDate);

		/// <summary>
		/// The creation date of the oldest <see cref="AxDiagnostics.Log"/> in this <see cref="LogGroup"/>.
		/// </summary>
		public DateTime FirstTimeLogged => OrderedLogs.First().CreationDate;

		/// <summary>
		/// The creation date of the newest <see cref="AxDiagnostics.Log"/> in this <see cref="LogGroup"/>.
		/// </summary>
		public DateTime LastTimeLogged => OrderedLogs.Last().CreationDate;

		/// <summary>
		/// The indentation level that the next added <see cref="AxDiagnostics.Log"/> will have.
		/// </summary>
		public byte Indentation { get; private set; }

		/// <summary>
		/// Add a <see cref="AxDiagnostics.Log"/> and display's it.
		/// </summary>
		/// <param name="log"></param>
		public void Log(Log log)
		{
			AddLog(log);
			log.Display();
		}

		/// <summary>
		/// Add a <see cref="AxDiagnostics.Log"/>.
		/// </summary>
		/// <param name="log"></param>
		public void AddLog(Log log)
		{
			Logs.Add(log);
			log.Indentation = Indentation;
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
