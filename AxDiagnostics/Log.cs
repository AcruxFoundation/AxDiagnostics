using System.Diagnostics;
using System.Reflection;

namespace AxDiagnostics
{
	/// <summary>
	/// Represents a message emitted by an event or system when certain actions are given.
	/// </summary>
	public class Log
	{
		public string Text { get; }
		public LogSeverity Severity { get; }
		public DateTime CreationDate { get; }
		private Thread OriginThread { get; }
		public string? OriginThreadName => OriginThread.Name;
		private MethodBase? OriginMethod { get; }
		public string? OriginMethodName => OriginMethod?.Name;
		public string? OriginTypeName => OriginMethod?.DeclaringType?.Name;
		public string? OriginTypeFQN => OriginMethod?.DeclaringType?.FullName;
		public string? Origin => $"{OriginTypeFQN}.{OriginTypeFQN}";
		public byte Indentation { get; set; }

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		public override string ToString()
		{
			string indentationString = new string('#', Indentation).Replace("#","|\t");
			return $"[{CreationDate}]\t{indentationString}[{Severity}] ({OriginThreadName} @ {OriginTypeFQN}.{OriginMethodName}) : {Text}";
		}

		#region Kind Driven Construction
		public static Log Message(string text) => new Log(text, LogSeverity.Message, 2);
		public static Log Info(string text) => new Log(text, LogSeverity.Info, 2);
		public static Log Warning(string text) => new Log(text, LogSeverity.Warning, 2);
		public static Log Error(string text) => new Log(text, LogSeverity.Error, 2);
		public static Log Fatal(string text) => new Log(text, LogSeverity.Fatal, 2);
		internal static Log ProcessStart(string text) => new Log(text, LogSeverity.ProcessStart, 3);
		internal static Log ProcessFinish(string text) => new Log(text, LogSeverity.ProcessFinish, 3);
		#endregion

		private Log(string text, LogSeverity kind, byte frameIndex)
		{
			StackTrace stackTrace = new StackTrace();

			Text = text;
			Severity = kind;
			OriginMethod = stackTrace.GetFrame(frameIndex)?.GetMethod();
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}

		public Log(string text, LogSeverity kind)
		{
			StackTrace stackTrace = new StackTrace();

			Text = text;
			Severity = kind;
			OriginMethod = stackTrace.GetFrame(1)?.GetMethod();
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}

		public Log(string text, LogSeverity kind, MethodBase? originMethod, Thread? originThread = null)
		{
			Text = text;
			Severity = kind;
			OriginMethod = originMethod;
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}
	}
}
