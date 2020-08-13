using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddMaintenanceWindow.xaml
    /// </summary>
    public partial class AddMaintenanceWindow : Window
    {
        public AddMaintenanceWindow()
        {
            InitializeComponent();
            this.DataContext = new AddMaintenanceViewModel(this);
        }

        /// <summary>
        /// Window constructor for editing maintenance
        /// </summary>
        /// <param name="maintenanceEdit">maintenance that is bing edited</param>
        public AddMaintenanceWindow(vwClinicMaintenance maintenanceEdit)
        {
            InitializeComponent();
            this.DataContext = new AddMaintenanceViewModel(this, maintenanceEdit);
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
