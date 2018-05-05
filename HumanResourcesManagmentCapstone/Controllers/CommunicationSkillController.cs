/*
* Description: Controller for managing employee Communication Skills, allows the creation of new Communication Skills, listing of all Communication Skills and editing and deleting.
* Author: Zee
* Due date: 05/05/2018
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
    public class CommunicationSkillController : Controller
    { 
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the CommunicationSkills of each employee.
        /// </summary>
        /// <returns> CommunicationSkill, Index view</returns>
        // GET: CommunicationSkill
        [Authorize]
        public ActionResult Index()
        {
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin");
            var communicationSkills = db.CommunicationSkills.Where(d => d.EmployeeId == loggeduserid || loggedadmin).ToList(); ;

            var model = new List<CommunicationSkillViewModel>();
            foreach (var item in communicationSkills)
            {
                model.Add(new CommunicationSkillViewModel
                {
                    Id = item.CommunicationSkillId,
                    SkillType = item.SkillType,
                    SkillLevel = item.SkillLevel,
                    EmployeeName = item.Employee.FullName,
                });
            }

            return View(model);
        }

        /// <summary>
        ///  Details of each CommunicationSkill.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>CommunicationSkill, Details view</returns>
        // GET: CommunicationSkill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationSkill communicationSkill = db.CommunicationSkills.Find(id);
            if (communicationSkill == null)
            {
                return HttpNotFound();
            }

            var model = new CommunicationSkillViewModel
            {
                Id = communicationSkill.CommunicationSkillId,
                SkillType = communicationSkill.SkillType,
                SkillLevel = communicationSkill.SkillLevel,
                EmployeeName = communicationSkill.Employee.FullName,
            };
            return View(model);
        }

        /// <summary>
        /// This action enables the creation of an CommunicationSkill.
        /// </summary>
        /// <returns> CommunicationSkill, Create view</returns>  
        // GET: CommunicationSkill/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            ViewBag.CommunicationSkillId = new SelectList(db.CommunicationSkills, "CommunicationSkillId", "SkillLevel");
            return View();
        }

        /// <summary>
        /// This action enables the creation of an CommunicationSkill.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> CommunicationSkill, Create view</returns>    
        // POST: CommunicationSkill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommunicationSkillViewModel model)
{
            if (ModelState.IsValid)
            {
                var communicationSkill = new CommunicationSkill
                {
                    CommunicationSkillId = model.Id,
                    SkillType = model.SkillType,
                    SkillLevel = model.SkillLevel,
                    EmployeeId = model.EmployeeId,
                };

                db.CommunicationSkills.Add(communicationSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.CommunicationSkillId = new SelectList(db.CommunicationSkills, "CommunicationSkillId", "SkillLevel");

            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a CommunicationSkill.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> CommunicationSkill, Edit view</returns>
        // GET: CommunicationSkill/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CommunicationSkill communicationSkill = db.CommunicationSkills.Find(id);
            if (communicationSkill == null)
            {
                return HttpNotFound();
            }

            CommunicationSkillViewModel model = new CommunicationSkillViewModel
            {
                Id = communicationSkill.CommunicationSkillId,
                SkillType = communicationSkill.SkillType,
                SkillLevel = communicationSkill.SkillLevel,
            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CommunicationSkillId = new SelectList(db.CommunicationSkills, "CommunicationSkillId", "SkillLevel");
            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a CommunicationSkill.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="model"></param>
        /// <returns> CommunicationSkill, Edit view</returns>
        // (POST: CommunicationSkill/Edit/5) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommunicationSkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                CommunicationSkill communicationSkill = db.CommunicationSkills.Find(id);
                if (communicationSkill == null)
                {
                    return HttpNotFound();
                }
                communicationSkill.SkillType = model.SkillType;
                communicationSkill.SkillLevel = model.SkillLevel;
                db.Entry(communicationSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CommunicationSkillId = new SelectList(db.CommunicationSkills, "CommunicationSkillId", "SkillLevel");
            return View(model);
        }

        /// <summary>
        /// This action allows deleting CommunicationSkill.
        /// </summary>
        /// <returns> CommunicationSkill, Delete view</returns>
        // GET: CommunicationSkill/Delete/5. 
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationSkill communicationSkill = db.CommunicationSkills.Find(id);
            if (communicationSkill == null)
            {
                return HttpNotFound();
            }
            var model = new CommunicationSkillViewModel
            {
                Id = communicationSkill.CommunicationSkillId,
                EmployeeName = communicationSkill.Employee.FullName,
                SkillType = communicationSkill.SkillType,
                SkillLevel = communicationSkill.SkillLevel,
            };

            return View(model);
        }
        /// <summary>
        /// This action allows deleting CommunicationSkill.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns> CommunicationSkill, Delete view</returns>
        // (POST: CommunicationSkill/Delete/5) 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommunicationSkill communicationSkill = db.CommunicationSkills.Find(id);
            db.CommunicationSkills.Remove(communicationSkill);
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

