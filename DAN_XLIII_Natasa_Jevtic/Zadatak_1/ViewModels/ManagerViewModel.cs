using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class ManagerViewModel : BaseViewModel
    {
        Employees employees = new Employees();
        Reports reports = new Reports();

        ManagerView managerView;

        private vwManager manager;

        public vwManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
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
                OnPropertyChanged("Employee");
            }
        }

        private List<vwEmployee> employeeList;

        public List<vwEmployee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

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
                OnPropertyChanged("Report");
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

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        private Visibility viewButtonAddEmployee;
        public Visibility ViewButtonAddEmployee
        {
            get
            {
                return viewButtonAddEmployee;
            }
            set
            {
                viewButtonAddEmployee = value;
                OnPropertyChanged("ViewButtonAddEmployee");
            }
        }

        private Visibility viewButtonDeleteEmployee;
        public Visibility ViewButtonDeleteEmployee
        {
            get
            {
                return viewButtonDeleteEmployee;
            }
            set
            {
                viewButtonDeleteEmployee = value;
                OnPropertyChanged("ViewButtonDeleteEmployee");
            }
        }

        private Visibility viewButtonEditEmployee;
        public Visibility ViewButtonEditEmployee
        {
            get
            {
                return viewButtonEditEmployee;
            }
            set
            {
                viewButtonEditEmployee = value;
                OnPropertyChanged("ViewButtonEditEmployee");
            }
        }

        private Visibility isVisibleEmployeeData = Visibility.Collapsed;
        public Visibility IsVisibleEmployeeData
        {
            get
            {
                return isVisibleEmployeeData;
            }
            set
            {
                isVisibleEmployeeData = value;
                OnPropertyChanged("IsVisibleEmployeeData");
            }
        }

        private ICommand deleteEmployee;

        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }

        }

        private ICommand editEmployee;

        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        private ICommand addEmployee;

        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddEmployeeExecute(), param => CanAddEmployeeExecute());
                }
                return addEmployee;
            }
        }

        private ICommand viewEmployee;

        public ICommand ViewEmployee
        {
            get
            {
                if (viewEmployee == null)
                {
                    viewEmployee = new RelayCommand(param => ViewEmployeeExecute(), param => CanViewEmployeeExecute());
                }
                return viewEmployee;
            }
        }

        private ICommand viewAllEmployees;

        public ICommand ViewAllEmployees
        {
            get
            {
                if (viewAllEmployees == null)
                {
                    viewAllEmployees = new RelayCommand(param => ViewAllEmployeesExecute(), param => CanViewAllEmployeesExecute());
                }
                return viewAllEmployees;
            }
        }

        private Visibility isVisibleReportData = Visibility.Collapsed;
        public Visibility IsVisibleReportData
        {
            get
            {
                return isVisibleReportData;
            }
            set
            {
                isVisibleReportData = value;
                OnPropertyChanged("IsVisibleReportData");
            }
        }

        private Visibility viewButtonDeleteReport;
        public Visibility ViewButtonDeleteReport
        {
            get
            {
                return viewButtonDeleteReport;
            }
            set
            {
                viewButtonDeleteReport = value;
                OnPropertyChanged("ViewButtonDeleteReport");
            }
        }

        private Visibility viewButtonEditReport;
        public Visibility ViewButtonEditReport
        {
            get
            {
                return viewButtonEditReport;
            }
            set
            {
                viewButtonEditReport = value;
                OnPropertyChanged("ViewButtonEditReport");
            }
        }

        private ICommand viewAllReports;

        public ICommand ViewAllReports
        {
            get
            {
                if (viewAllReports == null)
                {
                    viewAllReports = new RelayCommand(param => ViewAllReportsExecute(), param => CanViewAllReportsExecute());
                }
                return viewAllReports;
            }
        }

        private ICommand deleteReport;

        public ICommand DeleteReport
        {
            get
            {
                if (deleteReport == null)
                {
                    deleteReport = new RelayCommand(param => DeleteReportExecute(), param => CanDeleteReportExecute());
                }
                return deleteReport;
            }

        }

        private ICommand editReport;

        public ICommand EditReport
        {
            get
            {
                if (editReport == null)
                {
                    editReport = new RelayCommand(param => EditReportExecute(), param => CanEditReportExecute());
                }
                return editReport;
            }
        }

        public ManagerViewModel(ManagerView managerView, vwManager manager)
        {
            this.managerView = managerView;
            this.manager = manager;
            SetVisibilityBasedOnSector(manager);
        }

        public void SetVisibilityBasedOnSector(vwManager manager)
        {
            if (manager.AccessLevel == "read-only")
            {
                ViewButtonAddEmployee = Visibility.Hidden;
                ViewButtonDeleteEmployee = Visibility.Hidden;
                ViewButtonEditEmployee = Visibility.Hidden;
            }
            if (manager.Sector == "FINANCIAL")
            {
                ViewButtonDeleteEmployee = Visibility.Collapsed;
                ViewButtonEditEmployee = Visibility.Collapsed;
            }
        }

        public void ViewAllEmployeesExecute()
        {
            IsVisibleReportData = Visibility.Hidden;
            IsVisibleEmployeeData = Visibility.Visible;
            EmployeeList = employees.GetEmployees();
        }
        public bool CanViewAllEmployeesExecute()
        {
            return true;
        }
        public void ViewEmployeeExecute()
        {
            IsVisibleReportData = Visibility.Hidden;
            IsVisibleEmployeeData = Visibility.Visible;
            EmployeeList = employees.FindEmployee(Username);
        }
        public bool CanViewEmployeeExecute()
        {
            if (!String.IsNullOrEmpty(Username))
            {
                if (employees.IsEmployeeExists(Username) == true)
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

        public void DeleteEmployeeExecute()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete the employee?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (Employee != null)
                    {
                        //invokes method to delete employee
                        employees.DeleteEmployee(Employee.EmployeeID);
                        //invokes method to update list of users
                        EmployeeList = employees.GetEmployees();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanDeleteEmployeeExecute()
        {
            return true;
        }

        public void EditEmployeeExecute()
        {
            try
            {
                EditEmployeeView editUser = new EditEmployeeView(Employee);
                editUser.ShowDialog();
                //invokes method to update a list of users
                EmployeeList = employees.GetEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanEditEmployeeExecute()
        {
            return true;
        }

        public void AddEmployeeExecute()
        {
            try
            {
                AddEmployeeView addEmployee = new AddEmployeeView();
                addEmployee.ShowDialog();
                EmployeeList = employees.GetEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanAddEmployeeExecute()
        {
            return true;
        }

        public void ViewAllReportsExecute()
        {
            IsVisibleEmployeeData = Visibility.Hidden;
            IsVisibleReportData = Visibility.Visible;
            ReportList = reports.GetReports();
        }

        public bool CanViewAllReportsExecute()
        {
            return true;
        }

        public void DeleteReportExecute()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete the report?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (Report != null)
                    {
                        //invokes method to delete report
                        reports.DeleteReport(report.ReportID);
                        //invokes method to update list of reports
                        ReportList = reports.GetReports();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanDeleteReportExecute()
        {
            return true;
        }

        public void EditReportExecute()
        {
            try
            {
                EditReportView editReportView = new EditReportView(Report);
                editReportView.ShowDialog();
                //invokes method to update a list of reports
                ReportList = reports.GetReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanEditReportExecute()
        {
            return true;
        }
    }
}
