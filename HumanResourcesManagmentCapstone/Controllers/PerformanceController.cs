﻿/*
* Description: Controller for inputting performance.
* Author: Zee
* Due date: 03/04/2018
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
    public class PerformanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the Performances of each employee.
        /// </summary>
        /// <returns> Performance, Index view</returns>
        // GET: Performance
        public ActionResult Index()
        {
            var performances = db.Performances.ToList();
            var model = new List<PerformanceViewModel>();
            foreach (var item in performances)
            {
                model.Add(new PerformanceViewModel
                {
                    Id = item.PerformanceId,
                    KPI = item.KPI,
                    Discipline = item.Discipline,
                    Status = item.Status,
                    IssueDate = item.IssueDate,
                    Comment = item.Comment,
                    Decision = item.Decision,
                    CreationDate = item.CreationDate,
                    EmployeeName = item.Employee.FullName,
                });
            }

            return View(model);
        }

        /// <summary>
        ///  Details of each Performance-.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Performance, Details view</returns>
        // GET: Performance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }

            var model = new PerformanceViewModel
            {
                Id = performance.PerformanceId,
                KPI = performance.KPI,
                Discipline = performance.Discipline,
                Status = performance.Status,
                IssueDate = performance.IssueDate,
                Comment = performance.Comment,
                Decision = performance.Decision,
                CreationDate = performance.CreationDate,
                EmployeeName = performance.Employee.FullName,
            };
            return View(model);
        }

        // GET: Performance/Create
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            ViewBag.PerformanceId = new SelectList(db.Performances, "PerformanceId", "Decision");
            return View();
        }

        /// <summary>
        /// This action enables the creation of a Performance.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Performance, Create view</returns>
        // POST: Performance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerformanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var performance = new Performance
                {
                    PerformanceId = model.Id,
                    KPI = model.KPI,
                    Discipline = model.Discipline,
                    Status = model.Status,
                    IssueDate = model.IssueDate,
                    Comment = model.Comment,
                    Decision = model.Decision,
                    CreationDate = model.CreationDate,
                    EmployeeId = model.EmployeeId,
                };

                db.Performances.Add(performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.PerformanceId = new SelectList(db.Performances, "PerformanceId", "Decision");
            return View(model);
        }

        // GET: Performance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }

            PerformanceViewModel model = new PerformanceViewModel
            {
                Id = performance.PerformanceId,
                KPI = performance.KPI,
                Discipline = performance.Discipline,
                Status = performance.Status,
                IssueDate = performance.IssueDate,
                Comment = performance.Comment,
                Decision = performance.Decision,
                CreationDate = performance.CreationDate,
            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the editing of a Performance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Performance, Edit view</returns>
        // POST: Performance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PerformanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Performance performance = db.Performances.Find(id);
                if (performance == null)
                {
                    return HttpNotFound();
                }
                performance.KPI = model.KPI;
                performance.Discipline = model.KPI;
                performance.Status = model.Status;
                performance.IssueDate = model.IssueDate;
                performance.Comment = model.Comment;
                performance.Decision = model.Decision;
                performance.CreationDate = model.CreationDate;

                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View(model);
        }

        // GET: Performance/Delete/5. 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            var model = new PerformanceViewModel
            {
                Id = performance.PerformanceId,
                KPI = performance.KPI,
                Discipline = performance.Discipline,
                Status = performance.Status,
                IssueDate = performance.IssueDate,
                Comment = performance.Comment,
                Decision = performance.Decision,
                CreationDate = performance.CreationDate,
            };

            return View(model);
        }

        /// <summary>
        /// This action allows deleting Performance.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Performance, Delete view</returns>
        // POST: Performance/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = db.Performances.Find(id);
            db.Performances.Remove(performance);
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

