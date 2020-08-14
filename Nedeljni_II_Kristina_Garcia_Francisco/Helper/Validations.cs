using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Validates the user inputs
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Checks if the Username is exists
        /// </summary>
        /// <param name="username">the username we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string UsernameChecker(string username, int id)
        {
            UserData userData = new UserData();

            List<tblUser> AllUsers = userData.GetAllUsers();
            string currectUsername = "";

            if (username == null)
            {
                return "Username cannot be empty.";
            }
            // Get the current users username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currectUsername = AllUsers[i].Username;
                    break;
                }
            }

            // Cannot be the same username as admins
            if (username == SuperAdmin.SuperAdminUsername)
            {
                return "This Username already exists!";
            }

            // Check if the username already exists, but it is not the current user username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if ((AllUsers[i].Username == username && currectUsername != username))
                {
                    return "This Username already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the Username is exists
        /// </summary>
        /// <param name="username">the username we are checking</param>
        /// <param name="currentUsername">current Username for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string HasCurrentUsernameUsernameChecker(string username, string currentUsername)
        {
            UserData userData = new UserData();

            List<tblUser> AllUsers = userData.GetAllUsers();

            if (username == null)
            {
                return "Username cannot be empty.";
            }

            // Check if the username already exists, but it is not the current user username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if ((AllUsers[i].Username == username && currentUsername != username))
                {
                    return "This Username already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the entered password is valid
        /// </summary>
        /// <param name="password">Password we are checking</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string PasswordValidation(string password)
        {
            string allowedSpecials = "!@#$%^&*()_+=-[]{}|';:?/\"\\<>,.";
            bool containsSpecial = false;

            if (password == null || password.Length < 8)
            {
                return "Password needs to be at least 8 characters long";
            }

            char[] characters = password.ToCharArray();

            foreach (char item in characters)
            {
                if (allowedSpecials.Contains(item.ToString()))
                {
                    containsSpecial = true;
                    break;
                }
            }

            if (password.Any(char.IsUpper) == true && password.Any(char.IsLower) == true 
                && password.Any(char.IsNumber) == true && containsSpecial == true)
            {
                return null;
            }
            else
            {
                return "Password needs to contain a lower case and upper case character, a symbol and a number";
            }
        }

        /// <summary>
        /// Checks if the identification card is exists
        /// </summary>
        /// <param name="identificationCard">the identification card we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string IdentificationCardChecker(string identificationCard, int id)
        {
            UserData userData = new UserData();

            List<tblUser> AllUsers = userData.GetAllUsers();
            string currectIdentificationCard = "";

            if (identificationCard == null)
            {
                return "Identification Card cannot be empty.";
            }

            if (identificationCard.Length != 9)
            {
                return "Identification Card has to be 9 characters long.";
            }

            // Get the current users id
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currectIdentificationCard = AllUsers[i].IdentificationCard;
                    break;
                }
            }

            // Check if the id already exists, but it is not the current user id
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if ((AllUsers[i].IdentificationCard == identificationCard && currectIdentificationCard != identificationCard))
                {
                    return "This Identification Card already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the doctor nuique number exists
        /// </summary>
        /// <param name="doctorNumber">the doctor Number we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string DoctorNumberChecker(string doctorNumber, int id)
        {
            DoctorData docData = new DoctorData();

            List<vwClinicDoctor> AllDoctors = docData.GetAllDoctors();
            string currentDoctorID = "";

            if (doctorNumber == null)
            {
                return "Doctor unique number cannot be empty.";
            }

            if (doctorNumber.Length != 9)
            {
                return "Doctor unique number has to be 9 characters long.";
            }

            // Get the current users id
            for (int i = 0; i < AllDoctors.Count; i++)
            {
                if (AllDoctors[i].UserID == id)
                {
                    currentDoctorID = AllDoctors[i].UniqueNumber;
                    break;
                }
            }

            // Check if the id already exists, but it is not the current user id
            for (int i = 0; i < AllDoctors.Count; i++)
            {
                if ((AllDoctors[i].UniqueNumber == doctorNumber && currentDoctorID != doctorNumber))
                {
                    return "This Doctor unique number already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the health care unique number exists
        /// </summary>
        /// <param name="number">the health care Number we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string HealthCareNumberChecker(string number, int id)
        {
            PatientData patData = new PatientData();

            List<vwClinicPatient> allPatients = patData.GetAllPatients();
            string currentHealthID = "";

            if (number == null)
            {
                return "Healthcare number cannot be empty.";
            }

            if (number.Length != 6)
            {
                return "Healthcare number has to be 6 characters long.";
            }

            // Get the current users id
            for (int i = 0; i < allPatients.Count; i++)
            {
                if (allPatients[i].UserID == id)
                {
                    currentHealthID = allPatients[i].HealthCareNumber;
                    break;
                }
            }

            // Check if the id already exists, but it is not the current user id
            for (int i = 0; i < allPatients.Count; i++)
            {
                if ((allPatients[i].HealthCareNumber == number && currentHealthID != number))
                {
                    return "This Healthcare number already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the bank account already exists
        /// </summary>
        /// <param name="bankAccount">the Bank Number we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string BankAccountChecker(string bankNumber, int id)
        {
            DoctorData docData = new DoctorData();

            List<vwClinicDoctor> AllDoctors = docData.GetAllDoctors();
            string currentDoctorBank = "";

            if (bankNumber == null)
            {
                return "Bank account number cannot be empty.";
            }

            if (bankNumber.Length < 4)
            {
                return "Bank account number has to be longer than 4 characters.";
            }

            // Get the current users id
            for (int i = 0; i < AllDoctors.Count; i++)
            {
                if (AllDoctors[i].UserID == id)
                {
                    currentDoctorBank = AllDoctors[i].BankAccount;
                    break;
                }
            }

            // Check if the id already exists, but it is not the current user id
            for (int i = 0; i < AllDoctors.Count; i++)
            {
                if ((AllDoctors[i].BankAccount == bankNumber && currentDoctorBank != bankNumber))
                {
                    return "This Bank account number already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// An entered value cannot be Zero
        /// </summary>
        /// <param name="value">value that is entered</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeZero(int value)
        {
            if (value == 0)
            {
                return "Entered Value cannot be Zero";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// An entered value cannot be lower than existing
        /// </summary>
        /// <param name="value">value that is entered</param>
        /// <param name="currentValueID">value id</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string EmergencyVehicleCannotBeLower(int value, int currentValueID)
        {
            int currentValue = 0;
            ClinicData clinicData = new ClinicData();

            if (value == 0)
            {
                return "Entered Value cannot be Zero";
            }

            // Get the current users id
            for (int i = 0; i < clinicData.GetAllClinics().Count; i++)
            {
                if (clinicData.GetAllClinics()[i].ClinicID == currentValueID)
                {
                    currentValue = clinicData.GetAllClinics()[i].EmergencyVehicleParkingLoots;
                    break;
                }
            }

            if (value < currentValue)
            {
                return "Value cannot be smaller than " + currentValue;
            }

            return null;
        }

        /// <summary>
        /// An entered value cannot be lower than existing
        /// </summary>
        /// <param name="value">value that is entered</param>
        /// <param name="currentValueID">value id</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string InvalidVehicleCannotBeLower(int value, int currentValueID)
        {
            int currentValue = 0;
            ClinicData clinicData = new ClinicData();

            if (value == 0)
            {
                return "Entered Value cannot be Zero";
            }

            // Get the current users id
            for (int i = 0; i < clinicData.GetAllClinics().Count; i++)
            {
                if (clinicData.GetAllClinics()[i].ClinicID == currentValueID)
                {
                    currentValue = clinicData.GetAllClinics()[i].InvalidVehicleParkingLoots;
                    break;
                }
            }

            if (value < currentValue)
            {
                return "Value cannot be smaller than " + currentValue;
            }

            return null;
        }

        /// <summary>
        /// The input cannot be empty
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Cannot be empty";
            }
            else
            {
                return null;
            }
        }
    }
}
