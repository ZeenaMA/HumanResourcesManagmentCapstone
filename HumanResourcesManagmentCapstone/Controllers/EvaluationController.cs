/*
* Description: Controller for managing employee evaluation, allows the creation of new evaluation, listing of all evaluation and editing and deleting.
* Author: Zee
* Due date: 18/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class EvaluationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists Evaluation.
        /// </summary>
        /// <returns> Evaluation, Index view</returns>
        // GET: Evaluation
        public ActionResult Index()
        {
            var possibleAnswers = new List<AnswerViewModel>
            {
                new AnswerViewModel { Id = 1, Text= "1"},
                new AnswerViewModel { Id = 2, Text= "2"},
                new AnswerViewModel { Id = 3, Text= "3"},
                new AnswerViewModel { Id = 4, Text= "4"},
                new AnswerViewModel { Id = 5, Text= "5"}
            };

            // Get from db
            var name = db.Criteria.ToList();

            var model = new EvaluationViewModel();

            foreach (var item in name)
            {
                model.Criteria.Add(
                    new CriterionViewModel
                    {
                        Id = item.CriterionId,
                        Criteria = item.Criteria,
                        PossibleAnswers = possibleAnswers
                    }
                    );
            }
            return View(model);
        }

        // POST: Evaluation/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EvaluationViewModel model)
        {
            var evaluation = new Evaluation
            {
                EvaluationId = model.Id
            };

            if (ModelState.IsValid)
            {
                foreach (var criterion in model.Criteria)
                {
                    criterion.Id = model.Id;
                    evaluation.GradeAttained = criterion.SelectedAnswer;
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}


