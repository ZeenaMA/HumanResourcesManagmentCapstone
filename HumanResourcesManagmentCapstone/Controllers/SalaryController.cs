/*
* Description: Controller for managing employee salary, allows the creation of new salary, listing of all salary and editing and deleting.
* Author: Zee
* Due date: 18/04/2018
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
    public class SalaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the salary of each employee.
        /// </summary>
        /// <returns> CommunicationSkill, Index view</returns>
        // GET: Salary
        [Authorize]
        public ActionResult Index()
        {
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin");
            var salaries = db.Salaries.Where(d => d.EmployeeId == loggeduserid || loggedadmin).ToList();

            var model = new List<SalaryViewModel>();
            foreach (var item in salaries)
            {
                model.Add(new SalaryViewModel
                {
                    Id = item.SerialId,
                    EmployeeName = item.Employee.FullName,
                    IssueDate = item.IssueDate,
                    BasicSalary = item.BasicSalary,
                    PerformanceBasedSalary = item.PerformanceBasedSalary
                });
            }

            return View(model);
        }

        //TODO salary calculation
        //public static void CalculateSalary(AttendanceViewModel)
        //{

        //}

        /// <summary>
        /// This action enables the creation of a salary.
        /// </summary>
        /// <returns> Performance, Create view</returns>
        // GET: Salary/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            return View();
        }

        /// <summary>
        /// This action enables the creation of a salary.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Performance, Create view</returns>
        // POST: Salary/Create
        [HttpPost]
        public ActionResult Create(Salary model)
        {
            if (ModelState.IsValid)
            {
                var salary = new Salary
                {
                    EmployeeId = model.EmployeeId,
                    IssueDate = DateTime.Now,
                    BasicSalary = model.BasicSalary,
                    PerformanceBasedSalary = model.PerformanceBasedSalary,
                };
                db.Salaries.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a salary.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Performance, Edit view</returns>
        // GET: Salary/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }

            SalaryViewModel model = new SalaryViewModel
            {
                Id = salary.SerialId,
                EmployeeName = salary.Employee.FullName,
                IssueDate = DateTime.Now,
                BasicSalary = salary.BasicSalary,
                PerformanceBasedSalary = salary.PerformanceBasedSalary

            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a salary.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Performance, Edit view</returns>
        // POST: Salary/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SalaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Salary salary = db.Salaries.Find(id);
                if (salary == null)
                {
                    return HttpNotFound();
                }
                salary.BasicSalary = model.BasicSalary;
                salary.PerformanceBasedSalary = model.PerformanceBasedSalary;
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View(model);
        }

        /// <summary>
        /// This action allows deleting salary.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Performance, Delete view</returns>
        // GET: Salary/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            var model = new SalaryViewModel
            {
                Id = salary.SerialId,
                EmployeeName = salary.Employee.FullName,
                IssueDate = salary.IssueDate,
                BasicSalary = salary.BasicSalary,
                PerformanceBasedSalary = salary.PerformanceBasedSalary
            };

            return View(model);
        }

        /// <summary>
        /// This action allows deleting salary.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Performance, Delete view</returns>
        // POST: Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = db.Salaries.Find(id);
            db.Salaries.Remove(salary);
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
