/*
* Description: Controller for managing employee Network, allows the creation of new Network, listing of all Network and editing and deleting.
* Author: Zee
* Due date: 04/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    public class NetworkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists the Networks of each employee.
        /// </summary>
        /// <returns> Network, Index view</returns>
        //GET: Network
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            var model = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                model.Add(new EmployeeViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName
                });
            }

            return View(model);
        }
        // GET: Network/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                UserName = employee.UserName
            };

            return View(model);
        }

        //// GET: Network/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Network([Bind(Include = "NetworkId,PlatformType,ContactsNumber")] NetworkViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var network = new Network
        //        {
        //            PlatformType = model.PlatformType,
        //            ContactsNumber = model.ContactsNumber,
        //        };
        //        db.Networks.Add(network);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(model);
        //}

        //// GET: Network/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Network network = db.Networks.Find(id);
        //    if (network == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var model = new NetworkViewModel
        //    {
        //        Id = network.NetworkId,
        //        PlatformType = network.PlatformType,
        //        ContactsNumber = network.ContactsNumber
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "NetworkId,PlatformType,ContactsNumber")] NetworkViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var network = new Network
        //        {
        //            NetworkId = model.Id,
        //            PlatformType = model.PlatformType,
        //            ContactsNumber = model.ContactsNumber
        //        };

        //        db.Entry(network).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        //// GET: Network/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var network = db.Networks.Find(id);
        //    if (network == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var model = new NetworkViewModel
        //    {
        //        Id = network.NetworkId,
        //        PlatformType = network.PlatformType,
        //        ContactsNumber = network.ContactsNumber
        //    };

        //    return View(model);
        //}

        //// POST: Network/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var network = db.Networks.Find(id);
        //    db.Networks.Remove(network);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult Networks(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                UserName = employee.UserName
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult AddNetworkPartial(NetworkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var network = new Network
                {
                    PlatformType = model.PlatformType,
                    ContactsNumber = model.ContactsNumber,
                    EmployeeId = model.EmployeeId,
                };

                db.Networks.Add(network);
                db.SaveChanges();

                return PartialView();
            }

            return PartialView(model);
        }
        // GET: Network
        public ActionResult GetNetworksPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var networks = db.Networks.Where(c => c.EmployeeId == id).ToList();

            var model = new List<NetworkViewModel>();
            foreach (var network in networks)
            {
                model.Add(new NetworkViewModel
                {
                    Id = network.NetworkId,
                    ContactsNumber = network.ContactsNumber,
                    PlatformType = network.PlatformType,
                    EmployeeId = network.EmployeeId,
                });
            }

            return PartialView(model);
        }

        public ActionResult GetNetworks()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            return View();
        }
    }
}

