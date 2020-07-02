using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EditReportViewModel : BaseViewModel
    {
        EditReportView editReportView;
        Validation validation = new Validation();
        Reports reports = new Reports();

        public vwReport CheckIsReportChanged { get; set; }

        private vwReport report;

        public vwReport Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

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

        public EditReportViewModel(EditReportView editReportView, vwReport reportToEdit)
        {
            this.editReportView = editReportView;
            this.report = reportToEdit;
            //gets reports initial values before editing
            CheckIsReportChanged = new vwReport
            {
                Date = reportToEdit.Date,
                Project = reportToEdit.Project,
                Hours = reportToEdit.Hours
            };
        }
        /// <summary>
        /// This method invokes a methods for editing report.
        /// </summary>
        public void SaveExecute()
        {
            try
            {
                reports.EditReport(report);
                editReportView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>       
        /// This method checks if report data is changed. If changed, checks if user input data is valid.
        /// </summary>
        /// <returns>True if data is changed and valid, false if not.</returns>  
        public bool CanSaveExecute()
        {
            DateTime date = DateTime.Now;
            try
            {
                //checks if user input data changed and valid               
                if ((report.Date != CheckIsReportChanged.Date || report.Project != CheckIsReportChanged.Project || report.Hours != CheckIsReportChanged.Hours)
                     &&
                     !String.IsNullOrEmpty(report.Date.ToString()) && !String.IsNullOrEmpty(report.Project) && Int32.TryParse(report.Hours.ToString(), out int number) && number > 0 && number <= 12
                     && validation.ValidationForReportsNumberPerDay(report.EmployeeID, report.Date) == true)
                {
                    if (validation.ValidationForHours(report.EmployeeID, report.ReportID, report.Date, report.Hours) == true)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for closing window of editing report.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                editReportView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }
    }
}

