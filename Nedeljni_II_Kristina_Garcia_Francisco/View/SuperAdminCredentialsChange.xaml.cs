using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for SuperAdminCredentialsChange.xaml
    /// </summary>
    public partial class SuperAdminCredentialsChange : Window
    {
        /// <summary>
        /// Super admin credentials window
        /// </summary>
        public SuperAdminCredentialsChange()
        {
            InitializeComponent();
            this.DataContext = new SuperAdminViewModel(this);
        }
    }
}
