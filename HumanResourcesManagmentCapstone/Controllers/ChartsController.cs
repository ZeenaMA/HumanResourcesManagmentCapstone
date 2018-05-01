using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Charts
        public ActionResult Index()
        {
            //var E = db.Employees.ToList();
            //var V = db.Evaluations;
            //int count = 0;
            //var labels = new List<string>();
            //var data = new List<int>();
            //foreach (var item in E)
            //{
            //    count = V.Count(m => m.Employee.Id == item.Id);
            //    if (count != 0)
            //    {
            //        labels.Add(item.FirstName);
            //        data.Add(count);
            //    }
            //}
            //ViewBag.Labels = labels.ToArray();
            //ViewBag.Data = data.ToArray();

            //return View();

            var employee = db.Employees.ToList();
            var evaluation = db.Evaluations.Select(x => x.GradeAttained).Sum();
            decimal? count = 0;
            var labels = new List<string>();
            var data = new List<decimal?>();

            foreach (var item in employee)
            {
                count = evaluation; /*.Sum(m => m.GradeAttained);*/ /*/ V.Average(m => m.GradeAttained);*/
                if (count != 0)
                {
                    labels.Add(item.UserName);
                    data.Add(count);
                }
            }
            ViewBag.Labels = labels.ToArray();
            ViewBag.Data = data.ToArray();

            return View();


        }

    }
}
