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
    /// Constituent class is a construct with custom types, it represents a multivalued relationship between employee and performance.
    /// </summary>
    [Table("Constituent")]
    public partial class Constituent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EPPerformanceId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EPEmployeeId { get; set; }

        public int? EmployeeNumber { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Performance Performance { get; set; }
    }
}
