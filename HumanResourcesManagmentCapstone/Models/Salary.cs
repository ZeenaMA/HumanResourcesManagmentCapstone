/*
* Description: This class depicts components of the human resource management system.
* Author: Zee
* Due date: 18/04/2018
*/
namespace HumanResourcesManagmentCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// Salary class is a construct with custom types, this class contains the salaries of an employee.
    /// </summary>
    [Table("Salary")]
    public partial class Salary
    {
        [Key]
        public int SerialId { get; set; }

        public int SerialNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Premium { get; set; }

        [Column(TypeName = "numeric")]
        public decimal BasicSalary { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PerformanceBasedSalary { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
