/*
* Description: Controller for managing employee evaluation, allows the creation of new evaluation, listing of all evaluation and editing and deleting.
* Author: Zee
* Due date: 05/05/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using Microsoft.AspNet.Identity;
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
        [Authorize]
        /// <summary>
        /// This action lists Evaluation.
        /// </summary>
        /// <returns> Evaluation, Index view</returns>
        // GET: Evaluation
        public ActionResult Index()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            var possibleAnswers = new List<AnswerViewModel>
            {
                new AnswerViewModel { Id = 1, Text= "1"},
                new AnswerViewModel { Id = 2, Text= "2"},
                new AnswerViewModel { Id = 3, Text= "3"},
                new AnswerViewModel { Id = 4, Text= "4"},
                new AnswerViewModel { Id = 5, Text= "5"}
            };

            // Get from db
            var criteria = db.Criteria.ToList();

            var model = new EvaluationViewModel();

            foreach (var item in criteria)
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

            decimal sum = 0;
            int count = 0;
            if (ModelState.IsValid)
            {
                foreach (var criterion in model.Criteria)
                {
                    if (criterion.SelectedAnswer.HasValue)
                    {
                        count++;
                        sum = sum + criterion.SelectedAnswer.Value;
                    }
                }
                if (count != 0)
                {
                    //calculate the average of the scores or the sum ???
                    //sum = sum / count;

                    var evaluation = new Evaluation
                    {
                        EmployeeId = model.EmployeeId,
                        EvaluatorId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                        //EmployeeId = 2, // Dropdownbox in the view to select employee to evaluate 
                        //EvaluatorId = 3, //User.Identity.GetUserId<int>(),
                        EvaluationDate = DateTime.Now,
                        GradeAttained = sum, // Store the average score for instance
                    };

                    db.Evaluations.Add(evaluation);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(String.Empty, "Select at least one answer");
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(String.Empty, "Something went wrong");
            return View();
        }
    }
}


