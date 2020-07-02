using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Zadatak_1.Models;

namespace Zadatak_1.Validations
{
    class Validation
    {
        /// <summary>
        /// This method checks if email adress is valid.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if valid, false if not.</returns>
        public bool ValidationForEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
        /// <summary>
        /// This method checks if a forwarded bank account, JMBG, username and email unique in database.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="jmbg"></param>
        /// <param name="username"></param>
        /// <param name="email></param>
        /// <returns>True if unique, false if not.</returns>
        public bool ValidationForUnique(string bankAccount, string jmbg, string username, string email)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            var list = employeeList.Where(x => x.JMBG != jmbg).ToList();
            if (list.Any(x => x.BankAccountNumber == bankAccount) || list.Any(x => x.JMBG == jmbg) || list.Any(x => x.Username == username)
                && list.Any(x => x.Email == email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidationForReportsNumberPerDay(int employeeID, DateTime date)
        {
            Reports reports = new Reports();
            List<vwReport> reportList = reports.GetReports();
            List<vwReport> reportsOfEmployee = reportList.Where(x => x.EmployeeID == employeeID && x.Date == date).ToList();
            if (reportsOfEmployee.Count() == 2)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool ValidationForHours(int employeeID, int reportID, DateTime date, int hours)
        {
            Reports reports = new Reports();
            List<vwReport> reportList = reports.GetReports();
            int reportHours = reportList.Where(x => x.EmployeeID == employeeID && x.Date == date && x.ReportID != reportID).Select(x => x.Hours).FirstOrDefault();
            if ((reportHours + hours) > 12)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
