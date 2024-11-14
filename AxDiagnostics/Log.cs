using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace AxDiagnostics
{
	/// <summary>
	/// Represents a message emitted by an event or system when certain actions are given.
	/// </summary>
	public class Log
	{
		[JsonProperty]
		public string Text { get; private set; }
		[JsonProperty]
		public LogSeverity Severity { get; private set; }
		[JsonProperty]
		public DateTime CreationDate { get; private set; }
		private Thread OriginThread { get; }
		[JsonProperty]
		public string? OriginThreadName { get; private set; }
		[JsonProperty]
		public string? OriginMethodName { get; private set; }
		[JsonProperty]
		public string? OriginTypeName { get; private set; }
		[JsonProperty]
		public string? OriginTypeFQN { get; private set; }
		public string? Origin => $"{OriginTypeFQN}.{OriginTypeFQN}";
		public byte Indentation { get; set; }

		public void Display()
		{
			ConsoleColor originalColor = Console.ForegroundColor;
			Console.ForegroundColor = LogSeverityColors.GetColorBySeverity(Severity);

			System.Diagnostics.Debug.WriteLine(this);
			Console.WriteLine(this);

			Console.ForegroundColor = originalColor;
		}

		public override string ToString()
		{
			string indentationString = new string('#', Indentation).Replace("#","|\t");
			return $"[{CreationDate}]\t{indentationString}[{Severity}] ({(OriginThreadName == null ? "" : $"{OriginThreadName} @ ")}{OriginTypeFQN}.{OriginMethodName}) : {Text}";
		}

		#region Kind Driven Construction
		/// <summary>
		/// Creates a new <see cref="Log"/> instance with severity <see cref="LogSeverity.Message"/>.
		/// </summary>
		/// <param name="text">The text of the new <see cref="Log"/> instance.</param>
		/// <returns>The new log instance.</returns>
		public static Log Message(string text) => new Log(text, LogSeverity.Message, 2);

		/// <summary>
		/// Creates a new <see cref="Log"/> instance with severity <see cref="LogSeverity.Info"/>.
		/// </summary>
		/// <param name="text">The text of the new <see cref="Log"/> instance.</param>
		/// <returns>The new log instance.</returns>
		public static Log Info(string text) => new Log(text, LogSeverity.Info, 2);

		/// <summary>
		/// Creates a new <see cref="Log"/> instance with severity <see cref="LogSeverity.Warning"/>.
		/// </summary>
		/// <param name="text">The text of the new <see cref="Log"/> instance.</param>
		/// <returns>The new log instance.</returns>
		public static Log Warning(string text) => new Log(text, LogSeverity.Warning, 2);

		/// <summary>
		/// Creates a new <see cref="Log"/> instance with severity <see cref="LogSeverity.Error"/>.
		/// </summary>
		/// <param name="text">The text of the new <see cref="Log"/> instance.</param>
		/// <returns>The new log instance.</returns>
		public static Log Error(string text) => new Log(text, LogSeverity.Error, 2);

		/// <summary>
		/// Creates a new <see cref="Log"/> instance with severity <see cref="LogSeverity.Fatal"/>.
		/// </summary>
		/// <param name="text">The text of the new <see cref="Log"/> instance.</param>
		/// <returns>The new log instance.</returns>
		public static Log Fatal(string text) => new Log(text, LogSeverity.Fatal, 2);
		internal static Log ProcessStart(string text) => new Log(text, LogSeverity.ProcessStart, 3);
		internal static Log ProcessFinish(string text) => new Log(text, LogSeverity.ProcessFinish, 3);
		#endregion

		private Log(string text, LogSeverity kind, byte frameIndex)
		{
			StackTrace stackTrace = new StackTrace();
			MethodBase? originMethod = stackTrace.GetFrame(frameIndex)?.GetMethod();
			Text = text;
			Severity = kind;
			
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;

			OriginThreadName = OriginThread.Name;
			OriginMethodName = originMethod?.Name;
			OriginTypeName = originMethod?.DeclaringType?.Name;
			OriginTypeFQN = originMethod?.DeclaringType?.FullName;
		}

		public Log(string text, LogSeverity kind)
		{
			StackTrace stackTrace = new StackTrace();
			MethodBase? originMethod = stackTrace.GetFrame(1)?.GetMethod();

			Text = text;
			Severity = kind;
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;

			OriginThreadName = OriginThread.Name;
			OriginMethodName = originMethod?.Name;
			OriginTypeName = originMethod?.DeclaringType?.Name;
			OriginTypeFQN = originMethod?.DeclaringType?.FullName;
		}

		public Log(string text, LogSeverity kind, MethodBase? originMethod, Thread? originThread = null)
		{
			Text = text;
			Severity = kind;
			OriginThread ??= Thread.CurrentThread;
			CreationDate = DateTime.Now;

			OriginThreadName = OriginThread.Name;
			OriginMethodName = originMethod?.Name;
			OriginTypeName = originMethod?.DeclaringType?.Name;
			OriginTypeFQN = originMethod?.DeclaringType?.FullName;
		}

		[JsonConstructor]
		private Log()
		{

		}
	}
}
