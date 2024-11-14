using AxDiagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxDiagnosticsInspector
{
	public static class AppHandler
	{
		private static MainWindow Window { get; }
		public static void DisplayOpenDiagnosticFilePage()
		{
			Window.Frame.Content = new OpenDiagnosticFilePage();
		}

		public static void DisplayInspector(string json)
		{
			Debug.Deserialize(json);
		}

		static AppHandler()
		{
			Window = MainWindow.Instance ?? throw new InvalidOperationException($"{nameof(AppHandler)} static constructor was called before {nameof(MainWindow)} was constructed.");
		}
	}
}
