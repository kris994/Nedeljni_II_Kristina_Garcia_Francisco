using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Gets the name and last name of the doctor for the given uniquenumber
    /// </summary>
    class DoctorNameConverter : IValueConverter
    {
        /// <summary>
        /// Convers the doctor uniquenumber to his name and last name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DoctorData docData = new DoctorData();

            if (value != null)
            {
                for (int i = 0; i < docData.GetAllDoctors().Count; i++)
                {
                    if (docData.GetAllDoctors()[i].UniqueNumber == (string)value)
                    {
                        return docData.GetAllDoctors()[i].FirstName + " " + docData.GetAllDoctors()[i].LastName;
                    }
                }
            }
            return value;
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
