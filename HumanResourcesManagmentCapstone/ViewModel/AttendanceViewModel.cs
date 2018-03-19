/*
* Description: .
* Author: Zee
* Due date: 20/03/2018
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance")]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Display(Name = "Target Working Hours")]
        public decimal TargetWorkingHours { get; set; }

        [Display(Name = "Employee Working Hours")]
        public decimal EmployeeWorkingHours { get; set; }

        [Display(Name = "Present Days")]
        public int PresentDays { get; set; }

        [Display(Name = "Absent Days")]
        public int AbsentDays { get; set; }

        [DataType(DataType.MultilineText)]
        public string FeedBack { get; set; }

        // Employee Attendance.
        public int AdministratorId { get; set; }

        public string Employee { get; set; }

    }
}