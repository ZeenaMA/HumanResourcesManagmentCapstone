/*
* Description: View model for Criterion passes information between Criterion views and its controller.
* Author: Zee
* Due date: 03/04/2018
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{    
     /// <summary>
     /// View model based on the Criterion model.
     /// </summary>
    public class CriterionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Criterion")]
        public string CriteriaName { get; set; }

        public decimal CriteriaScore { get; set; }
    }
}