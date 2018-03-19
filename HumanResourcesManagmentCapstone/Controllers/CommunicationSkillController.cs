/*
* Description: 
* Author: Zee
* Due date: 27/02/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class CommunicationSkillController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommunicationSkill
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommunicationSkill/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommunicationSkill/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "SkillLevel");
            return View();
        }
             
        // POST: CommunicationSkill/Create
        [HttpPost]
        public ActionResult Create(CommunicationSkill model)
{ 
       if (ModelState.IsValid)
            {
                // Create the course from the model
                var communicationSkill = new CommunicationSkill
                {
               SkillLevel = model.SkillLevel,
               SkillType = model.SkillType,
                };
            }


            return RedirectToAction("Index");
        }

        // GET: CommunicationSkill/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommunicationSkill/Edit/5
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

        // GET: CommunicationSkill/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommunicationSkill/Delete/5
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
