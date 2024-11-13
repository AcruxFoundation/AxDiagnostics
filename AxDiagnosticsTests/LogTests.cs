using Microsoft.VisualStudio.TestTools.UnitTesting;
using AxDiagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AxDiagnostics.Tests
{
	[TestClass()]
	public class LogTests
	{
		[TestMethod()]
		public void LogTest()
		{
			Log log = new Log("Testing!", LogKind.Info, MethodInfo.GetCurrentMethod());
			log.Display();
		}

		[TestMethod()]
		public void LogGroupTest()
		{
			LogGroup group = new LogGroup("Testing Group");
			for(int i = 0; i < 10; ++i)
			{
				group.AddLog(new Log($"Log #{i + 1}!", LogKind.Message, MethodInfo.GetCurrentMethod()));
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
					group.AddLog(new Log($"Log #{j + 1}!", LogKind.Message, MethodInfo.GetCurrentMethod()));
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
						group.AddLog(new Log($"Log #{j + 1}!", LogKind.Message, MethodInfo.GetCurrentMethod()));
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
	}
}