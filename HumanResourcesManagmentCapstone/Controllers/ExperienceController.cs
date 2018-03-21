﻿/*
* Description: Controller for managing employee Experience.
* Author: Zee
* Due date: 20/03/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class ExperienceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the Experiences of each employee.
        /// </summary>
        /// <returns> Experience, Index view</returns>
        // GET: Experience
        public ActionResult Index()
    {
        var experiences = db.Experiences.ToList();
        var model = new List<ExperienceViewModel>();
        foreach (var item in experiences)
        {
            model.Add(new ExperienceViewModel
            {
                Id = item.ExperienceId,
                EmploymentPlace = item.EmploymentPlace,
                EmploymentType = item.EmploymentType,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Description = item.Description,
                OrgnizationType = item.OrgnizationType,
                EmployeeName = item.Employee.FullName,
            });
        }

        return View(model);
    }

        /// <summary>
        ///  Details of each Experience.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Experience, Details view</returns>
        // GET: Experience/Details/5
        public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
            Experience experience = db.Experiences.Find(id);
        if (experience == null)
        {
            return HttpNotFound();
        }

        var model = new ExperienceViewModel
        {
            Id = experience.ExperienceId,
            EmploymentPlace = experience.EmploymentPlace,
            EmploymentType = experience.EmploymentType,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description,
            OrgnizationType = experience.OrgnizationType,
        };
        return View();
    }

        // GET: Experience/Create
        public ActionResult Create()
    {
        var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

        ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "OrgnizationType");
        return View();
    }
        /// <summary>
        /// This action enables the creation of an Experience.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Experience, Create view</returns>
        // POST: Experience/Create
        [HttpPost]
         [ValidateAntiForgeryToken]
    public ActionResult Create(ExperienceViewModel model)
    {
        if (ModelState.IsValid)
        {
            var experience = new Experience
            {
                ExperienceId = model.Id,
                EmploymentPlace = model.EmploymentPlace,
                EmploymentType = model.EmploymentType,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Description = model.Description,
                OrgnizationType = model.OrgnizationType,
                EmployeeId = model.EmployeeId,
            };

            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "OrgnizationType");
            return View(model);
    }

        /// <summary>
        /// This action enables the editing of a Experience.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Experience, Edit view</returns>
        // GET: Experience/Edit/5
        public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

            Experience experience = db.Experiences.Find(id);
        if (experience == null)
        {
            return HttpNotFound();
        }

        var model = new ExperienceViewModel
        {
            Id = experience.ExperienceId,
            EmploymentPlace = experience.EmploymentPlace,
            EmploymentType = experience.EmploymentType,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description,
            OrgnizationType = experience.OrgnizationType,
            EmployeeId = experience.EmployeeId,
        };
        return View();
    }

        // GET: Experience/Delete/5. 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            var model = new ExperienceViewModel
            {
                Id = experience.ExperienceId,
                EmploymentPlace = experience.EmploymentPlace,
                EmploymentType = experience.EmploymentType,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                Description = experience.Description,
                OrgnizationType = experience.OrgnizationType,
            };

            return View(model);
        }

        /// <summary>
        /// This action allows deleting Experience.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Experience, Delete view</returns>
        // (POST: Experience/Delete/5) 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = db.Experiences.Find(id);
            db.Experiences.Remove(experience);
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
