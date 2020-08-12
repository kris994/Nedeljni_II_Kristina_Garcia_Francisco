using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All Maintenance crud operations
    /// </summary>
    class MaintenanceData
    {
        UserData userData = new UserData();

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

                        return maintenance;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == maintenance.UserID select ss).First();
                        userData.AddUser(userToEdit);

                        tblClinicMaintenance maintenanceToEdit = (from ss in context.tblClinicMaintenances where ss.UserID == maintenance.UserID select ss).First();
                        maintenanceToEdit.ClinicExtentionAllowed = maintenance.ClinicExtentionAllowed;
                        maintenanceToEdit.DisabledAccessabilityResponsibility = maintenance.DisabledAccessabilityResponsibility;
                        maintenanceToEdit.UserID = userToEdit.UserID;
                        maintenanceToEdit.MaintenanceID = maintenance.MaintenanceID;

                        context.SaveChanges();                     
                        return maintenance;
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
                            context.tblClinicMaintenances.Remove(main);
                            context.SaveChanges();
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
