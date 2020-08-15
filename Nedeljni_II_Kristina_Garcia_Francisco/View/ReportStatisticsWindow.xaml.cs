using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for ReportStatisticsWindow.xaml
    /// </summary>
    public partial class ReportStatisticsWindow : Window
    {
        public ReportStatisticsWindow()
        {
            InitializeComponent();
            this.DataContext = new ReportStatisticsViewModel(this);
        }
    }
}
