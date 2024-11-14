using AxDiagnostics;

Debug.DiagnosticsReportFileDestination = "report.axd";
DebugSection section = Debug.GetOrCreateSection("Console App");
LogGroup group = section.GetGroupForThread();

using(var process1 = new DebugProcess(group))
{
	process1.Log(Log.Message("Message!"));
	using (var process2 = new DebugProcess(group))
	{
		process2.Log(Log.Message("Message!"));
		process2.Log(Log.Message("Message!"));
		process2.Log(Log.Message("Message!"));
		process2.Log(Log.Message("Message!"));
		process2.Log(Log.Info("Message!"));
		process2.Log(Log.Warning("Message!"));
		process2.Log(Log.Message("Message!"));
		process2.Log(Log.Message("Message!"));
		process2.Log(Log.Error("Message!"));
		process2.Log(Log.Fatal("Message!"));
	}
}
Debug.SaveToFile();