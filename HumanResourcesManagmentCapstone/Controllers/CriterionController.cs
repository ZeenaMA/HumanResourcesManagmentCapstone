/*
* Description: Controller for managing employee achievements.
* Author: Zee
* Due date: 20/03/2018
*/
using AutoMapper;
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
                    CriteriaName = item.CriteriaName,
                    CriteriaScore = item.CriteriaScore,
                });
            }

            return View(model);
        }

        // GET: Criterion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Criterion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Criterion/Create
        [HttpPost]
        public ActionResult Create(CriterionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var criterion = new Criterion
                {
                    CriterionId = model.Id,
                    CriteriaName = model.CriteriaName,
                    CriteriaScore = model.CriteriaScore,
                };

                db.Criteria.Add(criterion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Criterion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Criterion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Criterion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Criterion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
