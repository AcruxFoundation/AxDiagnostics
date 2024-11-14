using Microsoft.VisualStudio.TestTools.UnitTesting;
using AxDiagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace AxDiagnostics.Tests
{
	[TestClass()]
	public class LogTests
	{
		[TestMethod()]
		public void LogTest()
		{
			Log log = new Log("Testing!", LogSeverity.Info, MethodInfo.GetCurrentMethod());
			log.Display();
		}

		[TestMethod()]
		public void LogGroupTest()
		{
			LogGroup group = new LogGroup("Testing Group");
			for(int i = 0; i < 10; ++i)
			{
				group.AddLog(new Log($"Log #{i + 1}!", LogSeverity.Message, MethodInfo.GetCurrentMethod()));
			}
			group.Display();
		}

		[TestMethod()]
		public void DebugSectionTest()
		{
			DebugSection section = new("Testing Section");

			for (int i = 0; i < 3; ++i)
			{
				LogGroup group = new LogGroup($"Testing Group #{i+1}");
				for (int j = 0; j < 10; ++j)
				{
					group.AddLog(new Log($"Log #{j + 1}!", LogSeverity.Message, MethodInfo.GetCurrentMethod()));
				}
				section.AddGroup(group);
			}
			section.Display();
		}

		[TestMethod()]
		public void DebugClassTest()
		{
			for(int k = 0; k < 2; ++k)
			{
				DebugSection section = new($"Testing Section #{k+1}");
				for (int i = 0; i < 3; ++i)
				{
					LogGroup group = new LogGroup($"Testing Group #{i + 1}");
					for (int j = 0; j < 10; ++j)
					{
						group.AddLog(new Log($"Log #{j + 1}!", LogSeverity.Message, MethodInfo.GetCurrentMethod()));
					}
					section.AddGroup(group);
				}
				Debug.AddSection(section);
			}
			Debug.Display();
		}

		[TestMethod()]
		public void DebugTest01()
		{
			DebugSection moduleDebugSection = Debug.GetOrCreateSection("Module");
			LogGroup logGroup = moduleDebugSection.GetGroupForThread();

			using (var process = new DebugProcess(logGroup))
			{
				process.Log(Log.Message("Testing!"));
				using (var innerProcess = new DebugProcess(logGroup))
				{
					process.Log(Log.Info("Reached inner process."));
				}
			}
		}

		[TestMethod()]
		public void LogSerializationTest()
		{
			Log log1 = Log.Info("This should be serialized correctly.");
			string json1 = JsonConvert.SerializeObject(log1, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine(json1);

			Log? log2 = JsonConvert.DeserializeObject<Log>(json1);
			string json2 = JsonConvert.SerializeObject(log2, Formatting.Indented);
			Assert.AreEqual(json1, json2);
		}

		[TestMethod()]
		public void LogGroupSerializationTest()
		{
			LogGroup group = new LogGroup("Testing Group");
			for (int i = 0; i < 10; ++i)
			{
				group.AddLog(new Log($"Log #{i + 1}!", LogSeverity.Message, MethodInfo.GetCurrentMethod()));
			}
			string json = JsonConvert.SerializeObject(group, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine(json);

			LogGroup? group2 = JsonConvert.DeserializeObject<LogGroup>(json);
			string json2 = JsonConvert.SerializeObject(group2, Formatting.Indented);

			Assert.AreEqual(json, json2);
		}

		[TestMethod()]
		public void DebugSectionSerializationTest()
		{
			DebugSection section = new("Testing Section");

			for (int i = 0; i < 3; ++i)
			{
				LogGroup group = new LogGroup($"Testing Group #{i + 1}");
				for (int j = 0; j < 10; ++j)
				{
					group.AddLog(new Log($"Log #{j + 1}!", LogSeverity.Message, MethodInfo.GetCurrentMethod()));
				}
				section.AddGroup(group);
			}
			string json = JsonConvert.SerializeObject(section, Formatting.Indented);
			System.Diagnostics.Debug.WriteLine(json);

			DebugSection? section2 = JsonConvert.DeserializeObject<DebugSection?>(json);
			string json2 = JsonConvert.SerializeObject(section2, Formatting.Indented);
			Assert.AreEqual(json, json2);
		}
	}
}