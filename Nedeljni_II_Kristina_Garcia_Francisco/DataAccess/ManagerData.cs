using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All manager crud operations
    /// </summary>
    class ManagerData
    {
        UserData userData = new UserData();
        DoctorData docData = new DoctorData();
        /// <summary>
        /// Check if data is changed
        /// </summary>
        public static bool isChanged = false;

        /// <summary>
        /// Get all data about managers from the database
        /// </summary>
        /// <returns>The list of all managers</returns>
        public List<vwClinicManager> GetAllManagers()
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<vwClinicManager> list = new List<vwClinicManager>();
                    list = (from x in context.vwClinicManagers select x).ToList();
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
        /// Get all data about managers that did not reach the maximum capacity
        /// </summary>
        /// <returns>The list of all managers</returns>
        public List<vwClinicManager> GetAllAvailableManagers()
        {
            List<vwClinicManager> list = new List<vwClinicManager>();
            for (int i = 0; i < GetAllManagers().Count; i++)
            {
                if (IsMaxDoctors(GetAllManagers()[i].MaxNumberOfDoctors, GetAllManagers()[i].ManagerID) == false)
                {
                    list.Add(GetAllManagers()[i]);
                }
            }

            return list;
        }

        /// <summary>
        /// Checks if the manager reached the max doctor capacity
        /// </summary>
        /// <param name="maxDoctors">maximum amount of doctors</param>
        /// <param name="managerID">doctors with that manager</param>
        /// <returns>true if the capacity was reached</returns>
        public bool IsMaxDoctors(int? maxDoctors, int managerID)
        {
            int counter = 0;
            for (int i = 0; i < docData.GetAllDoctors().Count; i++)
            {
                if (docData.GetAllDoctors()[i].ManagerID == managerID)
                {
                    counter++;
                }
            }

            if (counter >= maxDoctors)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates or edits an manager
        /// </summary>
        /// <param name="manager">the manager that is being added</param>
        /// <returns>a new or edited manager</returns>
        public vwClinicManager AddManager(vwClinicManager manager)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (manager.ManagerID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = manager.FirstName,
                            LastName = manager.LastName,
                            IdentificationCard = manager.IdentificationCard,
                            Gender = manager.Gender,
                            DateOfBirth = manager.DateOfBirth,
                            Citizenship = manager.Citizenship,
                            Username = manager.Username,
                            UserPassword = manager.UserPassword
                        };

                        userData.AddUser(newUser);

                        tblClinicManager newManager = new tblClinicManager
                        {
                            FloorNumber = manager.FloorNumber,
                            MaxNumberOfDoctors = manager.MaxNumberOfDoctors,
                            MinNumberOfRooms = manager.MinNumberOfRooms,
                            OmissionNumber = manager.OmissionNumber,
                            UserID = newUser.UserID
                        };

                        context.tblClinicManagers.Add(newManager);
                        context.SaveChanges();
                        manager.ManagerID = newManager.ManagerID;

                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog($"Created Manager {manager.FirstName} {manager.LastName}, Identification Card: {manager.IdentificationCard}, " +
                            $"Gender: {manager.Gender}, Date of Birth: {manager.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {manager.Citizenship}, Floor Number: {manager.FloorNumber} " +
                            $", Max Number of Doctors: {manager.MaxNumberOfDoctors}, Min Number of Rooms: {manager.MinNumberOfRooms}, Omission Number: {manager.OmissionNumber}"));
                        logger.Start();

                        return manager;
                    }
                    else
                    {
                        tblUser userToEdit = new tblUser
                        {
                            FirstName = manager.FirstName,
                            LastName = manager.LastName,
                            IdentificationCard = manager.IdentificationCard,
                            Gender = manager.Gender,
                            DateOfBirth = manager.DateOfBirth,
                            Citizenship = manager.Citizenship,
                            Username = manager.Username,
                            UserPassword = manager.UserPassword,
                            UserID = manager.UserID
                        };
                        userData.AddUser(userToEdit);

                        tblClinicManager managerToEdit = (from ss in context.tblClinicManagers where ss.UserID == manager.UserID select ss).First();

                        managerToEdit.FloorNumber = manager.FloorNumber;
                        managerToEdit.MaxNumberOfDoctors = manager.MaxNumberOfDoctors;
                        managerToEdit.MinNumberOfRooms = manager.MinNumberOfRooms;
                        managerToEdit.OmissionNumber = manager.OmissionNumber;
                        managerToEdit.UserID = userToEdit.UserID;
                        managerToEdit.ManagerID = manager.ManagerID;

                        context.SaveChanges();

                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog($"Updated Manager {userToEdit.FirstName} {userToEdit.LastName}, Identification Card: {userToEdit.IdentificationCard}, " +
                            $"Gender: {userToEdit.Gender}, Date of Birth: {userToEdit.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {userToEdit.Citizenship}, Floor Number: {managerToEdit.FloorNumber} " +
                            $", Max Number of Doctors: {managerToEdit.MaxNumberOfDoctors}, Min Number of Rooms: {managerToEdit.MinNumberOfRooms}, Omission Number: {managerToEdit.OmissionNumber}"));
                        logger.Start();

                        isChanged = true;

                        return manager;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                Thread logger = new Thread(() => LogManager.Instance.WriteLog("Failed to delete Manager"));
                logger.Start();
                return null;
            }
        }

        /// <summary>
        /// Deletes manager user
        /// </summary>
        /// <param name="userID">the manager that is being deleted</param>
        public void DeleteManager(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    for (int i = 0; i < GetAllManagers().Count; i++)
                    {
                        if (GetAllManagers().ToList()[i].UserID == userID)
                        {
                            tblClinicManager man = (from r in context.tblClinicManagers where r.UserID == userID select r).First();
                            context.tblClinicManagers.Remove(man);
                            context.SaveChanges();
                            break;
                        }
                    }

                    userData.DeleteUser(userID);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
