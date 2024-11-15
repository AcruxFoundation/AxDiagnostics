using AxDiagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AxDiagnosticsInspector
{
	public static class AppHandler
	{
		private static MainWindow Window { get; }
		private static InspectorPage? InspectorPage { get; set; }
		public static void DisplayOpenDiagnosticFilePage()
		{
			SetWindowPage(new OpenDiagnosticFilePage());
		}

		public static void DisplayInspector(string json)
		{
			Debug.Deserialize(json);

			if (InspectorPage == null) InspectorPage = new InspectorPage();
			SetWindowPage(InspectorPage);
		}

		private static void SetWindowPage(Page page)
		{
			Window.Frame.Content = page;
		}

		static AppHandler()
		{
			Window = MainWindow.Instance ?? throw new InvalidOperationException($"{nameof(AppHandler)} static constructor was called before {nameof(MainWindow)} was constructed.");
		}
	}
}
