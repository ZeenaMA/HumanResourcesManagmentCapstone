/*
* Description: View model for Salary passes information between Salary views and its controller.
* Author: Zee
* Due date: 03/04/2018
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    /// <summary>
    /// View model based on the Salary model.
    /// </summary>
    public class SalaryViewModel
    {
        public int Id { get; set; }

        public int SerialNumber { get; set; }

        [Display(Name = "Issue Date")]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Basic Salary")]
        public decimal BasicSalary { get; set; }

        [Display(Name = "Performance Based Salary")]
        public decimal PerformanceBasedSalary { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}