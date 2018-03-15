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
    /// Employee class is a construct that is the supertype of all the system users. 
    /// </summary>
    [Table("Employee")]
    public partial class Employee : ApplicationUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Achievements = new HashSet<Achievement>();
            Attendances = new HashSet<Attendance>();
            Attendances1 = new HashSet<Attendance>();
            Certifications = new HashSet<Certification>();
            CommunicationSkills = new HashSet<CommunicationSkill>();
            Constituents = new HashSet<Constituent>();
            Evaluations = new HashSet<Evaluation>();
            Evaluations1 = new HashSet<Evaluation>();
            Experiences = new HashSet<Experience>();
            Networks = new HashSet<Network>();
            Performances = new HashSet<Performance>();
            Salaries = new HashSet<Salary>();
        }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [StringLength(60)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        //[Required]
        //[StringLength(30)]
        //public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime HiredDate { get; set; }

        public int NationalIqamaID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IqamaExpiryDate { get; set; }

        [Required]
        [StringLength(13)]
        public string BankAccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public EmployeeGender? Gender { get; set; }

        //[Required]
        //[StringLength(10)]
        public TypeEmployee? EmployeeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Achievement> Achievements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certification> Certifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommunicationSkill> CommunicationSkills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Constituent> Constituents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluations1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Experience> Experiences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Network> Networks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Performance> Performances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salary> Salaries { get; set; }
    }

    // Enum for EmployeeType.
    public enum TypeEmployee
    {
        CEO,
        Admin,

        [Display(Name = "Team Member ")]
        TeamMember
    }

    // Enum for Gender.
    public enum EmployeeGender
    {
        Male,
        Female
    }
}
