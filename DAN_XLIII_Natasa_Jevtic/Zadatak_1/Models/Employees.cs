using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Employees
    {
        /// <summary>
        /// This method checks if exist employee with forwarded username and password.
        /// </summary>
        /// <param name="username">Employee username.</param>
        /// <param name="password">Employee password.</param>
        /// <returns>True if exist, false if not.</returns>
        public bool AuthenticateEmployee(string username, string password)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblEmployee employeeToFind = context.tblEmployees.Where(x => x.Username == username).FirstOrDefault();
                    if (employeeToFind != null)
                    {
                        if (employeeToFind.Password == password)
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This methods finds employee based on forwarded username and password.
        /// </summary>
        /// <param name="username">Employee username.</param>
        /// <param name="password">Employee password.</param>
        /// <returns>Employee.</returns>
        public vwEmployee FindEmployee(string username, string password)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    return context.vwEmployees.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method checks if employee exists.
        /// </summary>
        /// <param name="username">Employee username.</param>
        /// <returns>True if employee exist, false if not.</returns>
        public bool IsEmployeeExists(string username)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {

                    vwEmployee sector = context.vwEmployees.Where(x => x.Username == username).FirstOrDefault();
                    if (sector != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method finds employee from DbSet based on forwarded username.
        /// </summary>
        /// <param name="username">Employee username.</param>
        /// <returns>Employee.</returns>
        public List<vwEmployee> FindEmployee(string username)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    return context.vwEmployees.Where(x => x.Username == username).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of all employees (including managers).
        /// </summary>
        /// <returns>List of employees.</returns>
        public List<tblEmployee> GetAllEmployees()
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    List<tblEmployee> employees = new List<tblEmployee>();
                    employees = (from x in context.tblEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        public List<vwEmployee> GetEmployees()
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    employees = (from x in context.vwEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method deletes employee from DbSet and then saves changes to database.
        /// </summary>
        /// <param name="employeeID">ID of employee.</param>
        public void DeleteEmployee(int employeeID)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    List<tblReport> reports = context.tblReports.Where(x => x.EmployeeID == employeeID).ToList();
                    if (reports.Count() > 0)
                    {
                        foreach (var report in reports)
                        {
                            context.tblReports.Remove(report);
                            context.SaveChanges();
                        }
                    }
                    tblEmployee employeeToDelete = context.tblEmployees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
                    context.tblEmployees.Remove(employeeToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method adds employee to DbSet and then save changes to database.
        /// </summary>
        /// <param name="employeeToAdd">Employee.</param>
        public void AddEmployee(vwEmployee employeeToAdd)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblEmployee employee = new tblEmployee
                    {
                        Name = employeeToAdd.Name,
                        Surname = employeeToAdd.Surname,
                        DateOfBirth = employeeToAdd.DateOfBirth,
                        JMBG = employeeToAdd.JMBG,
                        BankAccountNumber = employeeToAdd.BankAccountNumber,
                        Email = employeeToAdd.Email,
                        Salary = employeeToAdd.Salary,
                        Position = employeeToAdd.Position,
                        Username = employeeToAdd.Username,
                        Password = employeeToAdd.Password
                    };
                    context.tblEmployees.Add(employee);
                    context.SaveChanges();
                    employeeToAdd.EmployeeID = employee.EmployeeID;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method edits employee in DbSet and then saves changes to database.
        /// </summary>
        /// <param name="employee">Employee to edit.</param>
        /// <returns>Edited employee.</returns>
        public vwEmployee EditEmployee(vwEmployee employee)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblEmployee employeeToEdit = context.tblEmployees.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
                    employeeToEdit.Name = employee.Name;
                    employeeToEdit.Surname = employee.Surname;
                    employeeToEdit.JMBG = employee.JMBG;
                    employeeToEdit.BankAccountNumber = employee.BankAccountNumber;
                    employeeToEdit.Email = employee.Email;
                    employeeToEdit.Salary = employee.Salary;
                    employeeToEdit.Position = employee.Position;
                    employeeToEdit.Username = employee.Username;
                    employeeToEdit.Password = employee.Password;
                    context.SaveChanges();
                    return employee;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
