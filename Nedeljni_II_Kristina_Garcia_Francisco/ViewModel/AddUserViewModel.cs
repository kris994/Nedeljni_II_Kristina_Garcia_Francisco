using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using System;
using System.Diagnostics;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class AddUserViewModel : BaseViewModel
    {
        AddAdminWindow addAdmin;
        AdminData adminData = new AdminData();

        #region Constructor
        /// <summary>
        /// Constructor with the add admin info window opening
        /// </summary>
        /// <param name="addAdminWindowOpen">opends the add admin window</param>
        public AddUserViewModel(AddAdminWindow addAdminWindowOpen)
        {
            admin = new vwClinicAdministrator();
            addAdmin = addAdminWindowOpen;
            AdminList = adminData.GetAllAdmins().ToList();
        }    
        #endregion

        #region Property
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
        /// Specific Admin
        /// </summary>
        private vwClinicAdministrator admin;
        public vwClinicAdministrator Admin
        {
            get
            {
                return admin;
            }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
            }
        }

        /// <summary>
        /// Checks if its possible to execute the add and edit admin commands
        /// </summary>
        private bool isUpdateAdmin;
        public bool IsUpdateAdmin
        {
            get
            {
                return isUpdateAdmin;
            }
            set
            {
                isUpdateAdmin = value;
            }
        }     
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new admin
        /// </summary>
        private ICommand saveAdmin;
        public ICommand SaveAdmin
        {
            get
            {
                if (saveAdmin == null)
                {
                    saveAdmin = new RelayCommand(param => SaveAdminExecute(), param => this.CanSaveAdminExecute);
                }
                return saveAdmin;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveAdminExecute()
        {
            var result = MessageBox.Show("Are you sure you want to create this admin?\nThis action cannot be reverted.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    adminData.AddAdmin(Admin);
                    IsUpdateAdmin = true;

                    addAdmin.Close();
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
        /// Checks if its possible to save the admin
        /// </summary>
        protected bool CanSaveAdminExecute
        {
            get
            {
                return Admin.IsValid;
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
                if (Application.Current.Windows.OfType<AddAdminWindow>().FirstOrDefault() != null)
                {
                    addAdmin.Close();
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
        #endregion
    }
}
