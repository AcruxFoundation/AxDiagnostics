﻿using AxDiagnostics;

DebugSection section = Debug.GetOrCreateSection("Console App");
LogGroup group = section.GetGroupForThread();

using(var process1 = new DebugProcess(group))
{
	process1.Log(Log.Message("Message!"));
	using (var process2 = new DebugProcess(group))
	{
		process1.Log(Log.Info("Message!"));
		process1.Log(Log.Warning("Message!"));
		process1.Log(Log.Error("Message!"));
		process1.Log(Log.Fatal("Message!"));
	}
}

Console.ReadKey();