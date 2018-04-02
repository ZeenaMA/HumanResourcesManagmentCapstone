/*
* Description: View model for Evaluation passes information between Evaluation views and its controller.
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
    /// View model based on the Evaluation model.
    /// </summary>
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

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        //public virtual Criterion Criterion { get; set; }

        //public virtual Employee Employee { get; set; }

        //public virtual Employee EmployeeEvaluation { get; set; }
    }
}