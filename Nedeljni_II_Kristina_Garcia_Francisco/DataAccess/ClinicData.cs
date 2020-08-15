using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All clinic crud operations
    /// </summary>
    class ClinicData
    {
        /// <summary>
        /// Get all data about clinic from the database
        /// </summary>
        /// <returns>The list of all clinics</returns>
        public List<tblClinic> GetAllClinics()
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    List<tblClinic> list = new List<tblClinic>();
                    list = (from x in context.tblClinics select x).ToList();
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
        /// Creates or edits a clinic
        /// </summary>
        /// <param name="clinic">the clinic that is being added</param>
        /// <returns>a new or edited clinic</returns>
        public tblClinic AddClinic(tblClinic clinic)
        {
            try
            {
                using (ClinicDBEntities context = new ClinicDBEntities())
                {
                    if (clinic.ClinicID == 0)
                    {
                        tblClinic newClinic = new tblClinic
                        {
                            ClinicName = clinic.ClinicName,
                            CreatingDate = clinic.CreatingDate,
                            ClinicOwner = clinic.ClinicOwner,
                            ClinicAddress = clinic.ClinicAddress,
                            ClinicFloorNumber = clinic.ClinicFloorNumber,
                            RoomsPerFloor = clinic.RoomsPerFloor,
                            Balcony = clinic.Balcony,
                            Garden = clinic.Garden,
                            EmergencyVehicleParkingLoots = clinic.EmergencyVehicleParkingLoots,
                            InvalidVehicleParkingLoots = clinic.InvalidVehicleParkingLoots,
                        };

                        context.tblClinics.Add(newClinic);
                        context.SaveChanges();
                        clinic.ClinicID = newClinic.ClinicID;

                        string addClinic = $"Created Clinic {clinic.ClinicName}, Creation Date {clinic.CreatingDate.ToString("dd.MM.yyyy")}, Owner: {clinic.ClinicOwner}, " +
                            $"Address: {clinic.ClinicAddress}";
                        Thread logger = new Thread(() => LogManager.Instance.WriteLog(addClinic));
                        logger.Start();

                        return clinic;
                    }
                    else
                    {
                        tblClinic clinicToEdit = (from ss in context.tblClinics where ss.ClinicID == clinic.ClinicID select ss).First();
                        clinicToEdit.ClinicName = clinic.ClinicName;
                        clinicToEdit.CreatingDate = clinic.CreatingDate;
                        clinicToEdit.ClinicOwner = clinic.ClinicOwner;
                        clinicToEdit.ClinicAddress = clinic.ClinicAddress;
                        clinicToEdit.ClinicFloorNumber = clinic.ClinicFloorNumber;
                        clinicToEdit.RoomsPerFloor = clinic.RoomsPerFloor;
                        clinicToEdit.Balcony = clinic.Balcony;
                        clinicToEdit.Garden = clinic.Garden;
                        clinicToEdit.EmergencyVehicleParkingLoots = clinic.EmergencyVehicleParkingLoots;
                        clinicToEdit.InvalidVehicleParkingLoots = clinic.InvalidVehicleParkingLoots;

                        context.SaveChanges();

                        string updateClinic = $"Updated Clinic {clinicToEdit.ClinicName}, Owner: {clinicToEdit.ClinicOwner}, " +
                            $"Emergency Vehicle Parking Loots: {clinicToEdit.EmergencyVehicleParkingLoots}, Invalid Vehicle Parking Loots: {clinicToEdit.InvalidVehicleParkingLoots}";
                        Thread logger = new Thread(() => LogManager.Instance.WriteLog(updateClinic));
                        logger.Start();

                        return clinic;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                Thread logger = new Thread(() => LogManager.Instance.WriteLog("Failed to update or create Clinic"));
                logger.Start();
                return null;
            }
        }
    }
}
