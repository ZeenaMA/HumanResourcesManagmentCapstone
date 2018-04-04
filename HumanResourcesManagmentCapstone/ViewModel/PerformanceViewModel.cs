/*
* Description: View model for Performance passes information between Performance views and its controller.
* Author: Zee
* Due date: 03/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    /// <summary>
    /// View model based on the Performance model.
    /// </summary>
    public class PerformanceViewModel
    {
        public int Id { get; set; }

        public decimal KPI { get; set; }

        public decimal Discipline { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public Decision? Decision { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}