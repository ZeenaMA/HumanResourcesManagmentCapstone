/*
* Description: Controller for managing employee certification.
* Author: Zee
* Due date: 20/03/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
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

        // GET: Certification
        public ActionResult Index()
        {
            return View();
        }

        // GET: Certification/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Certification/Create
        //[Authorize]
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");

            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "CertificationType");

            ViewBag.CertificationId = new SelectList(db.Certifications, "CertificationId", "InternationalUniversity");
            return View();
        }

        // POST: Certification/Create
        [HttpPost]
        public ActionResult Create(Certification model)
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
                };
                db.Certifications.Add(certification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");
            return View(model);
        }

        // GET: Certification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Certification/Edit/5
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

        // GET: Certification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Certification/Delete/5
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
