using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class ReportStatisticsViewModel : BaseViewModel
    {
        UserData userData = new UserData();
        PatientData patData = new PatientData();
        HealthExam he = new HealthExam();
        ReportStatisticsWindow reportWindow;

        #region Constructor
        /// <summary>
        /// Constructor with the report info window opening
        /// </summary>
        /// <param name="reportWindowOpen">opends the report window</param>
        public ReportStatisticsViewModel(ReportStatisticsWindow reportWindowOpen)
        {
            reportWindow = reportWindowOpen;
            TotalEmployees = userData.CountEmployees().ToString();
            TotalPatients = patData.CountPatients().ToString();
            AverageAge = he.AverageSickPatientsAge().ToString();
        }
        #endregion

        #region Property
        /// <summary>
        /// Employee number label
        /// </summary>
        private string totalEmployees;
        public string TotalEmployees
        {
            get
            {
                return totalEmployees;
            }
            set
            {
                totalEmployees = value;
                OnPropertyChanged("TotalEmployees");
            }
        }

        /// <summary>
        /// Patients number label
        /// </summary>
        private string totalPatients;
        public string TotalPatients
        {
            get
            {
                return totalPatients;
            }
            set
            {
                totalPatients = value;
                OnPropertyChanged("TotalPatients");
            }
        }

        /// <summary>
        /// Average age label
        /// </summary>
        private string averageAge;
        public string AverageAge
        {
            get
            {
                return averageAge;
            }
            set
            {
                averageAge = value;
                OnPropertyChanged("AverageAge");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that closes the window
        /// </summary>
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                reportWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }
}
