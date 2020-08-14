using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this);
        }

        void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
