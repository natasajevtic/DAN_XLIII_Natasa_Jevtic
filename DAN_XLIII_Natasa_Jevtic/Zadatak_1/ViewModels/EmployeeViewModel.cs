using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EmployeeViewModel : BaseViewModel
    {
        EmployeeView employeeView;
        Reports reports = new Reports();

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

        private List<vwReport> reportList;
        public List<vwReport> ReportList
        {
            get
            {
                return reportList;
            }
            set
            {
                reportList = value;
                OnPropertyChanged("ReportList");
            }
        }
        private ICommand addReport;

        public ICommand AddReport
        {
            get
            {
                if (addReport == null)
                {
                    addReport = new RelayCommand(param => AddReportExecute(), param => CanAddReportExecute());
                }
                return addReport;
            }
        }

        public EmployeeViewModel(EmployeeView employeeView, vwEmployee employee)
        {
            this.employeeView = employeeView;
            this.employee = employee;
            ReportList = reports.GetAllEmployeeReports(employee.EmployeeID);
        }

        public void AddReportExecute()
        {
            try
            {
                AddReportView addReport = new AddReportView(Employee);
                addReport.ShowDialog();
                ReportList = reports.GetAllEmployeeReports(employee.EmployeeID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanAddReportExecute()
        {
            return true;
        }
    }
}

