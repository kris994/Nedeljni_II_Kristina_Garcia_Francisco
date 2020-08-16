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
    /// Holds all the information needed to check on users in the clinic
    /// </summary>
    class ClinicViewModel : BaseViewModel
    {
        #region Access Data and Window
        /// <summary>
        /// Window for adding clinc
        /// </summary>
        AddClinicWindow addClinic;
        /// <summary>
        /// Admin window
        /// </summary>
        AdminWindow adminWidnow;
        /// <summary>
        /// Window for editing the clinic
        /// </summary>
        EditClinicWindow editClinic;
        /// <summary>
        /// Clinic Data
        /// </summary>
        ClinicData clinicData = new ClinicData();
        /// <summary>
        /// Manager Data
        /// </summary>
        ManagerData managerData = new ManagerData();
        /// <summary>
        /// Admin Data
        /// </summary>
        AdminData adminData = new AdminData();
        /// <summary>
        /// Doctor data
        /// </summary>
        DoctorData docData = new DoctorData();
        /// <summary>
        /// Patient data
        /// </summary>
        PatientData patData = new PatientData();
        /// <summary>
        /// Maintenance data
        /// </summary>
        MaintenanceData maintenanceData = new MaintenanceData();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor with the add clinic info window opening
        /// </summary>
        /// <param name="addClinicWindowOpen">opends the add clinic window</param>
        public ClinicViewModel(AddClinicWindow addClinicWindowOpen)
        {
            clinic = new tblClinic ();
            addClinic = addClinicWindowOpen;
            ClinicList = clinicData.GetAllClinics().ToList();
            MaintenanceList = maintenanceData.GetAllMaintenances();
        }

        /// <summary>
        /// Constructor with the admin info window opening
        /// </summary>
        /// <param name="adminWindowOpen">opends the admin window</param>
        public ClinicViewModel(AdminWindow adminWindowOpen)
        {
            adminWidnow = adminWindowOpen;
            ClinicList = clinicData.GetAllClinics().ToList();
            ManagerList = managerData.GetAllManagers().ToList();
            MaintenanceList = maintenanceData.GetAllMaintenances();
            AdminList = adminData.GetAllAdmins().ToList();
            DoctorList = docData.GetAllDoctors().ToList();
            PatientList = patData.GetAllPatients().ToList();

            if (isNewClinic == true)
            {
                InfoLabelBG = "#28a745";
                InfoLabel = "Successfully created the Clinic";
                isNewClinic = false;
            }
        }

        /// <summary>
        /// Constructor with edit clinic window opening
        /// </summary>
        /// <param name="editClinicOpen">opens the edit clinic window</param>
        /// <param name="clinicEdit">gets the clinic info that is being edited</param>
        public ClinicViewModel(EditClinicWindow editClinicOpen, tblClinic clinicEdit)
        {
            clinic = clinicEdit;
            editClinic = editClinicOpen;
            ClinicList = clinicData.GetAllClinics().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of clinics
        /// </summary>
        private List<tblClinic> clinicList;
        public List<tblClinic> ClinicList
        {
            get
            {
                return clinicList;
            }
            set
            {
                clinicList = value;
                OnPropertyChanged("ClinicList");
            }
        }

        /// <summary>
        /// List of admins
        /// </summary>
        private List<vwClinicAdministrator> adminList;
        public List<vwClinicAdministrator> AdminList
        {
            get
            {
                return adminList;
            }
            set
            {
                adminList = value;
                OnPropertyChanged("AdminList");
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
        /// List of maintenance
        /// </summary>
        private Queue<vwClinicMaintenance> maintenanceList;
        public Queue<vwClinicMaintenance> MaintenanceList
        {
            get
            {
                return maintenanceList;
            }
            set
            {
                maintenanceList = value;
                OnPropertyChanged("MaintenanceList");
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
        /// Specific Clinic
        /// </summary>
        private tblClinic clinic;
        public tblClinic Clinic
        {
            get
            {
                return clinic;
            }
            set
            {
                clinic = value;
                OnPropertyChanged("Clinic");
            }
        }

        /// <summary>
        /// Specific Maintenence
        /// </summary>
        private vwClinicMaintenance maintenance;
        public vwClinicMaintenance Maintenance
        {
            get
            {
                return maintenance;
            }
            set
            {
                maintenance = value;
                OnPropertyChanged("Maintenance");
            }
        }

        /// <summary>
        /// Specific manager
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
        /// <summary>
        /// Checks if clinic was just created
        /// </summary>
        private static bool isNewClinic = false;

        /// <summary>
        /// Checks if clinic was updated
        /// </summary>
        private static bool isUpdateClinic = false;
        #endregion

        #region Commands
        /// <summary>
        /// Command that edits existing clinic
        /// </summary>
        private ICommand editExistingClinic;
        public ICommand EditExistingClinic
        {
            get
            {
                if (editExistingClinic == null)
                {
                    editExistingClinic = new RelayCommand(param => EditExistingClinicExecute(), param => CanEditExistingClinicExecute());
                }
                return editExistingClinic;
            }
        }

        /// <summary>
        /// Executes the edit clinic command
        /// </summary>
        private void EditExistingClinicExecute()
        {
            try
            {
                EditClinicWindow editClinicWindow = new EditClinicWindow(ClinicList[0]);
                editClinicWindow.ShowDialog();
                if (isUpdateClinic == true)
                {
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully updated the Clinic";
                }
                ClinicList = clinicData.GetAllClinics().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new admin
        /// </summary>
        /// <returns>true</returns>
        private bool CanEditExistingClinicExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to save the new clinic
        /// </summary>
        private ICommand saveClinic;
        public ICommand SaveClinic
        {
            get
            {
                if (saveClinic == null)
                {
                    saveClinic = new RelayCommand(param => SaveClinicExecute(), param => CanSaveClinicExecute());
                }
                return saveClinic;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveClinicExecute()
        {
            var result = MessageBox.Show("Are you sure you want to save this clinic?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    clinicData.AddClinic(Clinic);
                    isNewClinic = true;

                    AdminWindow adminWindow = new AdminWindow();
                    addClinic.Close();
                    adminWindow.Show();
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
        /// Checks if its possible to save the clinic
        /// </summary>
        protected bool CanSaveClinicExecute()
        {
            if (Clinic.ClinicFloorNumber == 0 || Clinic.RoomsPerFloor == 0
                || Clinic.InvalidVehicleParkingLoots == 0 || Clinic.EmergencyVehicleParkingLoots == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to save the edit clinic
        /// </summary>
        private ICommand saveClinicEdit;
        public ICommand SaveClinicEdit
        {
            get
            {
                if (saveClinicEdit == null)
                {
                    saveClinicEdit = new RelayCommand(param => SaveClinicEditExecute(), param => this.CanSaveClinicEditxecute);
                }
                return saveClinicEdit;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveClinicEditExecute()
        {
            var result = MessageBox.Show("Are you sure you want to edit this clinic?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    isUpdateClinic = true;
                    clinicData.AddClinic(Clinic);
                    editClinic.Close();
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
        /// Checks if its possible to save the clinic
        /// </summary>
        protected bool CanSaveClinicEditxecute
        {
            get
            {
                return Clinic.IsValid;
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
                if (Application.Current.Windows.OfType<EditClinicWindow>().FirstOrDefault() != null)
                {
                    editClinic.Close();
                }
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

        /// <summary>
        /// Command that edits admin
        /// </summary>
        private ICommand editAdmin;
        public ICommand EditAdmin
        {
            get
            {
                if (editAdmin == null)
                {
                    editAdmin = new RelayCommand(param => EditAdminExecute(), param => CanEditEditAdminExecute());
                }
                return editAdmin;
            }
        }

        /// <summary>
        /// Executes the edit admin command
        /// </summary>
        private void EditAdminExecute()
        {
            try
            {
                AddAdminWindow addAdmin = new AddAdminWindow(AdminList[0]);
                addAdmin.ShowDialog();
                if (AdminData.isChanged == true)
                {
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully updated the Admin";
                }
                AdminList = adminData.GetAllAdmins().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new admin
        /// </summary>
        /// <returns>true</returns>
        private bool CanEditEditAdminExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to add a new maintenance
        /// </summary>
        private ICommand addNewMaintenance;
        public ICommand AddNewMaintenance
        {
            get
            {
                if (addNewMaintenance == null)
                {
                    addNewMaintenance = new RelayCommand(param => AddNewMaintenanceExecute(), param => CanAddNewMaintenanceExecute());
                }
                return addNewMaintenance;
            }
        }

        /// <summary>
        /// Executes the add Maintenance command
        /// </summary>
        private void AddNewMaintenanceExecute()
        {
            try
            {
                AddMaintenanceWindow addMaintenance = new AddMaintenanceWindow();
                addMaintenance.ShowDialog();
                if ((addMaintenance.DataContext as AddMaintenanceViewModel).IsUpdateMaintenance == true)
                {
                    MaintenanceList = maintenanceData.GetAllMaintenances();

                    if (MaintenanceList.Count < 4)
                    {
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully created a new Maintenance Client";
                    }
                    else
                    {
                        maintenanceData.QueueSize(MaintenanceList, 3);
                        MaintenanceList.Dequeue();                    
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully created a new Maintenance and deleted old. (Exceeded max number of 3)";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new maintenance
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewMaintenanceExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to delete the Maintenance
        /// </summary>
        private ICommand deleteMaintenance;
        public ICommand DeleteMaintenance
        {
            get
            {
                if (deleteMaintenance == null)
                {
                    deleteMaintenance = new RelayCommand(param => DeleteMaintenanceExecute(), param => CanDeleteMaintenanceExecute());
                }
                return deleteMaintenance;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteMaintenanceExecute()
        {
            // Checks if the user really wants to delete the user
            var result = MessageBox.Show("Are you sure you want to delete the maintenance?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Maintenance != null)
                    {
                        int userID = Maintenance.UserID;
                        maintenanceData.DeleteMaintenance(userID);
                        MaintenanceList = maintenanceData.GetAllMaintenances();

                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully deleted a Maintenance";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the maintenance can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteMaintenanceExecute()
        {
            if (MaintenanceList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit maintenance window
        /// </summary>
        private ICommand editMaintenance;
        public ICommand EditMaintenance
        {
            get
            {
                if (editMaintenance == null)
                {
                    editMaintenance = new RelayCommand(param => EditMaintenanceExecute(), param => CanEditMaintenanceExecute());
                }
                return editMaintenance;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditMaintenanceExecute()
        {
            try
            {
                if (Maintenance != null)
                {
                    AddMaintenanceWindow addMaintenenece = new AddMaintenanceWindow(Maintenance);
                    addMaintenenece.ShowDialog();

                    if (MaintenanceData.isChanged == true)
                    {
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully updated a Maintenance";
                        MaintenanceData.isChanged = false;
                    }

                    MaintenanceList = maintenanceData.GetAllMaintenances();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the maintenance can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditMaintenanceExecute()
        {
            if (MaintenanceList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to add a new manager
        /// </summary>
        private ICommand addNewManager;
        public ICommand AddNewManager
        {
            get
            {
                if (addNewManager == null)
                {
                    addNewManager = new RelayCommand(param => AddNewManagerExecute(), param => CanAddNewManagerExecute());
                }
                return addNewManager;
            }
        }

        /// <summary>
        /// Executes the add Manager command
        /// </summary>
        private void AddNewManagerExecute()
        {
            try
            {
                AddManagerWindow addManager = new AddManagerWindow();
                addManager.ShowDialog();
                if ((addManager.DataContext as AddManagerViewModel).IsUpdateManager == true)
                {
                    ManagerList = managerData.GetAllManagers().ToList();
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully created a new Manager";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new manager
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewManagerExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to delete the Manager
        /// </summary>
        private ICommand deleteManager;
        public ICommand DeleteManager
        {
            get
            {
                if (deleteManager == null)
                {
                    deleteManager = new RelayCommand(param => DeleteManagerExecute(), param => CanDeleteManagerExecute());
                }
                return deleteManager;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteManagerExecute()
        {
            // Checks if the user really wants to delete the user
            var result = MessageBox.Show("Are you sure you want to delete the manager?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Manager != null)
                    {
                        int userID = Manager.UserID;
                        managerData.DeleteManager(userID);
                        ManagerList = managerData.GetAllManagers();
                        DoctorList = docData.GetAllDoctors().ToList();

                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully deleted a Manager";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the manager can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteManagerExecute()
        {
            if (ManagerList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit manager window
        /// </summary>
        private ICommand editManager;
        public ICommand EditManager
        {
            get
            {
                if (editManager == null)
                {
                    editManager = new RelayCommand(param => EditManagerExecute(), param => CanEditManagerExecute());
                }
                return editManager;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditManagerExecute()
        {
            try
            {
                if (Manager != null)
                {
                    AddManagerWindow addManager = new AddManagerWindow(Manager);
                    addManager.ShowDialog();

                    if (ManagerData.isChanged == true)
                    {
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully updated a Manager";
                        MaintenanceData.isChanged = false;
                    }

                    ManagerList = managerData.GetAllManagers().ToList();
                    DoctorList = docData.GetAllDoctors().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the manager can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditManagerExecute()
        {
            if (MaintenanceList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

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
        /// Command that tries to delete the Doctor
        /// </summary>
        private ICommand deleteDoctor;
        public ICommand DeleteDoctor
        {
            get
            {
                if (deleteDoctor == null)
                {
                    deleteDoctor = new RelayCommand(param => DeleteDoctorExecute(), param => CanDeleteDoctorExecute());
                }
                return deleteDoctor;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteDoctorExecute()
        {
            // Checks if the user really wants to delete the user
            var result = MessageBox.Show("Are you sure you want to delete the doctor?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Doctor != null)
                    {
                        int userID = Doctor.UserID;
                        docData.DeleteDoctor(userID);
                        DoctorList = docData.GetAllDoctors().ToList();
                        PatientList = patData.GetAllPatients().ToList();

                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully deleted a Doctor";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the doctor can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteDoctorExecute()
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
                    PatientList = patData.GetAllPatients().ToList();
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
        /// Command that tries to add a new Patient
        /// </summary>
        private ICommand addNewPatient;
        public ICommand AddNewPatient
        {
            get
            {
                if (addNewPatient == null)
                {
                    addNewPatient = new RelayCommand(param => AddNewPatientExecute(), param => CanAddNewPatientExecute());
                }
                return addNewPatient;
            }
        }

        /// <summary>
        /// Executes the add Patient command
        /// </summary>
        private void AddNewPatientExecute()
        {
            try
            {
                AddPatientWindow addPatient = new AddPatientWindow();
                addPatient.ShowDialog();
                if ((addPatient.DataContext as AddPatientViewModel).IsUpdatePatient == true)
                {
                    PatientList = patData.GetAllPatients().ToList();
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully created a new Patient";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new patient
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewPatientExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to delete the Patient
        /// </summary>
        private ICommand deletePatient;
        public ICommand DeletePatient
        {
            get
            {
                if (deletePatient == null)
                {
                    deletePatient = new RelayCommand(param => DeletePatientExecute(), param => CanDeletePatientExecute());
                }
                return deletePatient;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeletePatientExecute()
        {
            // Checks if the user really wants to delete the user
            var result = MessageBox.Show("Are you sure you want to delete the patient?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Patient != null)
                    {
                        int userID = Patient.UserID;
                        patData.DeletePatient(userID);
                        PatientList = patData.GetAllPatients().ToList();

                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully deleted a Patient";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the patient can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeletePatientExecute()
        {
            if (PatientList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit patient window
        /// </summary>
        private ICommand editPatient;
        public ICommand EditPatient
        {
            get
            {
                if (editPatient == null)
                {
                    editPatient = new RelayCommand(param => EditPatientExecute(), param => CanEditPatientExecute());
                }
                return editPatient;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditPatientExecute()
        {
            try
            {
                if (Patient != null)
                {
                    AddPatientWindow adPatient = new AddPatientWindow(Patient);
                    adPatient.ShowDialog();

                    if (PatientData.isChanged == true)
                    {
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully updated a Patient";
                        PatientData.isChanged = false;
                    }

                    PatientList = patData.GetAllPatients().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the patient can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditPatientExecute()
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
        /// Statistic Report Button
        /// </summary>
        private ICommand statisticReportOpen;
        public ICommand StatisticReportOpen
        {
            get
            {
                if (statisticReportOpen == null)
                {
                    statisticReportOpen = new RelayCommand(param => StatisticReportOpenExecute(), param => CanStatisticReportOpenExecute());
                }
                return statisticReportOpen;
            }
        }

        /// <summary>
        /// Tries to open the report
        /// </summary>
        private void StatisticReportOpenExecute()
        {
            try
            {
                ReportStatisticsWindow statisticReportWindow = new ReportStatisticsWindow();
                statisticReportWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to open the statistic report
        /// </summary>
        protected bool CanStatisticReportOpenExecute()
        {
            return true;
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
                if (Application.Current.Windows.OfType<AddClinicWindow>().FirstOrDefault() != null)
                {
                    Login login = new Login();
                    addClinic.Close();
                    login.Show();
                }
                else if (Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault() != null)
                {
                    adminWidnow.Close();
                }
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
