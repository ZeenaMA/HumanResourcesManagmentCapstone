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


        //GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,TargetWorkingHours,EmployeeWorkingHours,PresentDays,AbsentDays,FeedBack")] AttendanceViewModel attendanceViewModel)
        {
            if (ModelState.IsValid)
            {
                db.AttendanceViewModels.Add(attendanceViewModel);
                db.SaveChanges();
                return RedirectToAction("AttendanceList");
            }

            return View(attendanceViewModel);
        }

        //GET: Attendance/AttendanceList
        public ActionResult AttendanceList()
        {
            //return View(db.Attendances.ToList());
            return View(db.AttendanceViewModels.ToList());

        }
        //public ActionResult List(int id)
        //{
        //    var users = db.AttendanceViewModels.Where(d => d.EmployeeId == id).ToList();
        //    var model = new List<AttendanceViewModel>();
        //    foreach (var user in users)
        //    {
        //        model.Add(new AttendanceViewModel
        //        {
        //            Id = user.Id,

        //        });
        //    }

        //    return PartialView(model);
        //}
        // GET: Attendance/Create
        //public ActionResult Create()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(AttendanceViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Attendance attendance = Mapper.Map<AttendanceViewModel, Attendance>(model);

        //        db.Attendances.Add(attendance);
        //        db.SaveChanges();
        //        return RedirectToAction("I");
        //    }

        //    return View(model);
        //}

        ////POST: Attendance/Create
        //[HttpPost]
        //public ActionResult Create(AttendanceViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Attendance attendance = new Attendance
        //        {
        //            StartDate = model.StartDate,
        //            EndDate = model.EndDate,
        //            TargetWorkingHours = model.TargetWorkingHours,
        //            EmployeeWorkingHours = model.EmployeeWorkingHours,
        //            PresentDays = model.PresentDays,
        //            AbsentDays = model.AbsentDays,
        //            FeedBack = model.FeedBack,
        //        };
        //        return RedirectToAction("I");

        //    }

        //    return View(model);
        //}

        //public ActionResult I()
        //{
        //    var users = db.Attendances.ToList();
        //    var model = new List<AttendanceViewModel>();

        //    foreach (var item in users)
        //    {
        //        model.Add(new AttendanceViewModel
        //            StartDate = model.StartDate,
        //            EndDate = model.EndDate,
        //            TargetWorkingHours = model.TargetWorkingHours,
        //            EmployeeWorkingHours = model.EmployeeWorkingHours,
        //            PresentDays = model.PresentDays,
        //            AbsentDays = model.AbsentDays,
        //            FeedBack = model.FeedBack,
        //        });
        //}

        //    return View(model);
        //}

    }
}



  