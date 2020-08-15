using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All Maintenance crud operations
    /// </summary>
    class MaintenanceData
    {
        /// <summary>
        /// Data from the user
        /// </summary>
        UserData userData = new UserData();
        /// <summary>
        /// Check if data is changed
        /// </summary>
        public static bool isChanged = false;

        /// <summary>
        /// Get all data about Maintenance from the database
        /// </summary>
        /// <returns>The list of all Maintenance</returns>
        public Queue<vwClinicMaintenance> GetAllMaintenances()
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<vwClinicMaintenance> list = new List<vwClinicMaintenance>();
                    list = (from x in context.vwClinicMaintenances select x).ToList();

                    Queue<vwClinicMaintenance> queue = new Queue<vwClinicMaintenance>(list);
                    return queue;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void QueueSize(Queue<vwClinicMaintenance> queue, int size)
        {
            if (queue.Count > size)
            {
                DeleteMaintenance(queue.Peek().UserID);
            }
        }

        /// <summary>
        /// Creates or edits a Maintenance
        /// </summary>
        /// <param name="maintenance">the Maintenance that is being added</param>
        /// <returns>a new or edited Maintenance</returns>
        public vwClinicMaintenance AddMaintenance(vwClinicMaintenance maintenance)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (maintenance.MaintenanceID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = maintenance.FirstName,
                            LastName = maintenance.LastName,
                            IdentificationCard = maintenance.IdentificationCard,
                            Gender = maintenance.Gender,
                            DateOfBirth = maintenance.DateOfBirth,
                            Citizenship = maintenance.Citizenship,
                            Username = maintenance.Username,
                            UserPassword = maintenance.UserPassword
                        };

                        userData.AddUser(newUser);

                        tblClinicMaintenance newMaintenance = new tblClinicMaintenance
                        {
                            ClinicExtentionAllowed = maintenance.ClinicExtentionAllowed,
                            DisabledAccessabilityResponsibility = maintenance.DisabledAccessabilityResponsibility,
                            UserID = newUser.UserID
                        };

                        context.tblClinicMaintenances.Add(newMaintenance);
                        context.SaveChanges();
                        maintenance.MaintenanceID = newMaintenance.MaintenanceID;

                        string addMain = $"Created Maintenance {maintenance.FirstName} {maintenance.LastName}, Identification Card: {maintenance.IdentificationCard}, " +
                            $"Gender: {maintenance.Gender}, Date of Birth: {maintenance.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {maintenance.Citizenship}" +
                            $", Clinic Extention Allowed: {maintenance.ClinicExtentionAllowed}, Disabled Accessability Responsibility: {maintenance.DisabledAccessabilityResponsibility}"
                        Thread logger = new Thread(() => LogManager.Instance.WriteLog(addMain));
                        logger.Start();

                        return maintenance;
                    }
                    else
                    {
                        tblUser userToEdit = new tblUser
                        {
                            FirstName = maintenance.FirstName,
                            LastName = maintenance.LastName,
                            IdentificationCard = maintenance.IdentificationCard,
                            Gender = maintenance.Gender,
                            DateOfBirth = maintenance.DateOfBirth,
                            Citizenship = maintenance.Citizenship,
                            Username = maintenance.Username,
                            UserPassword = maintenance.UserPassword,
                            UserID = maintenance.UserID
                        };
                        userData.AddUser(userToEdit);

                        tblClinicMaintenance maintenanceToEdit = (from ss in context.tblClinicMaintenances where ss.UserID == maintenance.UserID select ss).First();
                        maintenanceToEdit.ClinicExtentionAllowed = maintenance.ClinicExtentionAllowed;
                        maintenanceToEdit.DisabledAccessabilityResponsibility = maintenance.DisabledAccessabilityResponsibility;
                        maintenanceToEdit.UserID = userToEdit.UserID;
                        maintenanceToEdit.MaintenanceID = maintenance.MaintenanceID;

                        context.SaveChanges();

                        string updateMain = $"Updated Maintenance {userToEdit.FirstName} {userToEdit.LastName}, Identification Card: {userToEdit.IdentificationCard}, " +
                            $"Gender: {userToEdit.Gender}, Date of Birth: {userToEdit.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {userToEdit.Citizenship}" +
                            $", Clinic Extention Allowed: {maintenanceToEdit.ClinicExtentionAllowed}, Disabled Accessability Responsibility: {maintenanceToEdit.DisabledAccessabilityResponsibility}"
                        Thread logger = new Thread(() => LogManager.Instance.WriteLog(updateMain));
                        logger.Start();

                        isChanged = true;

                        return maintenance;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                Thread logger = new Thread(() => LogManager.Instance.WriteLog("Failed to update or create Maintenance"));
                logger.Start();
                return null;
            }
        }

        /// <summary>
        /// Deletes Maintenance user
        /// </summary>
        /// <param name="userID">the Maintenance that is being deleted</param>
        public void DeleteMaintenance(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    for (int i = 0; i < GetAllMaintenances().Count; i++)
                    {
                        if (GetAllMaintenances().ToList()[i].UserID == userID)
                        {
                            tblClinicMaintenance main = (from r in context.tblClinicMaintenances where r.UserID == userID select r).First();

                            string mainDel = $"Deleted Maintenance {GetAllMaintenances().ToList()[i].FirstName} {GetAllMaintenances().ToList()[i].LastName}, " +
                                $"Identification Card: {GetAllMaintenances().ToList()[i].IdentificationCard}, " +
                                $"Gender: {GetAllMaintenances().ToList()[i].Gender}, Date of Birth: {GetAllMaintenances().ToList()[i].DateOfBirth.ToString("dd.MM.yyyy")}, " +
                                $"Citizenship: {GetAllMaintenances().ToList()[i].Citizenship}";
                            Thread logger = new Thread(() => LogManager.Instance.WriteLog(mainDel));
                            logger.Start();

                            context.tblClinicMaintenances.Remove(main);
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
