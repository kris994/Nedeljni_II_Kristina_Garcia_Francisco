using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Doctow View
    /// </summary>
    class DoctorViewModel : BaseViewModel
    {
        /// <summary>
        /// Doctor data
        /// </summary>
        DoctorData docData = new DoctorData();
        /// <summary>
        /// Doctor window
        /// </summary>
        DoctorWindow docWindow;

        #region Constructor
        /// <summary>
        /// Constructor with the doctor info window opening
        /// </summary>
        /// <param name="doctorWindowOpen">opends the doctor window</param>
        public DoctorViewModel(DoctorWindow doctorWindowOpen)
        {
            docWindow = doctorWindowOpen;
            DoctorPatientList = docData.GetAllPatientsFromDoctors(LoggedInUser.CurrentUser.UserID).ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of doctor
        /// </summary>
        private List<vwClinicPatient> doctorPatientList;
        public List<vwClinicPatient> DoctorPatientList
        {
            get
            {
                return doctorPatientList;
            }
            set
            {
                doctorPatientList = value;
                OnPropertyChanged("DoctorPatientList");
            }
        }

        /// <summary>
        /// Info label
        /// </summary>
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }

        /// <summary>
        /// Info label background
        /// </summary>
        private string infoLabelBG;
        public string InfoLabelBG
        {
            get
            {
                return infoLabelBG;
            }
            set
            {
                infoLabelBG = value;
                OnPropertyChanged("InfoLabelBG");
            }
        }
        #endregion

        #region Commands      
        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                docWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }
        #endregion
    }
}
