/*
* Description: This class depicts components of the human resource management system.
* Author: Zee
* Due date: 18/04/2018
*/
namespace HumanResourcesManagmentCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Score class is a construct with custom types, it contains evaluation scores.
    /// </summary>
    [Table("Score")]
    public partial class Score
    {
        public int ScoreId { get; set; }

        public int ScoreNumber { get; set; }

        public int EvaluationId { get; set; }

        public int? CriterionId { get; set; }

        public virtual Evaluation Evaluation { get; set; }

        public virtual Criterion Criterion { get; set; }
        
    }
}
