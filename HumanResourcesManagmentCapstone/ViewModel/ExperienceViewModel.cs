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
    public class ExperienceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Employment Place")]
        public string EmploymentPlace { get; set; }

        [Display(Name = "Employment Type")]
        public string EmploymentType { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Orgnization Type")]
        public Orgnization? OrgnizationType { get; set; }

        public int EmployeeId { get; set; }

        public string Employee { get; set; }
    }
}