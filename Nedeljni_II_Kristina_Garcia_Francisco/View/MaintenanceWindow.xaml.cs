using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for MaintenanceWindow.xaml
    /// </summary>
    public partial class MaintenanceWindow : Window
    {
        /// <summary>
        /// Maintenance window
        /// </summary>
        public MaintenanceWindow()
        {
            InitializeComponent();
            this.DataContext = new MaintenanceViewModel(this);
        }

        /// <summary>
        /// Closes the Window and opens the Login window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
