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
    /// Network class is a construct with custom types, this class contains all the social midea contacts of an employee.
    /// </summary>
    [Table("Network")]
    public partial class Network
    {
        public int NetworkId { get; set; }

        public PlatformType? PlatformType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ContactsNumber { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }

    // Enum for PlatformType.
    public enum PlatformType
    {
        Facebook,
        Instagram,
        Snapchat,
        Mnassa,
        LinkedIn,
        Twitter,

        [Display(Name = "Phone Contacts ")]
        PhoneContacts
    }
}
