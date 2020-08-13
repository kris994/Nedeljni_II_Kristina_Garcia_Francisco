using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// class used to create the CRUD structure of the application
    /// </summary>
    class UserData
    {
        /// <summary>
        /// Get all data about users from the database
        /// </summary>
        /// <returns>The list of all users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Search if user with that ID exists in the user table
        /// </summary>
        /// <param name="userID">Takes the user id that we want to search for</param>
        /// <returns>True if the user exists</returns>
        private bool IsUserID(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    int result = (from x in context.tblUsers where x.UserID == userID select x.UserID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Adds or edits a user in the database
        /// </summary>
        /// <param name="user">The user ID we are adding or editing</param>
        /// <returns>The new or edited user</returns>
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (user.UserID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IdentificationCard = user.IdentificationCard,
                            Gender = user.Gender,
                            DateOfBirth = user.DateOfBirth,
                            Citizenship = user.Citizenship,
                            Username = user.Username,
                            UserPassword = PasswordHasher.Hash(user.UserPassword)
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return user;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == user.UserID select ss).First();

                        userToEdit.FirstName = user.FirstName;
                        userToEdit.LastName = user.LastName;
                        userToEdit.IdentificationCard = user.IdentificationCard;
                        userToEdit.Gender = user.Gender;
                        userToEdit.DateOfBirth = user.DateOfBirth;
                        userToEdit.Citizenship = user.Citizenship;
                        userToEdit.Username = user.Username;
                        userToEdit.UserPassword = PasswordHasher.Hash(user.UserPassword);

                        userToEdit.UserID = user.UserID;
                        context.SaveChanges();

                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Deletes a specific user
        /// </summary>
        /// <param name="userID">User that we are deleting</param>
        public void DeleteUser(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    bool isUser = IsUserID(userID);

                    if (isUser == true)
                    {
                        tblUser userToDelete = (from r in context.tblUsers where r.UserID == userID select r).First();

                        context.tblUsers.Remove(userToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the user");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
