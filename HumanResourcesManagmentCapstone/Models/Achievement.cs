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
    /// Achievement class is a construct with custom types, this class contains all the achivements of an employee.
    /// </summary>
    [Table("Achievement")]
    public partial class Achievement
    {
        [Key]
        public int AchievementId { get; set; }

        public AchievementType? AchievementType { get; set; }

        [StringLength(200)]
        public string Discription { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
    // Enum for AchievementType .
    public enum AchievementType
    {
        Business,
        NonProfit,
        Freelance,
        Personal,
        Other
    }
}
