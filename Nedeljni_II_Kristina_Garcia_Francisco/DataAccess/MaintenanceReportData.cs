using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// All Maintenance report crud operations
    /// </summary>
    class MaintenanceReportData
    {
        /// <summary>
        /// Get all data about reports from the file
        /// </summary>
        /// <returns>The list of all reports</returns>
        public List<MaintenanceReport> GetAllReports()
        {
            string file = LoggedInUser.CurrentUser.Username + ".txt";
            List<MaintenanceReport> list = new List<MaintenanceReport>();

            // Load this only if the file exists
            if (!File.Exists(@"~\..\..\..\TextFiles\" + file))
            {
                File.Create(@"~\..\..\..\TextFiles\" + file).Close();
            }

            string[] readFile = File.ReadAllLines(@"~\..\..\..\TextFiles\" + file);

            for (int i = 0; i < readFile.Length; i++)
            {
                if (!string.IsNullOrEmpty(readFile[i]))
                {
                    string[] trim = readFile[i].Split('|');
                    MaintenanceReport mr = new MaintenanceReport
                    {
                        Date = trim[0],
                        TotalHours = int.Parse(trim[1]),
                        ShortDescription = trim[2],
                        UserID = int.Parse(trim[3])
                    };

                    list.Add(mr);
                }
            }
            return list;
        }

        /// <summary>
        /// Get all data about reports from loggedInUser
        /// </summary>
        /// <param name="userId">id of the logged in user</param>
        /// <returns>The list of all yser reports</returns>
        public List<MaintenanceReport> GetAllUserReports(int userId)
        {           
            List<MaintenanceReport> list = new List<MaintenanceReport>();
            for (int i = 0; i < GetAllReports().Count; i++)
            {
                if(GetAllReports()[i].UserID == userId)
                {
                    list.Add(GetAllReports()[i]);
                }
            }
            
            return list;
        }

        /// <summary>
        /// Saves Report to a file
        /// </summary>
        /// <param name="report">the report that is being saved</param>
        /// <returns>the report</returns>
        public MaintenanceReport AddReportMaintenance(MaintenanceReport report)
        {
            string file = LoggedInUser.CurrentUser.Username + ".txt";
            DateTime dt = DateTime.Parse(report.Date);
            string date = dt.ToString("dd.MM.yyyy");

            using (StreamWriter sw = new StreamWriter(@"~\..\..\..\TextFiles\" + file, append: true))
            {
                sw.WriteLine($"{date}|{report.TotalHours}|{report.ShortDescription}|{report.UserID}");
            }

            GetAllReports().Add(report);

            Thread logger = new Thread(() => LogManager.Instance.WriteLog($"Crated a new report {date}, {report.TotalHours}, {report.ShortDescription}"));
            logger.Start();

            return report;
        }
    }
}
