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
    /// CommunicationSkill class is a construct with custom types, this class contains all skills of an employee.
    /// </summary>
    [Table("CommunicationSkill")]
    public partial class CommunicationSkill
    {
        public int CommunicationSkillId { get; set; }

        [Required]
        [StringLength(15)]
        public string SkillType { get; set; }

        public LevelLevel? SkillLevel { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
    // Enum for SkillLevel.
    public enum LevelLevel
    {
        Basic,
        Intermediate,
        Professional,
    }
}
