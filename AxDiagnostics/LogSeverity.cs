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
		Message,
		Info,
		Warning,
		Error,
		Fatal,
		ProcessStart,
		ProcessFinish
	}
}
