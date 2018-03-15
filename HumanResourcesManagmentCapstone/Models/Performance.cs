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
    /// Performance class is a construct with custom types, it is used for the creation of reports and is composed of employee performance and other class types such as Discipline and KPI.
    /// </summary>
    [Table("Performance")]
    public partial class Performance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Performance()
        {
            Constituents = new HashSet<Constituent>();
        }

        public int PerformanceId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal KPI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Discipline { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime IssueDate { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        public DecisionType? Decision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        public int EmployeeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Constituent> Constituents { get; set; }

        public virtual Employee Employee { get; set; }
    }
    // Enum for Decision.
    public enum DecisionType
    {
        Approved,
        Disapproved
    }
}
