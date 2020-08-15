using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All doctor crud operations
    /// </summary>
    class DoctorData
    {
        /// <summary>
        /// Data from the user
        /// </summary>
        UserData userData = new UserData();
        /// <summary>
        /// Data from the patient
        /// </summary>
        PatientData patData = new PatientData();

        /// <summary>
        /// Check if data is changed
        /// </summary>
        public static bool isChanged = false;

        /// <summary>
        /// Get all data about doctors from the database
        /// </summary>
        /// <returns>The list of all doctors</returns>
        public List<vwClinicDoctor> GetAllDoctors()
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<vwClinicDoctor> list = new List<vwClinicDoctor>();
                    list = (from x in context.vwClinicDoctors select x).ToList();
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
        /// Get all data about patients from logged in doctors from the database
        /// </summary>
        /// <param name="userID">id of the logged in doctor</param>
        /// <returns>The list of all doctors</returns>
        public List<vwClinicPatient> GetAllPatientsFromDoctors(int userID)
        {
            vwClinicDoctor doctor = GetAllDoctors().Where(doc => doc.UserID == userID).FirstOrDefault();

            List<vwClinicPatient> list = new List<vwClinicPatient>();
            for (int i = 0; i < patData.GetAllPatients().Count; i++)
            {
                if (patData.GetAllPatients()[i].UniqueNumber == doctor.UniqueNumber)
                {
                    list.Add(patData.GetAllPatients()[i]);
                }
            }
            return list;
        }

        /// <summary>
        /// Creates or edits an doctor
        /// </summary>
        /// <param name="doctor">the doctor that is being added</param>
        /// <returns>a new or edited doctor</returns>
        public vwClinicDoctor AddDoctor(vwClinicDoctor doctor)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (doctor.DoctorID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = doctor.FirstName,
                            LastName = doctor.LastName,
                            IdentificationCard = doctor.IdentificationCard,
                            Gender = doctor.Gender,
                            DateOfBirth = doctor.DateOfBirth,
                            Citizenship = doctor.Citizenship,
                            Username = doctor.Username,
                            UserPassword = doctor.UserPassword
                        };

                        userData.AddUser(newUser);

                        tblClinicDoctor newDoctor = new tblClinicDoctor
                        {
                            UniqueNumber = doctor.UniqueNumber,
                            BankAccount = doctor.BankAccount,
                            Department = doctor.Department,
                            WorkingShift = doctor.WorkingShift,
                            ReceivingPatient = doctor.ReceivingPatient,
                            ManagerID = doctor.ManagerID,
                            UserID = newUser.UserID
                        };

                        context.tblClinicDoctors.Add(newDoctor);
                        context.SaveChanges();
                        doctor.DoctorID = newDoctor.DoctorID;

                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog($"Created Doctor {doctor.FirstName} {doctor.LastName}, Identification Card: {doctor.IdentificationCard}, " +
                            $"Gender: {doctor.Gender}, Date of Birth: {doctor.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {doctor.Citizenship}, Unique Number: {doctor.UniqueNumber} " +
                            $", Bank Account: {doctor.BankAccount}, Department: {doctor.Department}, Working Shift: {doctor.WorkingShift}, Receiving Patient: {doctor.ReceivingPatient}"));
                        logger.Start();

                        return doctor;
                    }
                    else
                    {
                        tblUser userToEdit = new tblUser
                        {
                            FirstName = doctor.FirstName,
                            LastName = doctor.LastName,
                            IdentificationCard = doctor.IdentificationCard,
                            Gender = doctor.Gender,
                            DateOfBirth = doctor.DateOfBirth,
                            Citizenship = doctor.Citizenship,
                            Username = doctor.Username,
                            UserPassword = doctor.UserPassword,
                            UserID = doctor.UserID
                        };
                        userData.AddUser(userToEdit);

                        tblClinicDoctor doctorToEdit = (from ss in context.tblClinicDoctors where ss.UserID == doctor.UserID select ss).First();

                        doctorToEdit.UniqueNumber = doctor.UniqueNumber;
                        doctorToEdit.BankAccount = doctor.BankAccount;
                        doctorToEdit.Department = doctor.Department;
                        doctorToEdit.WorkingShift = doctor.WorkingShift;
                        doctorToEdit.ReceivingPatient = doctor.ReceivingPatient;
                        doctorToEdit.ManagerID = doctor.ManagerID;
                        doctorToEdit.UserID = userToEdit.UserID;
                        doctorToEdit.DoctorID = doctor.DoctorID;

                        context.SaveChanges();

                        Thread logger = new Thread(() =>
                            LogManager.Instance.WriteLog($"Updated Doctor {userToEdit.FirstName} {userToEdit.LastName}, Identification Card: {userToEdit.IdentificationCard}, " +
                            $"Gender: {userToEdit.Gender}, Date of Birth: {userToEdit.DateOfBirth.ToString("dd.MM.yyyy")}, Citizenship: {userToEdit.Citizenship}, Unique Number: {doctorToEdit.UniqueNumber} " +
                            $", Bank Account: {doctorToEdit.BankAccount}, Department: {doctorToEdit.Department}, Working Shift: {doctorToEdit.WorkingShift}, Receiving Patient: {doctorToEdit.ReceivingPatient}"));
                        logger.Start();

                        isChanged = true;

                        return doctor;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                Thread logger = new Thread(() => LogManager.Instance.WriteLog("Failed to update or create Doctor Client"));
                logger.Start();
                return null;
            }
        }

        /// <summary>
        /// Deletes Doctor user
        /// </summary>
        /// <param name="userID">the Doctor that is being deleted</param>
        public void DeleteDoctor(int userID)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    for (int i = 0; i < GetAllDoctors().Count; i++)
                    {
                        if (GetAllDoctors().ToList()[i].UserID == userID)
                        {
                            tblClinicDoctor doc = (from r in context.tblClinicDoctors where r.UserID == userID select r).First();
                            context.tblClinicDoctors.Remove(doc);
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
        /// Checks how many doctors are currently available
        /// </summary>
        /// <param name="doc">list of doctors</param>
        /// <returns>the amount of doctors that are available</returns>
        public int AvailableDoctors(List<vwClinicDoctor> doc)
        {
            int count = 0;

            for (int i = 0; i < doc.Count; i++)
            {
                if (doc[i].ReceivingPatient == true)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
