using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        Login view;
        UserData userData = new UserData();
        ClinicData clinicData = new ClinicData();
        PatientData patientData = new PatientData();
        FileReadWrite frw = new FileReadWrite();   

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = userData.GetAllUsers().ToList();
            ClinicList = clinicData.GetAllClinics().ToList();
            PatientList = patientData.GetAllPatients().ToList();
            frw.ReadAdminFile();
        }
        #endregion

        #region Property
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

        /// <summary>
        /// List of all patient in the application
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
        /// List of all clinics in the application
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
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to add a new patient
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
                    PatientList = patientData.GetAllPatients().ToList();
                    UserList = userData.GetAllUsers().ToList();
                    InfoLabel = "Created a patient";
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
                    if (User.Username == UserList[i].Username && PasswordHasher.Verify(password, UserList[i].UserPassword) == true)
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
                        ManagerData md = new ManagerData();
                        MaintenanceData maind = new MaintenanceData();
                        DoctorData docd = new DoctorData();
                        InfoLabel = "Logged in";
                        found = true;

                        if (ad.GetAllAdmins().Any(id => id.Username == UserList[i].Username) == true)
                        {
                            if (ClinicList.Count == 0)
                            {
                                AddClinicWindow clinicWindow = new AddClinicWindow();
                                view.Close();
                                clinicWindow.Show();
                            }
                            else
                            {
                                AdminWindow adminWindow = new AdminWindow();
                                view.Close();
                                adminWindow.Show();
                            }
                        }
                        else if (md.GetAllManagers().Any(id => id.Username == UserList[i].Username) == true)
                        {
                            ManagerWindow manWindow = new ManagerWindow();
                            view.Close();
                            manWindow.Show();
                        }
                        else if (maind.GetAllMaintenances().Any(id => id.Username == UserList[i].Username) == true)
                        {
                            MaintenanceWindow mainWindow = new MaintenanceWindow();
                            view.Close();
                            mainWindow.Show();
                        }
                        else if (docd.GetAllDoctors().Any(id => id.Username == UserList[i].Username) == true)
                        {
                            DoctorWindow docWindow = new DoctorWindow();
                            view.Close();
                            docWindow.Show();
                        }
                        else if (patientData.GetAllPatients().Any(id => id.Username == UserList[i].Username) == true)
                        {
                            PatientWindow patWindow = new PatientWindow();
                            view.Close();
                            patWindow.Show();
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
