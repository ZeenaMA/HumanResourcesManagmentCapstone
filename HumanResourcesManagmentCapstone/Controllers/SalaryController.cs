/*
* Description: Controller for managing employee salary, allows the creation of new salary, listing of all salary and editing and deleting.
* Author: Zee
* Due date: 05/05/2018
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
                    IssueDate = item.IssueDate.Value.Date,
                    BasicSalary = item.BasicSalary,
                    PerformanceBasedSalary = GetEmployeePerformanceSalary(item.EmployeeId)
                });
            }

            return View(model);
        }

        public decimal GetEmployeePerformanceSalary(int id)
        {
            // Points
            // Sum WorkingHours, Discipline, KPI, Evaluation for each employee.
            var employeeWorkingHours = db.Attendances.Where(x => x.EmployeeId == id).Select(x => x.EmployeeWorkingHours).DefaultIfEmpty().Sum();
            var employeeDiscipline = db.Performances.Where(x => x.EmployeeId == id).Select(x => x.Discipline).Sum();
            var employeeKPI = db.Performances.Where(x => x.EmployeeId == id).Select(x => x.KPI).Sum();
            var employeeEvaluation = db.Evaluations.Where(x => x.EmployeeId == id).Select(x => x.GradeAttained).Sum();
            //OrderByDescending(p => p.EvaluationDate).Select(x => x.GradeAttained).FirstOrDefault();

            // Sum WorkingHours, Discipline, KPI, Evaluation for all employees.
            var totalWorkingHours = db.Attendances.Select(x => x.EmployeeWorkingHours).Sum();
            var totalDiscipline = db.Performances.Select(x => x.Discipline).Sum();
            var totalKPI = db.Performances.Select(x => x.KPI).Sum();
            var totalEvaluation = db.Evaluations.Select(x => x.GradeAttained).Sum();

            var workingHours = (employeeWorkingHours / totalWorkingHours) * 100;
            var discipline = (employeeDiscipline / totalDiscipline) * 100;
            var KPI = (employeeKPI / totalKPI) * 100;
            var evaluation = (employeeEvaluation / totalEvaluation) * 100;

            // Total points.
            var points = (workingHours + discipline + KPI + evaluation.GetValueOrDefault());

            // Market Value. 
            // Get certificate type of the employee
            var certificateType = db.Certifications.Where(p => p.EmployeeId == id).Select(p => p.CertificationType).FirstOrDefault();

            decimal CertificationGrade = 0M;

            if (certificateType != null)
            {
                switch (certificateType)
                {
                    case CertificationType.Bachelor:
                        CertificationGrade = 6.3M;
                        break;
                    case CertificationType.Diploma:
                        CertificationGrade = 1.4M;
                        break;
                    case CertificationType.Master:
                        CertificationGrade = 5.6M;
                        break;
                    case CertificationType.Phd:
                        CertificationGrade = 12.6M;
                        break;
                    case CertificationType.PostGrad:
                        CertificationGrade = 2.1M;
                        break;
                    default:
                        CertificationGrade = 0M;
                        break;
                }
            }

            // Sum Networks, premium for each employee.
            var employeeNetworks = db.Networks.Where(x => x.EmployeeId == id).Select(x => x.ContactsNumber).DefaultIfEmpty().Sum();
            var premium = db.Salaries.Where(x => x.EmployeeId == id).Select(x => x.Premium).Sum();

            // Sum Networks for all employees.
            var totalNetworks = db.Networks.Select(x => x.ContactsNumber).Sum();
            var networks = ((employeeNetworks / totalNetworks) * 10) * 2;

            // Extract year and month from Start and End date.
            var experienceStartDate = db.Experiences.Where(x => x.EmployeeId == id).Select(x => x.StartDate.Year).Min();
            var experienceEndDate = db.Experiences.Where(x => x.EmployeeId == id).Select(x => x.EndDate.Year).Max();
            var experienceStartDateMonth = db.Experiences.Where(x => x.EmployeeId == id).Select(x => x.StartDate.Month).Min();
            var experienceEndDateMonth = db.Experiences.Where(x => x.EmployeeId == id).Select(x => x.EndDate.Month).Max();
            var experienceYear = experienceEndDate - experienceStartDate;
            var experienceMonth = experienceEndDateMonth - experienceStartDateMonth;
            var experience = (experienceYear * 2.0M) + (experienceMonth * 0.16M);

            // Total Market Value 
            var marketValue = (networks + premium + experience + CertificationGrade);

            // Get basicSalary for each employee then for all employees.
            var basicSalary = db.Salaries.Where(x => x.EmployeeId == id).Select(x => x.BasicSalary).Sum();
            var tatalBasicSalary = db.Salaries.Select(x => x.BasicSalary).Sum();

            //Score %
            var score = marketValue * points;
            var totalScores = score + score;
            var scorePercentage = score / totalScores;
            var salary = basicSalary + ((scorePercentage * tatalBasicSalary));

            // return final salary
            return (salary);
        }

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
                    Premium = model.Premium,
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

        public ActionResult Report()
        {
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin");
            var salaries = db.Salaries.Where(d => d.EmployeeId == loggeduserid || loggedadmin).ToList();

            var model = new List<SalaryViewModel>();
            foreach (var item in salaries)
            {
                model.Add(new SalaryViewModel
                {

                });
            }

            return View(model);
        }



















    }
}
