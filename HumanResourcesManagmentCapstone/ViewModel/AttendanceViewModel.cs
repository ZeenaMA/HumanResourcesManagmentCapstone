using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TargetWorkingHours { get; set; }

        public decimal EmployeeWorkingHours { get; set; }

        public int PresentDays { get; set; }

        public int AbsentDays { get; set; }

        public string FeedBack { get; set; }
    }
}