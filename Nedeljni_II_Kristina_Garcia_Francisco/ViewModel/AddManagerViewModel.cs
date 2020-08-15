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
    /// Adds or Edits a manager
    /// </summary>
    class AddManagerViewModel : BaseViewModel
    {
        /// <summary>
        /// Add Manager window
        /// </summary>
        AddManagerWindow addManager;
        /// <summary>
        /// Manager data
        /// </summary>
        ManagerData managerData = new ManagerData();

        #region Construcotr
        /// <summary>
        /// Constructor with the add manager info window opening
        /// </summary>
        /// <param name="addManagerOpen">opends the add manager window</param>
        public AddManagerViewModel(AddManagerWindow addManagerOpen)
        {
            manager = new vwClinicManager();
            addManager = addManagerOpen;
            ManagerList = managerData.GetAllManagers().ToList();
        }

        /// <summary>
        /// Constructor with edit manager window opening
        /// </summary>
        /// <param name="addManagerOpen">opens the edit manager window</param>
        /// <param name="managerEdit">gets the manager info that is being edited</param>
        public AddManagerViewModel(AddManagerWindow addManagerOpen, vwClinicManager managerEdit)
        {
            manager = managerEdit;
            addManager = addManagerOpen;
            ManagerList = managerData.GetAllManagers().ToList();
        }
        #endregion

        #region Property
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
        /// Cheks if the manager was added or updated
        /// </summary>
        private bool isUpdateManager;
        public bool IsUpdateManager
        {
            get
            {
                return isUpdateManager;
            }
            set
            {
                isUpdateManager = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new manager
        /// </summary>
        private ICommand saveManager;
        public ICommand SaveManager
        {
            get
            {
                if (saveManager == null)
                {
                    saveManager = new RelayCommand(param => SaveManagerExecute(), param => this.CanSaveManagerExecute);
                }
                return saveManager;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveManagerExecute()
        {
            var result = MessageBox.Show("Are you sure you want to save this manager?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    managerData.AddManager(Manager);
                    IsUpdateManager = true;

                    addManager.Close();
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
        /// Checks if its possible to save the manager
        /// </summary>
        protected bool CanSaveManagerExecute
        {
            get
            {
                return Manager.IsValid;
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
                addManager.Close();
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
