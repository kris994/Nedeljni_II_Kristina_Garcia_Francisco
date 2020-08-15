﻿using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for EditClinicWindow.xaml
    /// </summary>
    public partial class EditClinicWindow : Window
    {
        /// <summary>
        /// Edit clinic window
        /// </summary>
        /// <param name="clinicEdit"></param>
        public EditClinicWindow(tblClinic clinicEdit)
        {
            InitializeComponent();
            this.DataContext = new ClinicViewModel(this, clinicEdit);
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
