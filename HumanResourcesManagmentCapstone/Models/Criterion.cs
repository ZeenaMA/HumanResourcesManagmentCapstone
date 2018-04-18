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
            Scores = new HashSet<Score>();
        }
        public int CriterionId { get; set; }

        [Required]
        [StringLength(200)]
        public string Criteria { get; set; }

        public int? SelectedAnswer { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
