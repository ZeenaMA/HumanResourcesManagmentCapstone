/*
* Description: 
* Author: Zee
* Due date: 27/02/2018
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

    public class AchievementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Achievement
        public ActionResult Index()
        {
            var achievements = db.Achievements.ToList();
            var model = new List<AchievementViewModel>();

            foreach (var item in achievements)
            {
                model.Add(new AchievementViewModel
                {
                    Id = item.AchievementId,
                    AchievementType = item.AchievementType,
                    Discription = item.Discription,
                });
            }

            return View(model);
        }

        // GET: Achievement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Achievement/Create
        public ActionResult Create()
        {
            ViewBag.AchievementId = new SelectList(db.Achievements, "AchievementId", "AchievementType");
            return View();
        }

        // POST: Achievement/Create
        [HttpPost]
        public ActionResult Create(AchievementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var achievement = new Achievement
                {
                    AchievementId = model.Id,
                    AchievementType = model.AchievementType,
                    Discription = model.Discription,
                };

                db.Achievements.Add(achievement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AchievementId = new SelectList(db.Achievements, "AchievementId", "AchievementType");
            return View(model);
        }

        // GET: Achievement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Achievement/Edit/5
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

        // GET: Achievement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Achievement/Delete/5
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
