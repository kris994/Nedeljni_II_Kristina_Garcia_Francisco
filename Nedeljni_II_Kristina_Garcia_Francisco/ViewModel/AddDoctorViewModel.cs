using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class AddDoctorViewModel : BaseViewModel
    {
        AddDoctorWindow addDoctor;
        DoctorData doctorData = new DoctorData();
        ManagerData managerData = new ManagerData();

        #region Constructor
        /// <summary>
        /// Constructor with the add doctor info window opening
        /// </summary>
        /// <param name="addDoctorWindowOpen">opends the add doctor window</param>
        public AddDoctorViewModel(AddDoctorWindow addDoctorWindowOpen)
        {
            doctor = new vwClinicDoctor();
            addDoctor = addDoctorWindowOpen;
            DoctorList = doctorData.GetAllDoctors().ToList();
            ManagerList = managerData.GetAllAvailableManagers().ToList();
        }

        /// <summary>
        /// Constructor with edit doctor window opening
        /// </summary>
        /// <param name="addDoctorWindowOpen">opens the edit doctor window</param>
        /// <param name=doctorEdit">gets the doctor info that is being edited</param>
        public AddDoctorViewModel(AddDoctorWindow addDoctorWindowOpen, vwClinicDoctor doctorEdit)
        {
            doctor = doctorEdit;
            addDoctor = addDoctorWindowOpen;
            DoctorList = doctorData.GetAllDoctors().ToList();
            ManagerList = managerData.GetAllAvailableManagers().ToList();
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
        /// Specific Manager
        /// </summary>
        private vwClinicManager manager;
        public vwClinicManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        /// <summary>
        /// Checks if its possible to execute the add and edit doctor commands
        /// </summary>
        private bool isUpdateDoctor;
        public bool IsUpdateDoctor
        {
            get
            {
                return isUpdateDoctor;
            }
            set
            {
                isUpdateDoctor = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new doctor
        /// </summary>
        private ICommand saveDoctor;
        public ICommand SaveDoctor
        {
            get
            {
                if (saveDoctor == null)
                {
                    saveDoctor = new RelayCommand(param => SaveDoctorExecute(), param => this.CanSaveDoctorExecute);
                }
                return saveDoctor;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveDoctorExecute()
        {
            var result = MessageBox.Show("Are you sure you want to create this doctor?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Manager != null)
                    {
                        Doctor.ManagerID = Manager.ManagerID;
                    }
                    
                    doctorData.AddDoctor(Doctor);
                    IsUpdateDoctor = true;

                    addDoctor.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception" + ex.Message.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if its possible to save the doctor
        /// </summary>
        protected bool CanSaveDoctorExecute
        {
            get
            {
                return Doctor.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add doctor or edit doctor window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addDoctor.Close();
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
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}