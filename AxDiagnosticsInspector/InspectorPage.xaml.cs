using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Lógica de interacción para InspectorPage.xaml
    /// </summary>
    public partial class InspectorPage : Page
    {
        public InspectorPage()
        {
            InitializeComponent();
        }

		private void TracksScrollChanged(object sender, ScrollChangedEventArgs e)
		{
            SvDatesScroll.ScrollToVerticalOffset(SvTracksScroll.VerticalOffset);
        }

		private void PageSizeChanged(object sender, SizeChangedEventArgs e)
		{
            ScrollBarVisibility visibility;
            if (SvTracksScroll.ComputedHorizontalScrollBarVisibility == Visibility.Visible) visibility = ScrollBarVisibility.Visible;
            else visibility = ScrollBarVisibility.Hidden;
			SvDatesScroll.HorizontalScrollBarVisibility = visibility;
		}
	}
}
