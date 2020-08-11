﻿using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        Login view;
        //Service service = new Service();
        FileReadWrite frw = new FileReadWrite();

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            //UserList = service.GetAllUsers().ToList();
            frw.ReadAdminFile();
        }
        #endregion

        #region Property
        /// <summary>
        /// Login info label
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
        /// Used for saving the current user
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// List of all users in the application
        /// </summary>
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command used to log te user into the application
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Checks if its possible to login depending on the given username and password and saves the logged in user to a list
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            bool found = false;
            if (User.Username == SuperAdmin.SuperAdminUsername && password == SuperAdmin.SuperAdminPassword)
            {
                InfoLabel = "Logged in";

                SuperAdminWindow superAdminView = new SuperAdminWindow();
                view.Close();
                superAdminView.Show();
            }
        //    else if (UserList.Any())
        //    {
        //        for (int i = 0; i < UserList.Count; i++)
        //        {
        //            if (User.Username == UserList[i].Username && password == UserList[i].UserPassword)
        //            {
        //                LoggedUser.CurrentUser = new tblUser
        //                {
        //                    UserID = UserList[i].UserID,
        //                    FirstName = UserList[i].FirstName,
        //                    LastName = UserList[i].LastName,
        //                    DateOfBirth = UserList[i].DateOfBirth,
        //                    Email = UserList[i].Email,
        //                    Username = UserList[i].Username,
        //                    UserPassword = UserList[i].UserPassword
        //                };
        //                InfoLabel = "Logged in";
        //                found = true;
        //                if (service.GetAllManagers().Any(id => id.UserID == UserList[i].UserID) == true)
        //                {
        //                    AllUsers users = new AllUsers();
        //                    view.Close();
        //                    users.Show();
        //                }
        //                else if (service.GetAllEmployees().Any(id => id.UserID == UserList[i].UserID) == true)
        //                {
        //                    Employee emp = new Employee();
        //                    view.Close();
        //                    emp.Show();
        //                }
        //                break;
        //            }
        //        }

        //        if (found == false)
        //        {
        //            InfoLabel = "Wrong Username or Password";
        //        }
        //    }
        }
        #endregion
    }
}
