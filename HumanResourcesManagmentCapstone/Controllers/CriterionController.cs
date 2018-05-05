/*
* Description: Controller for managing employee Criterion, allows the creation of new Criterion, listing of all Criterion and editing and deleting.
* Author: Zee
* Due date: 05/05/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class CriterionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists critera for the evaluation.
        /// </summary>
        // GET: Criterion
        public ActionResult Index()
        {
            var criteria = db.Criteria.ToList();

            var model = new List<CriterionViewModel>();
            foreach (var item in criteria)
            {
                model.Add(new CriterionViewModel
                {
                    Id = item.CriterionId,
                    Criteria = item.Criteria,
                });
            }
            return View(model);
        }
    }
}
