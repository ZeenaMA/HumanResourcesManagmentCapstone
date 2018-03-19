/*
* Description: .
* Author: Zee
* Due date: 20/03/2018
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class EvaluationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Evaluation Date")]
        public DateTime EvaluationDate { get; set; }

        [Display(Name = "Grade")]
        public decimal? GradeAttained { get; set; }

        public int EvaluatorId { get; set; }

        public int EmployeeId { get; set; }

        public int CriterionId { get; set; }

        public string Criterion { get; set; }

        public string Employee { get; set; }

        public string EmployeeEvaluation { get; set; }
    }
}