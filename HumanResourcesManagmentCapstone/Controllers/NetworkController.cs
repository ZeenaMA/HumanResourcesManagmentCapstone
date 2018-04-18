/*
* Description: Controller for managing employee Network, allows the creation of new Network, listing of all Network and editing and deleting.
* Author: Zee
* Due date: 18/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagmentCapstone.Controllers
{
    [Authorize]
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
            var loggeduserid = User.Identity.GetUserId<int>();
            var loggedadmin = User.IsInRole("Admin , CEO");
            var employees = db.Employees.Where(d => d.Id == loggeduserid || loggedadmin).ToList();

            var model = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                model.Add(new EmployeeViewModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName
                });
            }

            return View(model);
        }

        /// <summary>
        /// This action enables the creation of Networks.
        /// </summary>
        /// <returns> Network, Create view</returns>
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

        /// <summary>
        /// This action enables the editing of a Networks.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Network, Edit view</returns>
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
            return View(model);
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

        /// <summary>
        /// This action allows deleting Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Network, Delete view</returns>
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
                EmployeeName = network.Employee.FullName,
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
        [HttpPost, ActionName("Delete")]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                UserName = employee.UserName,
                FirstName = employee.FirstName,
                LastName = employee.LastName
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

