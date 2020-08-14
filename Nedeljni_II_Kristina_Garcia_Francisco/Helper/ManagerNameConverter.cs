using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Gets the name and last name of the manager for the given id
    /// </summary>
    class ManagerNameConverter : IValueConverter
    {
        /// <summary>
        /// Convers the manager id to his name and last name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ManagerData manData = new ManagerData();

            if (value != null)
            {
                for (int i = 0; i < manData.GetAllManagers().Count; i++)
                {
                    if (manData.GetAllManagers()[i].ManagerID == (int)value)
                    {
                        return manData.GetAllManagers()[i].FirstName + " " + manData.GetAllManagers()[i].LastName;
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
