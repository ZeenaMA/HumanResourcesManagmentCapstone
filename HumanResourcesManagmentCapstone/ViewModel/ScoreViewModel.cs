using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class ScoreViewModel
    {
        public int ScoreId { get; set; }

        public int ScoreNumber { get; set; }

        public int EvaluationId { get; set; }

        public int? CriterionId { get; set; }

        public string Evaluation { get; set; }

        public string Criterion { get; set; }
    }
}