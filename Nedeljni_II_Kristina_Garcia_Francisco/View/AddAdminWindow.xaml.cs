using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddAdmin.xaml
    /// </summary>
    public partial class AddAdminWindow : Window
    {
        /// <summary>
        /// AddAdmin Window
        /// </summary>
        public AddAdminWindow()
        {
            InitializeComponent();
            this.DataContext = new AddAdminViewModel(this);
        }

        /// <summary>
        /// Window constructor for editing admin
        /// </summary>
        /// <param name="adminEdit">admin that is bing edited</param>
        public AddAdminWindow(vwClinicAdministrator adminEdit)
        {
            InitializeComponent();
            this.DataContext = new AddAdminViewModel(this, adminEdit);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
