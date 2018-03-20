/*
* Description: Controller for managing employee achievements.
* Author: Zee
* Due date: 2/03/2018
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
    /// <summary>
    /// Create/ Edit/ Delete achievements for employees.
    /// </summary>
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
                    //HACK get the fullname
                    EmployeeName = item.Employee.FullName,
                });
            }

            return View(model);
        }

        // GET: Achievement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }

           //AchievementViewModel model = Mapper.Map<Achievement, AchievementViewModel>(achievement);
           return View(model);
    }

    // GET: Achievement/Create
    public ActionResult Create()
        {
            //HACK Build the list to display with full name
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName});
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            //ViewBag.AchievementId = new SelectList(db.Achievements, "AchievementId", "AchievementType");
            return View();
        }

        // POST: Achievement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AchievementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var achievement = new Achievement
                {
                    AchievementId = model.Id,
                    Discription = model.Discription,
                    AchievementType = model.AchievementType,
                    EmployeeId = model.EmployeeId,
                };

                db.Achievements.Add(achievement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //HACK Get employee list using firstname and lastname
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            //ViewBag.AchievementId = new SelectList(db.Achievements, "AchievementId", "AchievementType");
            return View(model);
        }

        // GET: Achievement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }

            //AchievementViewModel model = Mapper.Map<Achievement, AchievementViewModel>(achievement);

            return View(model);
        }

    }
    }

