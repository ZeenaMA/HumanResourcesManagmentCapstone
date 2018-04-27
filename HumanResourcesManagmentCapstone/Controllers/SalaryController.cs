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
                    //PerformanceBasedSalary = item.PerformanceBasedSalary,
                    PerformanceBasedSalary = GetEmployeePerformanceSalary(item.EmployeeId), // Calculate salary
                });
            }

            return View(model);
        }

        public decimal GetEmployeePerformanceSalary(int id)
        {
            // Get certificate type of the employee
            var certificateType = db.Certifications.Where(p => p.EmployeeId == id).Select(p => p.CertificationType).Single();

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
            var workingHours = db.Attendances.Where(x => x.EmployeeId == id).Select(x => x.EmployeeWorkingHours).Sum();
            var discipline = db.Performances.Where(x => x.EmployeeId == id).Select(x => x.Discipline).Sum();
            var KPI = db.Performances.Where(x => x.EmployeeId == id).Select(x => x.KPI).Sum();
            var evaluation = db.Evaluations.Where(x => x.EmployeeId == id).OrderBy(p => p.EvaluationDate).Select(x => x.GradeAttained).FirstOrDefault();

            var totalWorkingHours = (workingHours / workingHours) * 100;
            var totaldiscipline = (discipline / discipline) * 100;
            var totalKPI = (KPI / KPI) * 100;
            //var totalevaluation = (evaluation / evaluation) * 100;

            //var points = totalWorkingHours + totaldiscipline + totalKPI + totalevaluation;

            //MK
            //var education = ;
            //var network = db.Networks.Where(x => x.EmployeeId == employee.Id).Select(x => x.ContactsNumber).Sum();
            //var experience =
            var a = db.Experiences.Where(x => x.EmployeeId == id).Select(x => x.StartDate);
            var b = db.Experiences.Where(x => x.EmployeeId == id).Select(x => x.EndDate);


            // return final salary
            return 1M;
        }

        //public ActionResult Salary()
        //{
        //    //var CertificationGrade = 0.0;
        //    //switch ()
        //    //{
        //    //    case CertificationType.Bachelor:
        //    //        CertificationGrade = 6.3;
        //    //        break;
        //    //    case CertificationType.Diploma:
        //    //        CertificationGrade = 1.4;
        //    //        break;
        //    //    case CertificationType.Master:
        //    //        CertificationGrade = 5.6;
        //    //        break;
        //    //    case CertificationType.Phd:
        //    //        CertificationGrade = 12.6;
        //    //        break;
        //    //    case CertificationType.PostGrad:
        //    //        CertificationGrade = 2.1;
        //    //        break;
        //    //    default:
        //    //        CertificationGrade += 0;
        //    //        break;
        //    //}


        //    foreach (var employee in db.Employees.ToList())
        //    {
        //        // Points.
        //        var workingHours = db.Attendances.Where(x => x.EmployeeId == employee.Id).Select(x => x.EmployeeWorkingHours).Sum();
        //        var discipline = db.Performances.Where(x => x.EmployeeId == employee.Id).Select(x => x.Discipline).Sum();
        //        var KPI = db.Performances.Where(x => x.EmployeeId == employee.Id).Select(x => x.KPI).Sum();
        //        //var evaluation = db.Evaluations.Where(x => x.EmployeeId == employee.Id).Select(x => x.Scores).Sum();

        //        var totalWorkingHours = (workingHours / workingHours) * 100;
        //        var totaldiscipline = (discipline / discipline) * 100;
        //        var totalKPI = (KPI / KPI) * 100;
        //        //var totalevaluation = (evaluation / evaluation) * 100;

        //        //var points = totalWorkingHours + totaldiscipline + totalKPI + totalevaluation;

        //        //MK
        //        //var education = ;
        //        //var network = db.Networks.Where(x => x.EmployeeId == employee.Id).Select(x => x.ContactsNumber).Sum();
        //        //var experience =
        //        var a = db.Experiences.Where(x => x.EmployeeId == employee.Id).Select(x => x.StartDate);
        //        var b = db.Experiences.Where(x => x.EmployeeId == employee.Id).Select(x => x.EndDate);

        //        //Score%
        //        // var marketValue = networks + education + experienc + premuim;
        //        // var points = KPI + workingHours + discipline + evaluation;
        //        // var score = marketValue * points;
        //        // var totalScores = score; // total tho
        //        // var scorePercentage = score / totalScores;

        //        //Basic salary
        //        // var basicsalary = Salaries.basicsalary;
        //        // var totalbasicsalary =  ;

        //        //Total salary
        //        var basicSalary = db.Salaries.Where(x => x.EmployeeId == employee.Id).Select(x => x.BasicSalary).Single();
        //        var salary = basicSalary;// * (totalScore * totalBasicSalary)  ;

        //        var salaryentity = db.Salaries.Where(x => x.EmployeeId == employee.Id).Single();
        //        salaryentity.PerformanceBasedSalary = salary;
        //        salaryentity.IssueDate = DateTime.Now;
        //        db.SaveChanges();

        //    }

        //    //var a = new DateTime(2010, 10, 1);
        //    //var b = new DateTime(2012, 10, 1);
        //    //var c = b - a;
        //    return View();
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
