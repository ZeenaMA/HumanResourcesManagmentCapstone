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
    /// Experience class is a construct with custom types, this class contains all the experiences of an employee.
    /// </summary>
    [Table("Experience")]
    public partial class Experience
    {
        public int ExperienceId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmploymentPlace { get; set; }

        [StringLength(50)]
        public string EmploymentType { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public Orgnization? OrgnizationType { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }

    // Enum for OrgnizationType
    public enum Orgnization
    {
        Corporate,
        Startup,
        Critical,
        Position,
        Multinational,
        Mnassa
    }
}
