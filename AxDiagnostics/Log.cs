using System.Reflection;

namespace AxDiagnostics
{
	public class Log
	{
		public string Message { get; }
		public LogKind Kind { get; }
		public DateTime CreationDate { get; }
		private Thread OriginThread { get; }
		public string? OriginThreadName => OriginThread.Name;
		private MethodBase? OriginMethod { get; }
		public string? OriginMethodName => OriginMethod?.Name;
		public string? OriginTypeName => OriginMethod?.DeclaringType?.Name;
		public string? OriginTypeFQN => OriginMethod?.DeclaringType?.FullName;
		public string? Origin => $"{OriginTypeFQN}.{OriginTypeFQN}";

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		public override string ToString()
		{
			return $"[{CreationDate}] [{Kind}] ({OriginThreadName} @ {OriginTypeFQN}.{OriginMethodName}) : {Message}";
		}

		public Log(string message, LogKind kind, MethodBase? originMethod, Thread? originThread = null)
		{
			Message = message;
			Kind = kind;
			OriginMethod = originMethod;
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}
	}
}
