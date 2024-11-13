using System.Diagnostics;
using System.Reflection;

namespace AxDiagnostics
{
	public class Log
	{
		public string Text { get; }
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

		#region Kind Driven Construction
		public static Log Message(string text) => new Log(text, LogKind.Message);
		public static Log Info(string text) => new Log(text, LogKind.Info);
		public static Log Warning(string text) => new Log(text, LogKind.Warning);
		public static Log Error(string text) => new Log(text, LogKind.Error);
		public static Log Fatal(string text) => new Log(text, LogKind.Fatal);
		#endregion

		public Log(string text, LogKind kind)
		{
			StackTrace stackTrace = new StackTrace();

			Text = text;
			Kind = kind;
			OriginMethod = stackTrace.GetFrame(1)?.GetMethod();
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}

		public Log(string text, LogKind kind, MethodBase? originMethod, Thread? originThread = null)
		{
			Text = text;
			Kind = kind;
			OriginMethod = originMethod;
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}
	}
}
