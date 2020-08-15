using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        /// <summary>
        /// Add patient window
        /// </summary>
        public AddPatientWindow()
        {
            InitializeComponent();
            this.DataContext = new AddPatientViewModel(this);
        }

        /// <summary>
        /// Window constructor for editing patient
        /// </summary>
        /// <param name="patientEdit">patient that is bing edited</param>
        public AddPatientWindow(vwClinicPatient patientEdit)
        {
            InitializeComponent();
            this.DataContext = new AddPatientViewModel(this, patientEdit);
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
