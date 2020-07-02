using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Models
{
    class Managers : Logger
    {
        /// <summary>
        /// This method finds manager based on forwarded username and password.
        /// </summary>
        /// <param name="username">Employee username.</param>
        /// <param name="password">Employee password.</param>
        /// <returns>Manager.</returns>
        public vwManager FindManager(string username, string password)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    return context.vwManagers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method created list of sectors.
        /// </summary>
        /// <returns>List of sectors.</returns>
        public List<string> GetSectors()
        {
            return new List<string> { "HR", "FINANCIAL", "R&D" };
        }
        /// <summary>
        /// This method creates list of access level.
        /// </summary>
        /// <returns>List of access level.</returns>
        public List<string> GetAccessLevel()
        {
            return new List<string> { "modify", "read-only" };
        }
        /// <summary>
        /// This method adds managers to DbSet and then save changes to database.
        /// </summary>
        /// <param name="managerToAdd">Employee.</param>
        public void AddManager(vwManager managerToAdd)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblEmployee employee = new tblEmployee
                    {
                        Name = managerToAdd.Name,
                        Surname = managerToAdd.Surname,
                        DateOfBirth = managerToAdd.DateOfBirth,
                        JMBG = managerToAdd.JMBG,
                        BankAccountNumber = managerToAdd.BankAccountNumber,
                        Email = managerToAdd.Email,
                        Salary = managerToAdd.Salary,
                        Position = managerToAdd.Position,
                        Username = managerToAdd.Username,
                        Password = managerToAdd.Password,
                        Sector = managerToAdd.Sector,
                        AccessLevel = managerToAdd.AccessLevel
                    };
                    context.tblEmployees.Add(employee);
                    context.SaveChanges();
                    managerToAdd.EmployeeID = employee.EmployeeID;
                    LogAction("Manager " + managerToAdd.Name + " " + managerToAdd.Surname + " created. Date of birth: " + managerToAdd.DateOfBirth + ", Position: " + managerToAdd.Position + ", Email: " + managerToAdd.Email
                        + ", Sector: " + managerToAdd.Sector);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
