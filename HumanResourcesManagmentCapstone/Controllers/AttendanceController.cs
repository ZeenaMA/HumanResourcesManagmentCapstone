﻿/*
* Description: Controller for managing employee WorkingHours, allows the creation of new WorkingHours, listing of all WorkingHours and editing and deleting.
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
    [Authorize]
    public class AttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// List employee attendance.
        /// </summary>
        /// <returns>Attendance list</returns>
        // GET: Attendance
        public ActionResult Index()
        {
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin");
            var attendances = db.Attendances.Where(d => d.EmployeeId == loggeduserid || loggedadmin).ToList();

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
                    EmployeeName = item.Employee.FullName,
                    AdministratorId = item.AdministratorId
                });
            }

            return View(model);
        }
        ///<summary>
        /// Details of each Employee.
        ///</summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Attendance, Details view</returns>
        // GET: Attendance/ Detailes
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
                EmployeeName = attendance.Employee.FullName,
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the creation of an Attendances.
        ///</summary>
        /// <param name="model"></param>
        /// <returns> Attendances, Create view</returns>
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
                    EmployeeId = model.EmployeeId,
                };

                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// Edit attendance.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns></returns>
        // GET: Attendance/Edit/5
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// This action enables the editing of a Attendances.
        ///</summary>
        /// <param name="id">Employee Id</param>
        /// <param name="model"></param>
        /// <returns> Attendances, Edit view</returns>
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
        /// <summary>
        /// Delete employee attendance. 
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns></returns>
        // GET: Attendance/Delete/5
        // Delete attendance
        [Authorize(Roles = "Admin")]
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
                EmployeeName = attendance.Employee.FullName,
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

        /// <summary>
        /// This action allows deleting Attendances.
        ///</summary>
        /// <param name="id">Employee Id</param>
        /// <returns> Attendances, Delete view</returns>
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
        /// <summary>
        /// List employee wotking hours.
        /// </summary>
        /// <returns>Workinghours Index</returns>
        public ActionResult WorkingHoursIndex()
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
                    EmployeeName = item.Employee.FullName,
                    AdministratorId = item.AdministratorId
                });
            }

            return View(model);
        }

        public ActionResult WorkingHoursCreate()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the creation of an Attendances.
        ///</summary>
        /// <param name="model"></param>
        /// <returns> Attendances, Create view</returns>
        //Post:Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkingHoursCreate(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attendance = new Attendance
                {
                    AttendanceId = model.Id,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TargetWorkingHours = model.TargetWorkingHours,
                    AbsentDays = model.AbsentDays,
                    EmployeeWorkingHours = model.EmployeeWorkingHours,
                    FeedBack = model.FeedBack,
                    EmployeeId = model.EmployeeId,
                };

                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("WorkingHoursIndex");
            }

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// Edit employee working hours.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns></returns>
        // GET: Attendance/Edit/5
        public ActionResult WorkingHoursEdit(int? id)
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

        /// <summary>
        /// This action enables the editing of a Attendances.
        ///</summary>
        /// <param name="id">Employee Id</param>
        /// <param name="model"></param>
        /// <returns> Attendances, Edit view</returns>
        // POST: Attendance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkingHoursEdit(int id, AttendanceViewModel model)
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
                return RedirectToAction("WorkingHoursIndex");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "UserName");
            return View(model);
        }
    }
    }