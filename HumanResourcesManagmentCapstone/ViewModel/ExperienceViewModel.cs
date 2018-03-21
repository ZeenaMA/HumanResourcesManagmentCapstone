/*
* Description: View model for Experience passes information between Experience views and its controller.
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
        /// <summary>
        /// View model based on the Experience model.
        /// </summary>
        public int Id { get; set; }

        [Display(Name = "Employment Place")]
        public string EmploymentPlace { get; set; }

        [Display(Name = "Employment Type")]
        public string EmploymentType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Orgnization Type")]
        public OrgnizationType? OrgnizationType { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}