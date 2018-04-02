/*
* Description: Controller for inputting network.
* Author: Zee
* Due date: 03/04/2018
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
        // GET: Network
        public ActionResult Index()
        {
            var networks = db.Networks.ToList();
            var model = new List<NetworkViewModel>();
            foreach (var item in networks)
            {
                model.Add(new NetworkViewModel
                {
                    Id = item.NetworkId,
                    PlatformType = item.PlatformType,
                    ContactsNumber = item.ContactsNumber,
                    EmployeeName = item.Employee.FullName,
                });
            }

            return View(model);
        }

        /// <summary>
        ///  Details of each Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Network, Details view</returns>
        // GET: Network/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return HttpNotFound();
            }

            var model = new NetworkViewModel
            {
                Id = network.NetworkId,
                ContactsNumber = network.ContactsNumber,
                PlatformType = network.PlatformType,
                EmployeeName = network.Employee.FullName,
            };
            return View(model);
        }

        // GET: Network/Create
        public ActionResult Create()
        {
            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            ViewBag.NetworkId = new SelectList(db.Networks, "NetworkId", "PlatformType");
            return View();
        }

        /// <summary>
        /// This action enables the creation of Networks.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Network, Create view</returns>
        // POST: Network/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NetworkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var network = new Network
                {
                    NetworkId = model.Id,
                    ContactsNumber = model.ContactsNumber,
                    PlatformType = model.PlatformType,
                    EmployeeId = model.EmployeeId,
                };

                db.Networks.Add(network);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
            ViewBag.NetworkId = new SelectList(db.Networks, "NetworkId", "PlatformType");
            return View(model);
        }

        // GET: Network/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return HttpNotFound();
            }

            NetworkViewModel model = new NetworkViewModel
            {
                Id = network.NetworkId,
                ContactsNumber = network.ContactsNumber,
                PlatformType = network.PlatformType,
            };
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the editing of a Networks.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Network, Edit view</returns>
        // POST: Network/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NetworkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Network network = db.Networks.Find(id);
                if (network == null)
                {
                    return HttpNotFound();
                }
                network.ContactsNumber = model.ContactsNumber;
                network.PlatformType = model.PlatformType;

                db.Entry(network).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View(model);
        }

        // GET: Network/Delete/5. 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return HttpNotFound();
            }
            var model = new NetworkViewModel
            {
                Id = network.NetworkId,
                ContactsNumber = network.ContactsNumber,
                PlatformType = network.PlatformType,
            };

            return View(model);
        }

        /// <summary>
        /// This action allows deleting Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Network, Delete view</returns>
        // POST: Network/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Network network = db.Networks.Find(id);
            db.Networks.Remove(network);
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

