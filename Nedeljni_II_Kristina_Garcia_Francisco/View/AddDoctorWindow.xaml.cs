using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddDoctorWindow.xaml
    /// </summary>
    public partial class AddDoctorWindow : Window
    {
        public AddDoctorWindow()
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModel(this);
        }

        /// <summary>
        /// Window constructor for editing doctor
        /// </summary>
        /// <param name="doctorEdit">doctor that is bing edited</param>
        public AddDoctorWindow(vwClinicDoctor doctorEdit)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModel(this, doctorEdit);
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
