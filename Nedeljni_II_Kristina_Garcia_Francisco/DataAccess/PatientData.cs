using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{   
    /// <summary>
     /// All patient crud operations
     /// </summary>
    class PatientData
    {
        UserData userData = new UserData();

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

                        return patient;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == patient.UserID select ss).First();
                        userData.AddUser(userToEdit);

                        tblClinicPatient patientToEdit = (from ss in context.tblClinicPatients where ss.UserID == patient.UserID select ss).First();

                        patientToEdit.UniqueNumber = patient.UniqueNumber;
                        patientToEdit.HealthCareNumber = patient.HealthCareNumber;
                        patientToEdit.ExperationDate = patient.ExperationDate;
                        patientToEdit.UserID = userToEdit.UserID;
                        patientToEdit.PatientID = patient.PatientID;

                        context.SaveChanges();
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
    }
}
