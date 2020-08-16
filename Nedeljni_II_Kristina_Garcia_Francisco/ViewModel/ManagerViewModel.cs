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
    /// Manager view model
    /// </summary>
    class ManagerViewModel : BaseViewModel
    {
        /// <summary>
        /// Doctor data
        /// </summary>
        DoctorData docData = new DoctorData();
        /// <summary>
        /// Manager data
        /// </summary>
        ManagerData managerData = new ManagerData();
        /// <summary>
        /// Manager window
        /// </summary>
        ManagerWindow managerWindow;

        #region Constructor
        /// <summary>
        /// Constructor with the manager info window opening
        /// </summary>
        /// <param name="managerWindowOpen">opends the manager window</param>
        public ManagerViewModel(ManagerWindow managerWindowOpen)
        {
            managerWindow = managerWindowOpen;
            DoctorList = docData.GetAllDoctors().ToList();
            ManagerList = managerData.GetAllManagers().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of doctor
        /// </summary>
        private List<vwClinicDoctor> doctorList;
        public List<vwClinicDoctor> DoctorList
        {
            get
            {
                return doctorList;
            }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }

        /// <summary>
        /// List of managers
        /// </summary>
        private List<vwClinicManager> managerList;
        public List<vwClinicManager> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }

        /// <summary>
        /// Specific Doctor
        /// </summary>
        private vwClinicDoctor doctor;
        public vwClinicDoctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
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
        /// Command that tries to add a new doctor
        /// </summary>
        private ICommand addNewDoctor;
        public ICommand AddNewDoctor
        {
            get
            {
                if (addNewDoctor == null)
                {
                    addNewDoctor = new RelayCommand(param => AddNewDoctorExecute(), param => CanAddNewDoctorExecute());
                }
                return addNewDoctor;
            }
        }

        /// <summary>
        /// Executes the add Doctor command
        /// </summary>
        private void AddNewDoctorExecute()
        {
            try
            {
                AddDoctorWindow addDoctor = new AddDoctorWindow();
                addDoctor.ShowDialog();
                if ((addDoctor.DataContext as AddDoctorViewModel).IsUpdateDoctor == true)
                {
                    DoctorList = docData.GetAllDoctors().ToList();
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully created a new Doctor";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new doctor
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewDoctorExecute()
        {
            return true;
        }
      
        /// <summary>
        /// Command that tries to open the edit doctor window
        /// </summary>
        private ICommand editDoctor;
        public ICommand EditDoctor
        {
            get
            {
                if (editDoctor == null)
                {
                    editDoctor = new RelayCommand(param => EditDoctorExecute(), param => CanEditDoctorExecute());
                }
                return editDoctor;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditDoctorExecute()
        {
            try
            {
                if (Doctor != null)
                {
                    AddDoctorWindow addDoctor = new AddDoctorWindow(Doctor);
                    addDoctor.ShowDialog();

                    if (DoctorData.isChanged == true)
                    {
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully updated a Doctor";
                        DoctorData.isChanged = false;
                    }

                    DoctorList = docData.GetAllDoctors().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the doctor can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditDoctorExecute()
        {
            if (DoctorList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

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
                managerWindow.Close();
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
