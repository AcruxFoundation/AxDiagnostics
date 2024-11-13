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
	}
}