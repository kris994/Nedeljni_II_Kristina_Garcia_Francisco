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
    /// Adds or Edits Maintenance
    /// </summary>
    class AddMaintenanceViewModel : BaseViewModel
    {
        /// <summary>
        /// Add Maintenance Window
        /// </summary>
        AddMaintenanceWindow addMaintenance;
        /// <summary>
        /// Maintenance Data
        /// </summary>
        MaintenanceData maintenanceData = new MaintenanceData();

        #region Construcotr
        /// <summary>
        /// Constructor with the add maintenance info window opening
        /// </summary>
        /// <param name="addMaintenanceOpen">opends the add maintenance window</param>
        public AddMaintenanceViewModel(AddMaintenanceWindow addMaintenanceOpen)
        {
            maintenance = new vwClinicMaintenance();
            addMaintenance = addMaintenanceOpen;
            MaintenanceList = maintenanceData.GetAllMaintenances().ToList();
        }

        /// <summary>
        /// Constructor with edit maintenance window opening
        /// </summary>
        /// <param name="addMaintananceOpen">opens the edit maintenance window</param>
        /// <param name="maintenanceEdit">gets the maintenance info that is being edited</param>
        public AddMaintenanceViewModel(AddMaintenanceWindow addMaintananceOpen, vwClinicMaintenance maintenanceEdit)
        {
            maintenance = maintenanceEdit;
            addMaintenance = addMaintananceOpen;
            MaintenanceList = maintenanceData.GetAllMaintenances().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of maintenance
        /// </summary>
        private List<vwClinicMaintenance> maintenanceList;
        public List<vwClinicMaintenance> MaintenanceList
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
        /// Specific maintenance
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
        /// Cheks if the Maintenance was added or updated
        /// </summary>
        private bool isUpdateMaintenance;
        public bool IsUpdateMaintenance
        {
            get
            {
                return isUpdateMaintenance;
            }
            set
            {
                isUpdateMaintenance = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new Maintenance
        /// </summary>
        private ICommand saveMaintenance;
        public ICommand SaveMaintenance
        {
            get
            {
                if (saveMaintenance == null)
                {
                    saveMaintenance = new RelayCommand(param => SaveMaintenanceExecute(), param => this.CanSaveMaintenanceExecute);
                }
                return saveMaintenance;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveMaintenanceExecute()
        {
            var result = MessageBox.Show("Are you sure you want to save this maintenance?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    maintenanceData.AddMaintenance(Maintenance);
                    IsUpdateMaintenance = true;

                    addMaintenance.Close();
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
        /// Checks if its possible to save the maintenance
        /// </summary>
        protected bool CanSaveMaintenanceExecute
        {
            get
            {
                return Maintenance.IsValid;
            }
        }

        /// <summary>
        /// Command that closes window
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
                addMaintenance.Close();
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
