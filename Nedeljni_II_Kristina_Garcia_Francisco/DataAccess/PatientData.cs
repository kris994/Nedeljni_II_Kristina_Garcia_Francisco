using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{   
    /// <summary>
     /// All patient crud operations
     /// </summary>
    class PatientData
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
        /// Get all data about patients from the database
        /// </summary>
        /// <returns>The list of all patients</returns>
        public List<vwClinicPatient> GetAllPatients()
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<vwClinicPatient> list = new List<vwClinicPatient>();
                    list = (from x in context.vwClinicPatients select x).ToList();
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
        /// Creates or edits an patient
        /// </summary>
        /// <param name="patient">the patient that is being added</param>
        /// <returns>a new or edited patient</returns>
        public vwClinicPatient AddPatient(vwClinicPatient patient)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (patient.PatientID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = patient.FirstName,
                            LastName = patient.LastName,
                            IdentificationCard = patient.IdentificationCard,
                            Gender = patient.Gender,
                            DateOfBirth = patient.DateOfBirth,
                            Citizenship = patient.Citizenship,
                            Username = patient.Username,
                            UserPassword = patient.UserPassword
                        };

                        userData.AddUser(newUser);

                        tblClinicPatient newPatient = new tblClinicPatient
                        {
                            HealthCareNumber = patient.HealthCareNumber,
                            ExperationDate = patient.ExperationDate,
                            UniqueNumber = patient.UniqueNumber,
                            UserID = newUser.UserID
                        };

                        context.tblClinicPatients.Add(newPatient);
                        context.SaveChanges();
                        patient.PatientID = newPatient.PatientID;

                        string addPat = $"Created Patient {patient.FirstName} {patient.LastName}, Identification Card: {patient.IdentificationCard}, " +
                            $"Gender: {patient.Gender}, Date of Birth: {patient.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {patient.Citizenship}, HealthCare Number: {patient.HealthCareNumber} " +
                            $", Experation Date: {patient.ExperationDate}";
                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog(addPat));
                        logger.Start();

                        return patient;
                    }
                    else
                    {
                        tblUser userToEdit = new tblUser
                        {
                            FirstName = patient.FirstName,
                            LastName = patient.LastName,
                            IdentificationCard = patient.IdentificationCard,
                            Gender = patient.Gender,
                            DateOfBirth = patient.DateOfBirth,
                            Citizenship = patient.Citizenship,
                            Username = patient.Username,
                            UserPassword = patient.UserPassword,
                            UserID = patient.UserID
                        };
                        userData.AddUser(userToEdit);

                        tblClinicPatient patientToEdit = (from ss in context.tblClinicPatients where ss.UserID == patient.UserID select ss).First();

                        patientToEdit.UniqueNumber = patient.UniqueNumber;
                        patientToEdit.HealthCareNumber = patient.HealthCareNumber;
                        patientToEdit.ExperationDate = patient.ExperationDate;
                        patientToEdit.UserID = userToEdit.UserID;
                        patientToEdit.PatientID = patient.PatientID;

                        context.SaveChanges();

                        string updatePat = $"Updated Patient {userToEdit.FirstName} {userToEdit.LastName}, Identification Card: {userToEdit.IdentificationCard}, " +
                            $"Gender: {userToEdit.Gender}, Date of Birth: {userToEdit.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {userToEdit.Citizenship}, HealthCare Number: {patientToEdit.HealthCareNumber} " +
                            $", Experation Date: {patientToEdit.ExperationDate}";
                        Thread logger = new Thread(() => LogManager.Instance.WriteLog(updatePat));
                        logger.Start();

                        isChanged = true;

                        return patient;
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
        /// Deletes Patient user
        /// </summary>
        /// <param name="userID">the Patient that is being deleted</param>
        public void DeletePatient(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    for (int i = 0; i < GetAllPatients().Count; i++)
                    {
                        if (GetAllPatients().ToList()[i].UserID == userID)
                        {
                            tblClinicPatient pat = (from r in context.tblClinicPatients where r.UserID == userID select r).First();

                            string patDel = $"Deleted Patient {GetAllPatients()[i].FirstName} {GetAllPatients()[i].LastName}, " +
                                $"Identification Card: {GetAllPatients()[i].IdentificationCard}, " +
                                $"Gender: {GetAllPatients()[i].Gender}, Date of Birth: {GetAllPatients()[i].DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {GetAllPatients()[i].Citizenship}";
                            Thread logger = new Thread(() => LogManager.Instance.WriteLog(patDel));
                            logger.Start();

                            context.tblClinicPatients.Remove(pat);
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

        /// <summary>
        /// Get patient count from the database
        /// </summary>
        /// <returns>Total amount of patients</returns>
        public int CountPatients()
        {
            int count = 0;
            try
            {
                List<vwClinicPatient> list = new List<vwClinicPatient>();
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    list = (from x in context.vwClinicPatients select x).ToList();                 
                }

                for (int i = 0; i < list.Count; i++)
                {
                    count++;
                }

                return count;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return 0;
            }
        }
    }
}
