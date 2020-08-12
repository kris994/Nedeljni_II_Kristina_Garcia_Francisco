using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All doctor crud operations
    /// </summary>
    class DoctorData
    {
        UserData userData = new UserData();

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

                        return doctor;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == doctor.UserID select ss).First();
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
                        return doctor;
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
