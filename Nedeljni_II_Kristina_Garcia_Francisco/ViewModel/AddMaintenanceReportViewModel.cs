using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class AddMaintenanceReportViewModel : BaseViewModel
    {
        AddMaintenanceReportWindow addReport;
        MaintenanceReportData reportData = new MaintenanceReportData();
        MaintenanceData mainData = new MaintenanceData();

        #region Constructor
        /// <summary>
        /// Constructor with the add report info window opening
        /// </summary>
        /// <param name="addMaintenanceReportWindowOpen">opends the add report window</param>
        public AddMaintenanceReportViewModel(AddMaintenanceReportWindow addMaintenanceReportWindowOpen)
        {
            maintenanceReport = new MaintenanceReport();
            addReport = addMaintenanceReportWindowOpen;
            MaintenanceReportList = reportData.GetAllReports().ToList();
            MaintenanceList = mainData.GetAllMaintenances();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of reports
        /// </summary>
        private List<MaintenanceReport> maintenanceReportList;
        public List<MaintenanceReport> MaintenanceReportList
        {
            get
            {
                return maintenanceReportList;
            }
            set
            {
                maintenanceReportList = value;
                OnPropertyChanged("MaintenanceReportList");
            }
        }

        /// <summary>
        /// List of maintenance users
        /// </summary>
        private Queue<vwClinicMaintenance> maintenanceList;
        public Queue<vwClinicMaintenance> MaintenanceList
        {
            get
            {
                return maintenanceList;
            }
            set
            {
                maintenanceList = value;
                OnPropertyChanged("MaintenanceList");
            }
        }

        /// <summary>
        /// Specific maintenance report
        /// </summary>
        private MaintenanceReport maintenanceReport;
        public MaintenanceReport MaintenanceReport
        {
            get
            {
                return maintenanceReport;
            }
            set
            {
                maintenanceReport = value;
                OnPropertyChanged("MaintenanceReport");
            }
        }

        /// <summary>
        /// Specific maintenance
        /// </summary>
        private vwClinicMaintenance maintenance;
        public vwClinicMaintenance Maintenance
        {
            get
            {
                return maintenance;
            }
            set
            {
                maintenance = value;
                OnPropertyChanged("Maintenance");
            }
        }

        /// <summary>
        /// Cheks if the Maintenance Report was added or updated
        /// </summary>
        private bool isUpdateMaintenanceReport;
        public bool IsUpdateMaintenanceReport
        {
            get
            {
                return isUpdateMaintenanceReport;
            }
            set
            {
                isUpdateMaintenanceReport = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new Maintenance Report
        /// </summary>
        private ICommand saveMaintenanceReport;
        public ICommand SaveMaintenanceReport
        {
            get
            {
                if (saveMaintenanceReport == null)
                {
                    saveMaintenanceReport = new RelayCommand(param => SaveMaintenanceReportExecute(), param => CanSaveMaintenanceReportExecute());
                }
                return saveMaintenanceReport;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveMaintenanceReportExecute()
        {
            try
            {
                MaintenanceReport.UserID = LoggedInUser.CurrentUser.UserID;
                reportData.AddReportMaintenance(MaintenanceReport);
                IsUpdateMaintenanceReport = true;

                addReport.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the report
        /// </summary>
        protected bool CanSaveMaintenanceReportExecute()
        {
            if (MaintenanceReport.TotalHours <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }        
        }

        /// <summary>
        /// Command that closes the report window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addReport.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
