using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for SuperAdminWindow.xaml
    /// </summary>
    public partial class SuperAdminWindow : Window
    {
        /// <summary>
        /// Super admin window
        /// </summary>
        public SuperAdminWindow()
        {
            InitializeComponent();
            this.DataContext = new SuperAdminViewModel(this);
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
