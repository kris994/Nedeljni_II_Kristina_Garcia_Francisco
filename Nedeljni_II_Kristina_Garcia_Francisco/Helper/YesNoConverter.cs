using System;
using System.Globalization;
using System.Windows.Data;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Convertes the value to yes or no
    /// </summary>
    class YesNoConverter : IValueConverter
    {
        /// <summary>
        /// Convers the bool value to yes or no
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }
        }

        /// <summary>
        /// Returns the selected value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}


