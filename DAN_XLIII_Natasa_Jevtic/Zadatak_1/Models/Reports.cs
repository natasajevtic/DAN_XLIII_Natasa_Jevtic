using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Reports
    {
        /// <summary>
        /// This methods creates a list of data from view of reports.
        /// </summary>
        /// <returns></returns>
        public List<vwReport> GetReports()
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    List<vwReport> reports = new List<vwReport>();
                    reports = (from x in context.vwReports select x).ToList();
                    return reports;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method deletes report from DbSet and saves changes to database.
        /// </summary>
        /// <param name="reportID"></param>
        public void DeleteReport(int reportID)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblReport reportToDelete = context.tblReports.Where(x => x.ReportID == reportID).FirstOrDefault();
                    context.tblReports.Remove(reportToDelete);
                    context.SaveChanges();
                    //LogAction("Employee with ID " + employeeToDelete.EmployeeID + " deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method edits reports in DbSet and then saves changes to database.
        /// </summary>
        /// <param name="report">Report to edit.</param>
        /// <returns>Edited report.</returns>
        public vwReport EditReport(vwReport report)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblReport reportToEdit = context.tblReports.Where(x => x.ReportID == report.ReportID).FirstOrDefault();
                    reportToEdit.Date = report.Date;
                    reportToEdit.Project = report.Project;
                    reportToEdit.Hours = report.Hours;
                    context.SaveChanges();
                    return report;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of report of employee.
        /// </summary>
        /// <param name="employeeID">Employee ID.</param>
        /// <returns>List of employee's reports.</returns>
        public List<vwReport> GetAllEmployeeReports(int employeeID)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    List<vwReport> reports = new List<vwReport>();
                    reports = context.vwReports.Where(x => x.EmployeeID == employeeID).ToList();
                    return reports;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method adds report to database.
        /// </summary>
        /// <param name="report">Report to add.</param>
        public void AddReport(vwReport report)
        {
            try
            {
                using (Data_RecordsEntities context = new Data_RecordsEntities())
                {
                    tblReport reportToAdd = new tblReport
                    {
                        EmployeeID = report.EmployeeID,
                        Date = report.Date,
                        Project = report.Project,
                        Hours = report.Hours
                    };
                    context.tblReports.Add(reportToAdd);
                    context.SaveChanges();
                    report.ReportID = reportToAdd.ReportID;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
