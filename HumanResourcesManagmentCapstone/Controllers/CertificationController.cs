/*
* Description: Controller for managing employee Certification, allows the creation of new Certification, listing of all Certification and editing and deleting.
* Author: Zee
* Due date: 04/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class CertificationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the Certifications of each employee.
        /// </summary>
        /// <returns> Certification, Index view</returns>
        // GET: Certification
        [Authorize]
        public ActionResult Index()
        {
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin");
            var certifications = db.Certifications.Where(d => d.EmployeeId == loggeduserid || loggedadmin).ToList();

            var model = new List<CertificationViewModel>();
            foreach (var item in certifications)
            {
                model.Add(new CertificationViewModel
                {
                    Id = item.CertificationId,
                    CertificationType = item.CertificationType,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    UniversityRank = item.UniversityRank,
                    Major = item.Major,
                    GPA = item.GPA,
                    Extracurricular = item.Extracurricular,
                    InternationalUniversity = item.InternationalUniversity,
                    EmployeeName = item.Employee.FullName,
                });
            }

            return View(model);
        }
        /// <summary>
        ///  Details of each Certification.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Certification, Details view</returns>
        // GET: Certification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return HttpNotFound();
            }

            var model = new CertificationViewModel
            {
                Id = certification.CertificationId,
                CertificationType = certification.CertificationType,
                StartDate = certification.StartDate,
                EndDate = certification.EndDate,
                UniversityRank = certification.UniversityRank,
                Major = certification.Major,
                GPA = certification.GPA,
                Extracurricular = certification.Extracurricular,
                InternationalUniversity = certification.InternationalUniversity,
                EmployeeName = certification.Employee.FullName,
            };
            return View(model);
        }

        /// <summary>
        /// This action enables the creation of an Certification.
        /// </summary>
        /// <returns> Certification, Create view</returns>
        // GET: Certification/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "CertificationType");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "InternationalUniversity");
            return View();
        }

        /// <summary>
        /// This action enables the creation of an Certification.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Certification, Create view</returns>
        // POST: Certification/Create
        [HttpPost]
        public ActionResult Create(CertificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var certification = new Certification
                {
                    CertificationType = model.CertificationType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    UniversityRank = model.UniversityRank,
                    Major = model.Major,
                    GPA = model.GPA,    
                    Extracurricular = model.Extracurricular,
                    InternationalUniversity = model.InternationalUniversity,
                    EmployeeId = model.EmployeeId,
                };
                db.Certifications.Add(certification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "CertificationType");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "InternationalUniversity");
            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a Certification.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Certification, Edit view</returns>
        // GET: Certification/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return HttpNotFound();
            }

            CertificationViewModel model = new CertificationViewModel
            {
                Id = certification.CertificationId,
                CertificationType = certification.CertificationType,
                StartDate = certification.StartDate,
                EndDate = certification.EndDate,
                UniversityRank = certification.UniversityRank,
                Major = certification.Major,
                GPA = certification.GPA,
                Extracurricular = certification.Extracurricular,
                InternationalUniversity = certification.InternationalUniversity,
                EmployeeName = certification.Employee.FullName,
            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "CertificationType");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "InternationalUniversity");
            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a Certification.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Certification, Edit view</returns>
        // (POST: Certification/Edit/5) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CertificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Certification certification = db.Certifications.Find(id);
                if (certification == null)
                {
                    return HttpNotFound();
                }
                certification.CertificationType = model.CertificationType;
                certification.StartDate = model.StartDate;
                certification.EndDate = model.EndDate;
                certification.UniversityRank = model.UniversityRank;
                certification.Major = model.Major;
                certification.GPA = model.GPA;
                certification.Extracurricular = model.Extracurricular;
                certification.InternationalUniversity = model.InternationalUniversity;
                db.Entry(certification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "CertificationType");
            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "InternationalUniversity");
            return View(model);
        }

        /// <summary>
        /// This action allows deleting Certification.
        /// </summary>
        /// <returns> Certification, Delete view</returns>
        // GET: Certification/Delete/5. 
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            var model = new CertificationViewModel
            {
                Id = certification.CertificationId,
                EmployeeName = certification.Employee.FullName,
                CertificationType = certification.CertificationType,
                StartDate = certification.StartDate,
                EndDate = certification.EndDate,
                UniversityRank = certification.UniversityRank,
                Major = certification.Major,
                GPA = certification.GPA,
                Extracurricular = certification.Extracurricular,
                InternationalUniversity = certification.InternationalUniversity,
            };

            return View(model);
        }

        /// <summary>
        /// This action allows deleting Certification.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Certification, Delete view</returns>
        // (POST: Certification/Delete/5) 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Certification certification = db.Certifications.Find(id);
            db.Certifications.Remove(certification);
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


