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
    /// Certification class is a construct with custom types, this class contains all the certifications of an employee. 
    /// </summary>
    [Table("Certification")]
    public partial class Certification
    {
        public int CertificationId { get; set; }

        public CertificationType? CertificationType { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int? UniversityRank { get; set; }

        [Required]
        [StringLength(50)]
        public string Major { get; set; }

        [Column(TypeName = "numeric")]
        public decimal GPA { get; set; }

        [StringLength(100)]
        public string Extracurricular { get; set; }

        public InternationalUniversity? InternationalUniversity { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

    }

    // Enum for CertificationType.
    public enum CertificationType
    {
        Bachelor,
        Master,

        [Display(Name = "Post Grad ")]
        PostGrad,

        Diploma,
        Phd
    }

    // Enum for InternationalUniversity.
    public enum InternationalUniversity
    {
        Yes,
        No
    }
}
