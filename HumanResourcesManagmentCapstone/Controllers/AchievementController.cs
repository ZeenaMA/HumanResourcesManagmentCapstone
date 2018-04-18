/*
* Description: Controller for managing employee achievements allows the creation of new achievements, listing of achievements and editing and deleting.
* Author: Zee
* Due date: 04/04/2018
*/
using AutoMapper;
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class AchievementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the Achivements of each employee.
        /// </summary>
        /// <returns> Achivement, Index view</returns>
        // GET: Achievement
        [Authorize]
        public ActionResult Index()
        {
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin");
            var achievements = db.Achievements.Where(d => d.EmployeeId == loggeduserid || loggedadmin).ToList();

            var model = new List<AchievementViewModel>();

            foreach (var item in achievements)
            {
                model.Add(new AchievementViewModel
                {
                    Id = item.AchievementId,
                    AchievementType = item.AchievementType,
                    Discription = item.Discription,
                    EmployeeName = item.Employee.FullName,
                });
            }

            return View(model);
        }

        /// <summary>
        ///  Details of each achievement-.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Achievement, Details view</returns>
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

            var model = new AchievementViewModel
            {
                Id = achievement.AchievementId,
                Discription = achievement.Discription,
                AchievementType = achievement.AchievementType,
                EmployeeName = achievement.Employee.FullName,
            };
            return View(model);
        }

        // GET: Achievement/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.AchievementId = new SelectList(db.Achievements, "AchievementId", "AchievementType");
            return View();
        }

        /// <summary>
        /// This action enables the creation of an Achievement.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Achievement, Create view</returns>
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

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.AchievementId = new SelectList(db.Achievements, "AchievementId", "AchievementType");
            return View(model);
        }

        // GET: Achievement/Edit/5
        [Authorize(Roles = "Admin")]
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

            AchievementViewModel model = new AchievementViewModel
            {
                Id = achievement.AchievementId,
                Discription = achievement.Discription,
                AchievementType = achievement.AchievementType,
            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the editing of a Achievement.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Achievement, Edit view</returns>
        // (POST: Achievement/Edit/5) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AchievementViewModel model)
        {
            if (ModelState.IsValid)
            {
                Achievement achievement = db.Achievements.Find(id);
                if (achievement == null)
                {
                    return HttpNotFound();
                }
                achievement.Discription = model.Discription;
                achievement.AchievementType = model.AchievementType;
                db.Entry(achievement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View(model);
        }

        // GET: Achievement/Delete/5. 
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
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
            var model = new AchievementViewModel
            {
                Id = achievement.AchievementId,
                EmployeeName = achievement.Employee.FullName,
                Discription = achievement.Discription,
                AchievementType = achievement.AchievementType,
            };

            return View(model);
        }

        /// <summary>
        /// This action allows deleting Achievement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Achievement, Delete view</returns>
        // (POST: Achievement/Delete/5) 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achievement achievement = db.Achievements.Find(id);
            db.Achievements.Remove(achievement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

