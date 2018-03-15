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
    /// Evaluation class is a construct with custom types, it represents the evlauation performed by/ belog to an employee based on criteria.
    /// </summary>
    [Table("Evaluation")]
    public partial class Evaluation
    {
        public int EvaluationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime EvaluationDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GradeAttained { get; set; }

        public int EvaluatorId { get; set; }

        public int EmployeeId { get; set; }

        public int CriterionId { get; set; }

        public virtual Criterion Criterion { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}
