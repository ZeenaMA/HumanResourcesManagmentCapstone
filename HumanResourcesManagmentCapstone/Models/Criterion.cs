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
    /// Criterion class is a construct with custom types, it contains the elements for an evlauation.
    /// </summary>
    [Table("Criterion")]
    public partial class Criterion
    {
        public Criterion()
        {
            Evaluations = new HashSet<Evaluation>();
        }

        public int CriterionId { get; set; }

        [Required]
        [StringLength(10)]
        public string CriteriaName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal CriteriaScore { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }
}
