﻿using Nedeljni_II_Kristina_Garcia_Francisco.Commands;
using Nedeljni_II_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using Nedeljni_II_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_II_Kristina_Garcia_Francisco.ViewModel
{
    class PatientViewModel : BaseViewModel
    {
        PatientWindow patientWindow;
        /// <summary>
        /// Background worker
        /// </summary>
        private readonly BackgroundWorker bgWorker = new BackgroundWorker();
        private bool _isRunning = false;
        /// <summary>
        /// Checks if th patient could be sick
        /// </summary>
        private bool possibleVirus = false;
        /// <summary>
        /// Numbper of requests
        /// </summary>
        private int reaquestAmount = 0;
        /// <summary>
        /// Checks if th patient is sick
        /// </summary>
        private int counter = 0;

        #region Constructor
        /// <summary>
        /// Constructor with Patient Window param
        /// </summary>
        /// <param name="patientOpen">opens the patient window</param>
        public PatientViewModel(PatientWindow patientOpen)
        {
            patientWindow = patientOpen;
            bgWorker.DoWork += WorkerOnDoWork;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.ProgressChanged += WorkerOnProgressChanged;
            bgWorker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
            IsSickLabel = "Check for Symptoms";
            ProgressBarVisibility = Visibility.Collapsed;
        }
        #endregion

        #region Property
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

        /// <summary>
        /// Exam Info label
        /// </summary>
        private string examInfoLabel;
        public string ExamInfoLabel
        {
            get
            {
                return examInfoLabel;
            }
            set
            {
                examInfoLabel = value;
                OnPropertyChanged("ExamInfoLabel");
            }
        }

        /// <summary>
        /// Exam IsSick Label
        /// </summary>
        private string isSickLabel;
        public string IsSickLabel
        {
            get
            {
                return isSickLabel;
            }
            set
            {
                isSickLabel = value;
                OnPropertyChanged("IsSickLabel");
            }
        }

        /// <summary>
        /// The progress bar property
        /// </summary>
        private int currentProgress;
        public int CurrentProgress
        {
            get
            {
                return currentProgress;
            }
            set
            {
                if (currentProgress != value)
                {
                    currentProgress = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }
        }

        /// <summary>
        /// The progress bar property
        /// </summary>
        private Visibility progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get
            {
                return progressBarVisibility;
            }
            set
            {
                progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }
        #endregion

        #region Background worker
        /// <summary>
        /// Updates the progress bar and prints the value
        /// </summary>
        /// <param name="sender">objecy sender</param>
        /// <param name="e">progress changed event</param>
        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
            ExamInfoLabel = counter + " seconds";
        }

        /// <summary>
        /// Method that the background worker executes
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">do work event</param>
        private void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            Random rng = new Random();
            counter = 0;
            // Restart progress
            bgWorker.ReportProgress(0);

            if (reaquestAmount != 2)
            {
                for (int i = 1; i <= 6; i++)
                {
                    Thread.Sleep(1000);
                    // Calling ReportProgress() method raises ProgressChanged event
                    // To this method pass the percentage of processing that is complete
                    counter++;
                    bgWorker.ReportProgress(100 / 5 * i);
                    if (counter == 5)
                    {
                        break;
                    }
                }

                // Checking if the patient is sick
                int value = rng.Next(0, 2);
                if (value == 1)
                {
                    possibleVirus = true;
                    reaquestAmount++;
                }
                else
                {
                    possibleVirus = false;
                    reaquestAmount = 0;
                }
            }
            else if (reaquestAmount == 2)
            {
                // Random time between 1 and 7
                int value = rng.Next(1, 8);

                for (int i = 1; i <= value+1; i++)
                {
                    Thread.Sleep(1000);
                    // Calling ReportProgress() method raises ProgressChanged event
                    // To this method pass the percentage of processing that is complete
                    counter++;
                    if (i == value)
                    {
                        // 100% if all seconds passed                   
                        bgWorker.ReportProgress(100);
                    }
                    else
                    {
                        bgWorker.ReportProgress(100 / value * i);
                    }

                    if (counter == value)
                    {
                        break;
                    }
                }
            }

            ExamInfoLabel = "";
            _isRunning = false;

            // Cancel the asynchronous operation if still in progress
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Print the appropriate message depending how the worker finished.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">worker completed event</param>
        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ExamInfoLabel = e.Error.Message;
                _isRunning = false;
            }
            else if (counter == 5)
            {
                _isRunning = false;

                if (possibleVirus == true && reaquestAmount < 2)
                {
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Finished patients exam request, a virus was detected";
                    IsSickLabel = "Exam Request";
                }
                else if (possibleVirus == true && reaquestAmount == 2)
                {
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Finished patients exam request, virus was again detected. Doing physical examination";

                    HealthExam hm = new HealthExam();
                    Thread foundSymptom = new Thread(() => hm.WriteToFile(LoggedInUser.CurrentUser));
                    foundSymptom.Start();

                    if (!bgWorker.IsBusy)
                    {
                        // This method will start the execution asynchronously in the background
                        bgWorker.RunWorkerAsync();
                        _isRunning = true;
                    }
                }
                else
                {
                    IsSickLabel = "Check for Symptoms";
                    InfoLabelBG = "#28a745";
                    InfoLabel = "Finished patients exam request, patient is healthy";
                }
            }
            else
            {
                InfoLabelBG = "#28a745";
                InfoLabel = "Finished patients physical examination";
                IsSickLabel = "Check for Symptoms";

                _isRunning = false;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Request Exam button
        /// </summary>
        private ICommand request;
        public ICommand Request
        {
            get
            {
                if (request == null)
                {
                    request = new RelayCommand(param => RequestExecute(), param => CanRequestExecute());
                }
                return request;
            }
        }

        /// <summary>
        /// Check if the button has the needed requirements to execute
        /// </summary>
        /// <returns>return true if it has the needed requirements to execute</returns>
        private bool CanRequestExecute()
        {
            return true;
        }

        /// <summary>
        /// Start the request once the button is pressed
        /// </summary>
        private void RequestExecute()
        {
            if (!bgWorker.IsBusy)
            {
                ProgressBarVisibility = Visibility.Visible;
                // This method will start the execution asynchronously in the background
                bgWorker.RunWorkerAsync();
                _isRunning = true;
                InfoLabelBG = "#17a2b8";
                InfoLabel = "Checking for virus symptoms";
            }
            else
            {
                InfoLabelBG = "#ffc107";
                InfoLabel = "Busy processing the request, please wait.";
            }
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
                patientWindow.Close();
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
