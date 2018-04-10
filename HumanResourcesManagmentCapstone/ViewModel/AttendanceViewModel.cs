/*
* Description: View model for Attendance passes information between Attendance views and its controller.
* Author: Zee
* Due date: 17/04/2018
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using HumanResourcesManagmentCapstone.Common;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    /// <summary>
    /// View model based on the Attendance model.
    /// </summary>
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[CurrentDate]
        [Display(Name = "Attendance")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Range(0, 200)]
        [Display(Name = "Target Working Hours")]
        public decimal TargetWorkingHours { get; set; }

        [Range(0, 200)]
        [Display(Name = "Employee Working Hours")]
        public decimal EmployeeWorkingHours { get; set; }

        [Range(10, 20)]
        [Display(Name = "Present Days")]
        public int PresentDays { get; set; }

        [Range(0, 9)]
        [Display(Name = "Absent Days")]
        public int AbsentDays { get; set; }

        [DataType(DataType.MultilineText)]
        public string FeedBack { get; set; }

        public int? AdministratorId { get; set; }

        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}