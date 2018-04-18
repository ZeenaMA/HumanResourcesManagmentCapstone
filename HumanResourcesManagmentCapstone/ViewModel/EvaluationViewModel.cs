/*
* Description: View model for Evaluation passes information between Evaluation views and its controller.
* Author: Zee
* Due date: 017/04/2018
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
        public EvaluationViewModel()
        {
            Criteria = new List<CriterionViewModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<CriterionViewModel> Criteria { get; set; }
    }
}