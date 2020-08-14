using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All admin crud operations
    /// </summary>
    class AdminData
    {
        UserData userData = new UserData();
        /// <summary>
        /// Check if data is changed
        /// </summary>
        public static bool isChanged = false;

        /// <summary>
        /// Get all data about admins from the database
        /// </summary>
        /// <returns>The list of all admins</returns>
        public List<vwClinicAdministrator> GetAllAdmins()
        {
            try
            {               
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<vwClinicAdministrator> list = new List<vwClinicAdministrator>();
                    list = (from x in context.vwClinicAdministrators select x).ToList();
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
        /// Creates or edits an admin
        /// </summary>
        /// <param name="admin">the admin that is being added</param>
        /// <returns>a new or edited admin</returns>
        public vwClinicAdministrator AddAdmin(vwClinicAdministrator admin)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (admin.AdminID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = admin.FirstName,
                            LastName = admin.LastName,
                            IdentificationCard = admin.IdentificationCard,
                            Gender = admin.Gender,
                            DateOfBirth = admin.DateOfBirth,
                            Citizenship = admin.Citizenship,
                            Username = admin.Username,
                            UserPassword = admin.UserPassword
                        };

                        userData.AddUser(newUser);

                        tblClinicAdministrator newAdmin = new tblClinicAdministrator
                        {
                            UserID = newUser.UserID
                        };

                        context.tblClinicAdministrators.Add(newAdmin);
                        context.SaveChanges();
                        admin.AdminID = newAdmin.AdminID;

                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog($"Created Admin {admin.FirstName} {admin.LastName}, Identification Card: {admin.IdentificationCard}, " +
                            $"Gender: {admin.Gender}, Date of Birth: {admin.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {admin.Citizenship}"));
                        logger.Start();

                        return admin;
                    }
                    else
                    {
                        tblUser userToEdit = new tblUser
                        {
                            FirstName = admin.FirstName,
                            LastName = admin.LastName,
                            IdentificationCard = admin.IdentificationCard,
                            Gender = admin.Gender,
                            DateOfBirth = admin.DateOfBirth,
                            Citizenship = admin.Citizenship,
                            Username = admin.Username,
                            UserPassword = admin.UserPassword,
                            UserID = admin.UserID
                        };

                        userData.AddUser(userToEdit);

                        tblClinicAdministrator adminToEdit = (from ss in context.tblClinicAdministrators where ss.UserID == admin.UserID select ss).First();
                        adminToEdit.UserID = userToEdit.UserID;
                        adminToEdit.AdminID = admin.AdminID;

                        context.SaveChanges();

                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog($"Edited Admin {userToEdit.FirstName} {userToEdit.LastName}, Identification Card: {userToEdit.IdentificationCard}, " +
                            $"Gender: {userToEdit.Gender}, Date of Birth: {userToEdit.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {userToEdit.Citizenship}"));
                        logger.Start();

                        isChanged = true;

                        return admin;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                Thread logger = new Thread(() => LogManager.Instance.WriteLog("Failed to update or create Admin Client"));
                logger.Start();
                return null;
            }
        }

        /// <summary>
        /// Deletes admin user
        /// </summary>
        /// <param name="userID">the admin that is being deleted</param>
        public void DeleteAdmin(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    for (int i = 0; i < GetAllAdmins().Count; i++)
                    {
                        if (GetAllAdmins()[i].UserID == userID)
                        {
                            tblClinicAdministrator admin = (from r in context.tblClinicAdministrators where r.UserID == userID select r).First();


                            Thread logger = new Thread(() =>
                                LogManager.Instance.WriteLog($"Deleted Admin {GetAllAdmins()[i].FirstName} {GetAllAdmins()[i].LastName}, Identification Card: {GetAllAdmins()[i].IdentificationCard}, " +
                                $"Gender: {GetAllAdmins()[i].Gender}, Date of Birth: {GetAllAdmins()[i].DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {GetAllAdmins()[i].Citizenship}"));
                            logger.Start();

                            context.tblClinicAdministrators.Remove(admin);
                            context.SaveChanges();
                        }
                    }

                    userData.DeleteUser(userID);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                Thread logger = new Thread(() => LogManager.Instance.WriteLog("Failed to delete Admin Client"));
                logger.Start();
            }
        }
    }
}
