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

        public decimal TargetWorkingHours { get; set; }

        public decimal EmployeeWorkingHours { get; set; }

        public int PresentDays { get; set; }

        public int AbsentDays { get; set; }

        [DataType(DataType.MultilineText)]
        public string FeedBack { get; set; }
    }
}