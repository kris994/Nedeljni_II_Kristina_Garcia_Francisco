using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class ClinicViewModel : BaseViewModel
    {
        AddClinicWindow addClinic;
        AdminWindow adminWidnow;
        EditClinicWindow editClinic;
        ClinicData clinicData = new ClinicData();
        ManagerData managerData = new ManagerData();
        MaintenanceData maintenanceData = new MaintenanceData();

        #region Constructor
        /// <summary>
        /// Constructor with the add clinic info window opening
        /// </summary>
        /// <param name="AddClinicWindow">opends the add clinic window</param>
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
                    ClinicList = clinicData.GetAllClinics().ToList();
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully updated the Clinic";
                }
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
            var result = MessageBox.Show("Are you sure you want to create this clinic?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
        /// Command that closes the add worker or edit worker window
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
                        InfoLabel = "Successfully created a new Maintenance Client and deleted old. (Exceeded max number of 3)";
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
            var result = MessageBox.Show("Are you sure you want to delete the maintenance client?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
                        InfoLabel = "Successfully deleted a Maintenance Client";
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

                    MaintenanceList = maintenanceData.GetAllMaintenances();

                    if (MaintenanceData.isChanged == true)
                    {
                        ManagerList = managerData.GetAllManagers().ToList();
                        InfoLabelBG = "#28a745";
                        InfoLabel = "Successfully updated a Maintenance Client";
                        MaintenanceData.isChanged = false;
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the report can be edited
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
