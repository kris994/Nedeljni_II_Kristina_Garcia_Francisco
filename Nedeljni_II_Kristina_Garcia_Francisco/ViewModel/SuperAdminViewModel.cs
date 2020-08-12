﻿using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Super admin view model
    /// </summary>
    class SuperAdminViewModel : BaseViewModel
    {
        AdminData adminData = new AdminData();
        FileReadWrite frw = new FileReadWrite();
        SuperAdminWindow superAdminView;
        SuperAdminCredentialsChange credentialsChange;

        #region Constructor
        /// <summary>
        /// Constructor with super admin param
        /// </summary>
        /// <param name="SuperAdminWindow">opens the admin window</param>
        public SuperAdminViewModel(SuperAdminWindow adminOpen)
        {
            superAdminView = adminOpen;
            AdminList = adminData.GetAllAdmins().ToList();
        }

        /// <summary>
        /// Constructor with credentials change param
        /// </summary>
        /// <param name="SuperAdminCredentialsChange">opens the credentials change window</param>
        public SuperAdminViewModel(SuperAdminCredentialsChange creentialsOpen)
        {
            credentialsChange = creentialsOpen;
        }
        #endregion

        #region Property
        /// <summary>
        /// List of users
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
        /// Username
        /// </summary>
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that creates a new admin user
        /// </summary>
        private ICommand addNewAdmin;
        public ICommand AddNewAdmin
        {
            get
            {
                if (addNewAdmin == null)
                {
                    addNewAdmin = new RelayCommand(param => AddNewAdminExecute(), param => CanAddNewAdminExecute());
                }
                return addNewAdmin;
            }
        }

        /// <summary>
        /// Executes the add admin command
        /// </summary>
        private void AddNewAdminExecute()
        {
            try
            {
                AddAdminWindow addAdminView = new AddAdminWindow();
                addAdminView.ShowDialog();
                if ((addAdminView.DataContext as AddUserViewModel).IsUpdateAdmin == true)
                {
                    AdminList = adminData.GetAllAdmins().ToList();
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
        private bool CanAddNewAdminExecute()
        {
            if (!AdminList.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Command that changes the super admin credentials
        /// </summary>
        private ICommand changeCredentials;
        public ICommand ChangeCredentials
        {
            get
            {
                if (changeCredentials == null)
                {
                    changeCredentials = new RelayCommand(param => ChangeCredentialsExecute(), param => CanChangeCredentialsExecute());
                }
                return changeCredentials;
            }
        }

        /// <summary>
        /// Executes the super admin credentials change
        /// </summary>
        private void ChangeCredentialsExecute()
        {
            try
            {
                credentialsChange = new SuperAdminCredentialsChange();
                credentialsChange.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to change credentials
        /// </summary>
        /// <returns>true</returns>
        private bool CanChangeCredentialsExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to save the new credentials
        /// </summary>
        private ICommand saveCredentials;
        public ICommand SaveCredentials
        {
            get
            {
                if (saveCredentials == null)
                {
                    saveCredentials = new RelayCommand(param => SaveCredentialsExecute(), param => CanSaveCredentialsExecuteExecute());
                }
                return saveCredentials;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveCredentialsExecute()
        {
            try
            {
                frw.WriteAdminFile(Username, Password);
                SuperAdmin.SuperAdminUsername = Username;
                SuperAdmin.SuperAdminPassword = Password;

                Thread logger = new Thread(() =>
                    LogManager.Instance.WriteLog($"Updated Super Admin credentials Username: {Username} and Password: {Password}"));
                logger.Start();

                credentialsChange.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the credentials
        /// </summary>
        protected bool CanSaveCredentialsExecuteExecute()
        {
            Validations val = new Validations();
            bool canSave = val.HasCurrentUsernameUsernameChecker(Username, SuperAdmin.SuperAdminUsername);

            if (canSave == true)
            {
                return true;
            }
            else
            {
                return false;
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
                credentialsChange.Close();
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
                Login login = new Login();
                superAdminView.Close();
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