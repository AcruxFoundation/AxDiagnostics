using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	/// <summary>
	/// Creates an automatic <see cref="LogGroup"/> manager. <br></br>
	/// Allows the automatic managment of the
	/// <see cref="LogGroup.Indent"/> and <see cref="LogGroup.Unindent"/> calls
	/// and simplifies the logging mechanism to the specified <see cref="Group"/>
	/// </summary>
	public class DebugProcess : IDisposable
	{
		private LogGroup Group { get; }
		private string? InitialMessage { get; }
		private string? FinalMessage { get; }

		public void Log(Log log)
		{
			Group.Log(log);
		}

		public void Dispose()
		{
			Group.Unindent();
			if(FinalMessage != null)
			{
				Log(AxDiagnostics.Log.ProcessFinish(FinalMessage));
			}
		}

		public DebugProcess(LogGroup group, string? initialMessage = "Process started", string? finalMessage = "Process finished")
		{
			Group = group;
			InitialMessage = initialMessage;
			FinalMessage = finalMessage;

			if(InitialMessage != null)
			{
				Log(AxDiagnostics.Log.ProcessStart(InitialMessage));
			}
			Group.Indent();
		}
	}
}
