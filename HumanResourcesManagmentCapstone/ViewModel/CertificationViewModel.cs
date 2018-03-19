/*
* Description: .
* Author: Zee
* Due date: 20/03/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class CertificationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Certification Type")]
        public TypeOfCertification? CertificationType { get; set; }
        
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "University Rank")]
        public int? UniversityRank { get; set; }

        [Required]
        public string Major { get; set; }

        public decimal GPA { get; set; }

        public string Extracurricular { get; set; }

        [Display(Name = "International University")]
        public InterlUniversity? InternationalUniversity { get; set; }

        //
        public int EmployeeId { get; set; }

        public string Employee { get; set; }
    }
}