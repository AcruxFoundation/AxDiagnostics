using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnostics
{
	public enum LogKind : byte
	{
		Message,
		Info,
		Warning,
		Error,
		Fatal
	}
}
