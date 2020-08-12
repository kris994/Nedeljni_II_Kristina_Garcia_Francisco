using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for SuperAdminWindow.xaml
    /// </summary>
    public partial class SuperAdminWindow : Window
    {
        public SuperAdminWindow()
        {
            InitializeComponent();
            this.DataContext = new SuperAdminViewModel(this);
        }
    }
}
