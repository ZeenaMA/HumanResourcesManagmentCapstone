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
        // GET: Evalaution
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

            // Get the questions from the database
            var questions = new List<QuestionViewModel>
            {
                new QuestionViewModel { Id = 1, Text = "Positive Energy", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 2, Text = "Taking Initiatives ", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 3, Text = "Action", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 4, Text = "Project Managment", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 5, Text = "Time Management", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 6, Text = "Functionally", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 7, Text = "Personally", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 8, Text = "Listening Skills", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 9, Text = "Persuading Skills", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 10, Text = "Skills", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 11, Text = "Knowladge", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 12, Text = "Attitudes", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 13, Text = "Doing their best", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 14, Text = "Having a vision", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 15, Text = "Moving others", PossibleAnswers = possibleAnswers },
                new QuestionViewModel { Id = 16, Text = "Doning Something deferent", PossibleAnswers = possibleAnswers },

            };

            // Create a new view model
            var newmodel = new EvaluationViewModel();

            // Copy the questions from the data model into the view model
            foreach (var item in questions)
            {
                newmodel.Questions.Add(item);
            }
            return View(newmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var question in model.Questions)
                {

                    //question.SelectedAnswer;
                     //User.Identity.GetUserId<int>(),

                }

                // Return to to your home
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}

