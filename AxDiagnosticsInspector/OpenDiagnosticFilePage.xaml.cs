using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AxDiagnosticsInspector
{
	/// <summary>
	/// Lógica de interacción para OpenDiagnosticFilePage.xaml
	/// </summary>
	public partial class OpenDiagnosticFilePage : Page
	{
		private string? FilePath { get; set; }
		private string? Json { get; set; }
		public OpenDiagnosticFilePage()
		{
			InitializeComponent();
		}

		private void BtnPickFile_Click(object sender, RoutedEventArgs eventArgs)
		{
			LblStatus.Content = null;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() ?? false)
			{
				FilePath = openFileDialog.FileName;
				Json = File.ReadAllText(FilePath);
				TxbFilepath.Text = FilePath;
			}
		}

		private void BtnDone_Click(object sender, RoutedEventArgs eventArgs)
		{
			if(Json == null)
			{
				LblStatus.Content = "ERROR: Must provide a file before loading.";
				return;
			}

			try
			{
				LblStatus.Content = $"Loading diagnostics file: '{Path.GetFileName(FilePath)}'.";
				AppHandler.DisplayInspector(Json);
			}
			catch (Exception e)
			{
				LblStatus.Content = $"ERROR: {e.Message}";
			}
		}
	}
}
