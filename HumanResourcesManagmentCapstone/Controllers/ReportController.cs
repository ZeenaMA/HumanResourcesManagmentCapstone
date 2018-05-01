using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GetEmployees()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            return View();

        }

        public ActionResult GetPerformancePartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var perfromances = db.Performances.Where(c => c.EmployeeId == id).ToList();

            var model = new List<PerformanceViewModel>();
            foreach (var perfromance in perfromances)
            {
                model.Add(new PerformanceViewModel
                {
                    Id = perfromance.PerformanceId,
                    KPI = perfromance.KPI,
                    Discipline = perfromance.Discipline,
                    EmployeeId = perfromance.EmployeeId,
                });
            }

            return PartialView(model);
        }

        public ActionResult GetSalaryPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salaries = db.Salaries.Where(c => c.EmployeeId == id).ToList();

            var model = new List<SalaryViewModel>();
            foreach (var salarie in salaries)
            {
                model.Add(new SalaryViewModel
                {
                    Id = salarie.SerialId,
                    BasicSalary = salarie.BasicSalary,
                    PerformanceBasedSalary = salarie.PerformanceBasedSalary,
                    EmployeeId = salarie.EmployeeId,
                });
            }

            return PartialView(model);
        }
    }
}
