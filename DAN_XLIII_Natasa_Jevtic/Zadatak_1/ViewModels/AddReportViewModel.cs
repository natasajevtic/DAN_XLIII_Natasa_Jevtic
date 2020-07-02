using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddReportViewModel : BaseViewModel
    {
        AddReportView addReportView;
        Validation validation = new Validation();
        Reports reports = new Reports();

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

        private vwEmployee employee;

        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
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

        public AddReportViewModel(AddReportView addReportView, vwEmployee employee)
        {
            this.addReportView = addReportView;
            this.employee = employee;
            report = new vwReport();
            report.EmployeeID = employee.EmployeeID;
        }
        /// <summary>
        /// This method invokes a methods for adding report.
        /// </summary>
        public void SaveExecute()
        {
            try
            {
                reports.AddReport(report);
                addReportView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>       
        /// This method checks if report data is valid.
        /// </summary>
        /// <returns>True if data is and valid, false if not.</returns>  
        public bool CanSaveExecute()
        {
            DateTime date = DateTime.Now;
            try
            {
                //checks if user input data changed and valid               
                if (!String.IsNullOrEmpty(report.Date.ToString()) && !String.IsNullOrEmpty(report.Project) && Int32.TryParse(report.Hours.ToString(), out int number) && number > 0 && number <= 12
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
        public void CancelExecute()
        {
            try
            {
                addReportView.Close();
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

