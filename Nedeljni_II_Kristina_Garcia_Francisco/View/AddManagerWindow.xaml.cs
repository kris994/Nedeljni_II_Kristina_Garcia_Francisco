using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddManagerWindow.xaml
    /// </summary>
    public partial class AddManagerWindow : Window
    {
        /// <summary>
        /// Add manager window
        /// </summary>
        public AddManagerWindow()
        {
            InitializeComponent();
            this.DataContext = new AddManagerViewModel(this);
        }

        /// <summary>
        /// Window constructor for editing manager
        /// </summary>
        /// <param name="managerEdit">manager that is bing edited</param>
        public AddManagerWindow(vwClinicManager managerEdit)
        {
            InitializeComponent();
            this.DataContext = new AddManagerViewModel(this, managerEdit);
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
