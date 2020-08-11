﻿using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;

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
    }
}
