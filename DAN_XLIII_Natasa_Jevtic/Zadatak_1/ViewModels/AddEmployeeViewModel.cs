using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Calculations;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddEmployeeViewModel : BaseViewModel
    {
        AddEmployeeView addEmployeeView;
        Calculation calculator = new Calculation();
        Validation validation = new Validation();
        Employees employees = new Employees();

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

        public AddEmployeeViewModel(AddEmployeeView addEmployeeView)
        {
            this.addEmployeeView = addEmployeeView;
            employee = new vwEmployee();
        }

        public void SaveExecute()
        {
            try
            {
                employees.AddEmployee(employee);
                addEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool CanSaveExecute()
        {
            DateTime date = DateTime.Now;
            try
            {
                //checks if user input data valid         
                if (!String.IsNullOrEmpty(employee.Name) && !String.IsNullOrEmpty(employee.Surname) && employee.BankAccountNumber.Length == 18 &&
                    employee.BankAccountNumber.All(Char.IsDigit) && employee.JMBG.Length == 13 && employee.JMBG.All(Char.IsDigit) && !String.IsNullOrEmpty(employee.Email)
                    && Int32.TryParse(employee.Salary.ToString(), out int number) && number > 0 && !String.IsNullOrEmpty(employee.Position) && !String.IsNullOrEmpty(employee.Username) && !String.IsNullOrEmpty(employee.Password)
                    && calculator.CalculateDateOfBirth(employee.JMBG, out date) == true
                    && validation.ValidationForUnique(employee.BankAccountNumber, employee.JMBG, employee.Username, employee.Email) == true)
                {
                    Employee.DateOfBirth = date;
                    if (validation.ValidationForEmail(employee.Email) == true)
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
                addEmployeeView.Close();
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

