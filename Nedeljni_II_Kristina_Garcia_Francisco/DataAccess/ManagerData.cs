﻿using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All manager crud operations
    /// </summary>
    class ManagerData
    {
        UserData userData = new UserData();

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

                        return manager;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == manager.UserID select ss).First();
                        userData.AddUser(userToEdit);

                        tblClinicManager managerToEdit = (from ss in context.tblClinicManagers where ss.UserID == manager.UserID select ss).First();

                        managerToEdit.FloorNumber = manager.FloorNumber;
                        managerToEdit.MaxNumberOfDoctors = manager.MaxNumberOfDoctors;
                        managerToEdit.MinNumberOfRooms = manager.MinNumberOfRooms;
                        managerToEdit.OmissionNumber = manager.OmissionNumber;
                        managerToEdit.UserID = userToEdit.UserID;
                        managerToEdit.ManagerID = manager.ManagerID;

                        context.SaveChanges();
                        return manager;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }       
    }
}
