/*
* Description: Controller for managing employee attendance.
* Author: Zee
* Due date: 20/03/2018
*/
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class AttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Action methods responsible for creating employee attendance, editing or deleleting it.
        /// </summary>
        
        // GET:Attendance
        //List of All attendances.
        public ActionResult Index()
        {
            var attendances = db.Attendances.ToList();
            var model = new List<AttendanceViewModel>();

            foreach (var item in attendances)
            {
                model.Add(new AttendanceViewModel
                {
                    Id = item.AttendanceId,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    TargetWorkingHours = item.TargetWorkingHours,
                    PresentDays = item.PresentDays,
                    AbsentDays = item.AbsentDays,
                    EmployeeWorkingHours = item.EmployeeWorkingHours,
                    FeedBack = item.FeedBack,
                });
            }
            return View(model);
        }

        // GET: Attendance/ Detailes
        // Attendance Detailes
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            var model = new AttendanceViewModel
            {
                Id = attendance.AttendanceId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                TargetWorkingHours = attendance.TargetWorkingHours,
                PresentDays = attendance.PresentDays,
                AbsentDays = attendance.AbsentDays,
                EmployeeWorkingHours = attendance.EmployeeWorkingHours,
                FeedBack = attendance.FeedBack,
            };

            return View(model);
        }

        //GET: Attendance/Create
        // Create attendance. 
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");
            return View();
        }

        //Post:Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attendance = new Attendance
                {
                    AttendanceId = model.Id,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TargetWorkingHours = model.TargetWorkingHours,
                    PresentDays = model.PresentDays,
                    AbsentDays = model.AbsentDays,
                    EmployeeWorkingHours = model.EmployeeWorkingHours,
                    FeedBack = model.FeedBack,
                    AdministratorId = model.Id,
                };

            db.Attendances.Add(attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");
            return View(model);
        }

        // GET: Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            AttendanceViewModel model = new AttendanceViewModel
            {
                Id = attendance.AttendanceId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                TargetWorkingHours = attendance.TargetWorkingHours,
                PresentDays = attendance.PresentDays,
                AbsentDays = attendance.AbsentDays,
                EmployeeWorkingHours = attendance.EmployeeWorkingHours,
                FeedBack = attendance.FeedBack,
            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");
            return View(model);
        }

        // POST: Attendance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Attendance attendance = db.Attendances.Find(id);
                if (attendance == null)
                {
                    return HttpNotFound();
                }
                attendance.StartDate = model.StartDate;
                attendance.EndDate = model.EndDate;
                attendance.TargetWorkingHours = model.TargetWorkingHours;
                attendance.PresentDays = model.PresentDays;
                attendance.AbsentDays = model.AbsentDays;
                attendance.EmployeeWorkingHours = model.EmployeeWorkingHours;
                attendance.FeedBack = model.FeedBack;
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");
            return View(model);
        }

        // GET: Attendance/Delete/5
        // Delete attendance 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            var model = new AttendanceViewModel
            {
                Id = attendance.AttendanceId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                TargetWorkingHours = attendance.TargetWorkingHours,
                PresentDays = attendance.PresentDays,
                AbsentDays = attendance.AbsentDays,
                EmployeeWorkingHours = attendance.EmployeeWorkingHours,
                FeedBack = attendance.FeedBack,
            };

            return View(model);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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




  