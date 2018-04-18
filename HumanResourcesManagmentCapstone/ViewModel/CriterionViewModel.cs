/*
* Description: View model for Criterion passes information between Criterion views and its controller.
* Author: Zee
* Due date: 03/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
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
        public CriterionViewModel()
        {
            PossibleAnswers = new List<AnswerViewModel>();
        }
        public int Id { get; set; }

        [Display(Name = "Criterion")]
        public string Criteria { get; set; }

        public int? SelectedAnswer { get; set; }

        public List<AnswerViewModel> PossibleAnswers { get; set; }
    }
}