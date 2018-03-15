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
    // Add.

    /// <summary>
    /// This controller uses Employee and EmployeeViewModel classes
    /// </summary>
    public class EmployeeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public EmployeeController()
        {
        }

        public EmployeeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

                    // Use Automapper instead of copying properties one by one
                    EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                    model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());

                    return View(model);
                }
                else
                {
                    // Customize the error view: /Views/Shared/Error.cshtml
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }
        // GET: Employee/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeType");
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model, params string[] roles)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmployeeType = model.EmployeeType,
                };

                var result = UserManager.Create(employee, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(employee.Id, "Employee");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeType");
                        // Display error messages in the view @Html.ValidationSummary()
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }
                }
                else
                {
                    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeType");
                    // Display error messages in the view @Html.ValidationSummary()
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    return View();
                }
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeType");
            return View();

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);

                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    //return HttpNotFound();
                    return View("Error");
                }

                EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                var userRoles = UserManager.GetRoles(userId);
                var rolesSelectList = db.Roles.ToList().Select(r => new SelectListItem()
                {
                    Selected = userRoles.Contains(r.Name),
                    Text = r.Name,
                    Value = r.Name
                });

                ViewBag.RolesSelectList = rolesSelectList;

                return View(model);
            }

            //return HttpNotFound();
            return View("Error");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, EmployeeViewModel model, params string[] roles)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {

                // Convert id to non-nullable int
                var userId = id ?? default(int);

                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                // Update the properties of the employee
                employee.Email = model.Email;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.UserName = model.Email;

                var userResult = UserManager.Update(employee);

                if (userResult.Succeeded)
                {
                    var userRoles = UserManager.GetRoles(employee.Id);
                    roles = roles ?? new string[] { };
                    var roleResult = UserManager.AddToRoles(employee.Id, roles.Except(userRoles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    roleResult = UserManager.RemoveFromRoles(employee.Id, userRoles.Except(roles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);
                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);
                model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());
                return View(model);
            }

            return HttpNotFound();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {
                var userId = id ?? default(int);
                var user = UserManager.FindById(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}