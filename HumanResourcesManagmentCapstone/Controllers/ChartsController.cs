/*
* Description: Controller for managing employee Performance, allows the creation of new Performance, listing of all Performance and editing and deleting.
* Author: Zee
* Due date: 05/05/2018
*/
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
        [Authorize(Roles = "Admin, CEO")]

        // GET: Charts
        public ActionResult Index()
        {

            var employee = db.Employees.ToList();
            var evaluation = db.Evaluations;
            decimal? count = 0;
            var labels = new List<string>();
            var data = new List<decimal?>();

            foreach (var item in employee)
            {
                count = evaluation.Where(m => m.Employee.Id == item.Id).Select(x => x.GradeAttained).Sum();
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
