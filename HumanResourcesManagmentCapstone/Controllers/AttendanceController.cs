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

namespace HumanResourcesManagmentCapstone.Controllers
    {
        /// <summary>
        ///
        /// </summary>
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
        public ActionResult DatePicker()
        {
            return View();
        }
        // GET: Employee
        public ActionResult Index()
            {
                var users = db.Employees.ToList();
                var model = new List<EmployeeViewModel>();

                foreach (var item in users)
                {
                    if (!(item is Employee))
                    {
                        model.Add(new EmployeeViewModel
                        {
                            Id = item.Id,
                            Email = item.Email,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                        });
                    }
                }

                return View(model);
            }

            // GET: Employee/Details/5
            // Example of displaying custom error view (Views/Shared/Error.cshtml) when id is null
            // The is parameter changed from int to int? to accept nulls
            public ActionResult Details(int? id)
            {
                if (id != null)
                {
                    // Convert id to int instead of int?
                    int userId = id ?? default(int);

                    // find the user in the database
                    var user = UserManager.FindById(userId);

                    // Check if the user exists and it is an emplyee not a simple application user
                    if (user != null && user is Employee)
                    {
                        var employee = (Employee)user;

                        EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                        model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());

                        return View(model);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("Error");
                }
            }


        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
