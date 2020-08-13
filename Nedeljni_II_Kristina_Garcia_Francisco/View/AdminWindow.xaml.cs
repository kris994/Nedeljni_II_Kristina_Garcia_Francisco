using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            this.DataContext = new ClinicViewModel(this);
        }

        void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
