/*
* Description: This class depicts components of the human resource management system.
* Author: Zee
* Due date: 27/02/2018
*/
namespace HumanResourcesManagmentCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Attendance class is a construct with custom types, it contains the employee attendance.
    /// </summary>
    [Table("Attendance")]
    public partial class Attendance
    {
        public int AttendanceId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TargetWorkingHours { get; set; }

        [Column(TypeName = "numeric")]
        public decimal EmployeeWorkingHours { get; set; }

        public int PresentDays { get; set; }

        public int AbsentDays { get; set; }

        [StringLength(500)]
        public string FeedBack { get; set; }

        public int EmployeeId { get; set; }

        public int AdministratorId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Administrator { get; set; }
    }
}
