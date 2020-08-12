using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
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
        UserData userData = new UserData();
        FileReadWrite frw = new FileReadWrite();   

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = userData.GetAllUsers().ToList();
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
            else if (UserList.Any())
            {
                for (int i = 0; i < UserList.Count; i++)
                {
                    if (userData.IsCorrectUser(User.Username, password) == true)
                    {
                        LoggedInUser.CurrentUser = new tblUser
                        {
                            UserID = UserList[i].UserID,
                            FirstName = UserList[i].FirstName,
                            LastName = UserList[i].LastName,
                            IdentificationCard = UserList[i].IdentificationCard,
                            DateOfBirth = UserList[i].DateOfBirth,
                            Citizenship = UserList[i].Citizenship,
                            Username = UserList[i].Username,
                            UserPassword = UserList[i].UserPassword
                        };
                        AdminData ad = new AdminData();
                        InfoLabel = "Logged in";
                        found = true;

                        if (ad.GetAllAdmins().Any(id => id.UserID == UserList[i].UserID) == true)
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            view.Close();
                            adminWindow.Show();
                        }
                        break;
                    }
                }
            }

            if (found == false)
            {
                InfoLabel = "Wrong Username or Password";
            }
        }
        #endregion
    }
}
