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
		public byte Indentation { get; set; }

		public void Display()
		{
			System.Diagnostics.Debug.WriteLine(this);
		}

		public override string ToString()
		{
			string indentationString = new string('#', Indentation).Replace("#","|\t");
			return $"[{CreationDate}]\t{indentationString}[{Kind}] ({OriginThreadName} @ {OriginTypeFQN}.{OriginMethodName}) : {Text}";
		}

		#region Kind Driven Construction
		public static Log Message(string text) => new Log(text, LogKind.Message, 2);
		public static Log Info(string text) => new Log(text, LogKind.Info, 2);
		public static Log Warning(string text) => new Log(text, LogKind.Warning, 2);
		public static Log Error(string text) => new Log(text, LogKind.Error, 2);
		public static Log Fatal(string text) => new Log(text, LogKind.Fatal, 2);
		internal static Log ProcessStart(string text) => new Log(text, LogKind.ProcessStart, 3);
		internal static Log ProcessFinish(string text) => new Log(text, LogKind.ProcessFinish, 3);
		#endregion

		private Log(string text, LogKind kind, byte frameIndex)
		{
			StackTrace stackTrace = new StackTrace();

			Text = text;
			Kind = kind;
			OriginMethod = stackTrace.GetFrame(frameIndex)?.GetMethod();
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;
		}

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
