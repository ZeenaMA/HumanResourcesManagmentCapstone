/*
* Description: View model for Critira Questions.
* Author: Zee
* Due date: 17/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    /// <summary>
    /// List of questions and selected score.
    /// </summary>
    public class QuestionEvaluationViewModel
    {
        List<Criterion> QuestionList = new List<Criterion>();

        List<int> GradeList = new List<int>();

    }
}