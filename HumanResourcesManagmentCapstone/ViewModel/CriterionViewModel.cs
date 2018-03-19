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
    public class CriterionViewModel
    {
        public CriterionViewModel()
        {
            Evaluations = new List<Evaluation>();
        }

        public int CriterionId { get; set; }

        [Display(Name = "Criteria")]
        public string CriteriaName { get; set; }

        [Display(Name = "Criteria Score")]
        public decimal CriteriaScore { get; set; }

        public List<Evaluation> Evaluations { get; set; }
    }
}