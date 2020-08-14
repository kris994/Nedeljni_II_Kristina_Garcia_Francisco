using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class MaintenanceViewModel : BaseViewModel
    {
        MaintenanceData mainData = new MaintenanceData();
        MaintenanceReportData reportData = new MaintenanceReportData();
        MaintenanceWindow maintenanaceWindow;

        #region Constructor
        /// <summary>
        /// Constructor with the maintenanace info window opening
        /// </summary>
        /// <param name="maintenanacerWindowOpen">opends the maintenanace window</param>
        public MaintenanceViewModel(MaintenanceWindow maintenanaceWindowOpen)
        {
            maintenanaceWindow = maintenanaceWindowOpen;
            MaintenanceList = mainData.GetAllMaintenances();
            MaintenanceReportList = reportData.GetAllReports().ToList();
            MaintenanceReportCurrentUserList = reportData.GetAllUserReports(LoggedInUser.CurrentUser.UserID).ToList();
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
        /// List of current user reports
        /// </summary>
        private List<MaintenanceReport> maintenanceReportCurrentUserList;
        public List<MaintenanceReport> MaintenanceReportCurrentUserList
        {
            get
            {
                return maintenanceReportCurrentUserList;
            }
            set
            {
                maintenanceReportCurrentUserList = value;
                OnPropertyChanged("MaintenanceReportCurrentUserList");
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
        /// Info label
        /// </summary>
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }

        /// <summary>
        /// Info label background
        /// </summary>
        private string infoLabelBG;
        public string InfoLabelBG
        {
            get
            {
                return infoLabelBG;
            }
            set
            {
                infoLabelBG = value;
                OnPropertyChanged("InfoLabelBG");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to add a new maintenance report
        /// </summary>
        private ICommand addNewMaintenanceReport;
        public ICommand AddNewMaintenanceReport
        {
            get
            {
                if (addNewMaintenanceReport == null)
                {
                    addNewMaintenanceReport = new RelayCommand(param => AddNewMaintenanceReportExecute(), param => CanAddNewMaintenanceReportExecute());
                }
                return addNewMaintenanceReport;
            }
        }

        /// <summary>
        /// Executes the add maintenance command
        /// </summary>
        private void AddNewMaintenanceReportExecute()
        {
            try
            {
                AddMaintenanceReportWindow addReportWindow = new AddMaintenanceReportWindow();
                addReportWindow.ShowDialog();
                if ((addReportWindow.DataContext as AddMaintenanceReportViewModel).IsUpdateMaintenanceReport == true)
                {
                    MaintenanceReportList = reportData.GetAllReports().ToList();
                    MaintenanceReportCurrentUserList = reportData.GetAllUserReports(LoggedInUser.CurrentUser.UserID).ToList();
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Successfully created a new Report";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new report
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewMaintenanceReportExecute()
        {
            return true;
        }       

        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                maintenanaceWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }
        #endregion
    }
}

