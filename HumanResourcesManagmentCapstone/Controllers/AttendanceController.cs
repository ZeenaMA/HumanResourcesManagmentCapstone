/*
* Description: 
* Author: Zee
* Due date: 27/02/2018
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

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class AttendanceController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AttendanceController()
        {
        }

        public AttendanceController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Attendance
        // List of Employees.
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            var model = new List<EmployeeViewModel>();

            foreach (var item in employees)
            {
                model.Add(new EmployeeViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    NationalIqamaID = item.NationalIqamaID,
                    Nationality = item.Nationality,
                    DateOfBirth = item.DateOfBirth,
                });
            }

            return View(model);
        }

        // GET:Attendance/AttendanceList
        //List of All attendances.
        public ActionResult AttendanceList()
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

        // Create attendance. 
        //GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post:Attendance/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
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
                };
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("AttendanceList");
            }
            return View(model);
        }

        // List attendance for each employee.
        public ActionResult EmployeeAttendance(int id)
        {
            var items = db.Attendances.Where(d => d.AdministratorId == id).ToList();
            var model = new List<AttendanceViewModel>();
            foreach (var item in items)
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
                    //Employee = user.Employee,
                });
            }

            return View();
        }

    }
}



  