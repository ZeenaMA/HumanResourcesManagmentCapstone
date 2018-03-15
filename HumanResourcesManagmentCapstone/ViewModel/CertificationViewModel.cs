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

        public TypeOfCertification? CertificationType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? UniversityRank { get; set; }

        [Required]
        public string Major { get; set; }

        public decimal GPA { get; set; }

        public string Extracurricular { get; set; }

        public InterlUniversity? InternationalUniversity { get; set; }
    }
}