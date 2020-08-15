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
    /// <summary>
    /// Adds or edits a patient
    /// </summary>
    class AddPatientViewModel : BaseViewModel
    {
        /// <summary>
        /// Add patient window
        /// </summary>
        AddPatientWindow addPatient;
        /// <summary>
        /// Patient data
        /// </summary>
        PatientData patientData = new PatientData();
        /// <summary>
        /// Doctor data
        /// </summary>
        DoctorData doctorData = new DoctorData();

        #region Constructor
        /// <summary>
        /// Constructor with the add patient info window opening
        /// </summary>
        /// <param name="addPatientWindowOpen">opends the add patient window</param>
        public AddPatientViewModel(AddPatientWindow addPatientWindowOpen)
        {
            patient = new vwClinicPatient();
            addPatient = addPatientWindowOpen;
            PatientList = patientData.GetAllPatients().ToList();
            DoctorList = doctorData.GetAllDoctors().ToList();
        }

        /// <summary>
        /// Constructor with edit patient window opening
        /// </summary>
        /// <param name="addPatientWindowOpen">opens the edit patient window</param>
        /// <param name="patientEdit">gets the patient info that is being edited</param>
        public AddPatientViewModel(AddPatientWindow addPatientWindowOpen, vwClinicPatient patientEdit)
        {
            patient = patientEdit;
            addPatient = addPatientWindowOpen;
            PatientList = patientData.GetAllPatients().ToList();
            DoctorList = doctorData.GetAllDoctors().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of patient
        /// </summary>
        private List<vwClinicPatient> patientList;
        public List<vwClinicPatient> PatientList
        {
            get
            {
                return patientList;
            }
            set
            {
                patientList = value;
                OnPropertyChanged("PatientList");
            }
        }

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
        /// Specific Patient
        /// </summary>
        private vwClinicPatient patient;
        public vwClinicPatient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
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
        /// Checks if the patient data was changed
        /// </summary>
        private bool isUpdatePatient;
        public bool IsUpdatePatient
        {
            get
            {
                return isUpdatePatient;
            }
            set
            {
                isUpdatePatient = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new patient
        /// </summary>
        private ICommand savePatient;
        public ICommand SavePatient
        {
            get
            {
                if (savePatient == null)
                {
                    savePatient = new RelayCommand(param => SavePatientExecute(), param => this.CanSavePatientExecute);
                }
                return savePatient;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SavePatientExecute()
        {
            var result = MessageBox.Show("Are you sure you want to save this patient?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Doctor != null)
                    {
                        Patient.UniqueNumber = Doctor.UniqueNumber;
                    }

                    patientData.AddPatient(Patient);
                    IsUpdatePatient = true;

                    addPatient.Close();
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
        /// Checks if its possible to save the patient
        /// </summary>
        protected bool CanSavePatientExecute
        {
            get
            {
                return Patient.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the window
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
                addPatient.Close();
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
