using AxDiagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AxDiagnosticsInspector
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static MainWindow? Instance { get; private set; }
		public MainWindow()
		{
			Instance = this;
			InitializeComponent();
			Loaded += Load;
		}

		private void Load(object sender, RoutedEventArgs e)
		{
			Frame.Content = new OpenDiagnosticFilePage();
		}
	}
}