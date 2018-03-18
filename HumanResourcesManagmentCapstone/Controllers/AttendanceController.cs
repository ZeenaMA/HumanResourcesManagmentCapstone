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
            var users = db.Employees.ToList();
            var model = new List<EmployeeViewModel>();

            foreach (var item in users)
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
            var attendance = db.Attendances.ToList();

            var model = new List<AttendanceViewModel>();
            foreach (var item in attendance)
            {
                model.Add(new AttendanceViewModel
                {
                    Id = item.AttendanceId,
                    StartDate = item.StartDate,
                    // OTHERSTUFF??!!!
                });
            }
            return View(model);
            //var users = db.Attendances.ToList();
            //var model = new List<AttendanceViewModel>();
            //foreach (var user in users)
            //{
            //    model.Add(new AttendanceViewModel
            //    {
            //        //Id = user.Id,???????????????
            //        StartDate = user.StartDate,
            //        EndDate = user.EndDate,
            //        TargetWorkingHours = user.TargetWorkingHours,
            //        PresentDays = user.PresentDays,
            //        AbsentDays = user.AbsentDays,
            //        EmployeeWorkingHours = user.EmployeeWorkingHours,
            //        FeedBack = user.FeedBack,
            //        //Employee = user.Employee.LastName,
            //    });
            //}
            //return View(model);
        }

        //GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }
        //Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find Employee
                var attendance = new Attendance
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TargetWorkingHours = model.TargetWorkingHours,
                    PresentDays = model.PresentDays,
                    AbsentDays = model.AbsentDays,
                    EmployeeWorkingHours = model.EmployeeWorkingHours,
                    FeedBack = model.FeedBack,
                    AdministratorId = model.AdministratorId,
                };
                db.Attendances.Add(attendance);
                db.SaveChanges();
            }
            return RedirectToAction("AttendanceList");
        }

        ////GET: Attendance/AttendanceList
        //public ActionResult AttendanceList()
        //{
        //    //return View(db.Attendances.ToList());
        //    return View(db.Attendances.ToList());

        //}

        //AdministratorId
        public ActionResult ForList(int id)
        {
            var users = db.Attendances.Where(d => d.EmployeeId == id).ToList();
            var model = new List<AttendanceViewModel>();
            foreach (var user in users)
            {
                model.Add(new AttendanceViewModel
                {
                    StartDate = user.StartDate,
                    EndDate = user.EndDate,
                    TargetWorkingHours = user.TargetWorkingHours,
                    PresentDays = user.PresentDays,
                    AbsentDays = user.AbsentDays,
                    EmployeeWorkingHours = user.EmployeeWorkingHours,
                    FeedBack = user.FeedBack,
                    //Employee = user.Employee,
                });
            }

            return View();
        }

    }
}



  