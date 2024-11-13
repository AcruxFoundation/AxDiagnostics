using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	/// <summary>
	/// Represents the severity or cause of the creation of a <see cref="Log"/>
	/// </summary>
	public enum LogSeverity : byte
	{
		/// <summary>
		/// A normal action given by a process.
		/// </summary>
		Message,

		/// <summary>
		/// An important or less common action given by a process.
		/// </summary>
		Info,

		/// <summary>
		/// A slight problem that could cause future problems if no action is taken.
		/// </summary>
		Warning,

		/// <summary>
		/// A problem that terminates the current process or operation
		/// </summary>
		Error,

		/// <summary>
		/// A problem that causes the total application termination
		/// </summary>
		Fatal,

		/// <summary>
		/// An automatic generated log by the <see cref="DebugProcess"/> class indicating the start of a process.
		/// </summary>
		ProcessStart,

		/// <summary>
		/// An automatic generated log by the <see cref="DebugProcess"/> class indicating the end of a process.
		/// </summary>
		ProcessFinish
	}
}
