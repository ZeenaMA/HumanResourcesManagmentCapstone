/*
* Description: View model for Certification passes information between Certification views and its controller.
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
        /// <summary>
        /// View model based on the Certification model.
        /// </summary>
        public int Id { get; set; }

        [Display(Name = "Certification Type")]
        public CertificationType? CertificationType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "University Rank")]
        public int? UniversityRank { get; set; }

        [Required]
        public string Major { get; set; }

        public decimal GPA { get; set; }

        public string Extracurricular { get; set; }

        [Display(Name = "International University")]
        public InternationalUniversity? InternationalUniversity { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
    }