using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkersRegistrationApp.Controllers
{
    /// <summary>
    /// The <c>WorkerController</c> class.
    /// Contains method for passing results of SQL-requests to/from table "Worker"
    /// </summary>
    public class WorkerController : Controller
    {
        /// <summary>
        /// Sends list of workers to the View
        /// </summary>
        public ActionResult WorkerList()
        {
            return View(Domain.SqlDataFlow.GetWorkerList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            //adding available companies to choose as worker working place 
            SelectList companies = new SelectList(Domain.SqlDataFlow.GetCompanyList(), "Id", "Title");
            //sending to View
            ViewBag.Companies = companies;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Domain.Worker worker)
        {
            Domain.SqlDataFlow.AddWorker(worker);
            return RedirectToAction("WorkerList");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Domain.Worker worker = Domain.SqlDataFlow.FindWorkerById(id);

            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Domain.Worker worker = Domain.SqlDataFlow.FindWorkerById(id);

            if (worker == null)
            {
                return HttpNotFound();
            }
            Domain.SqlDataFlow.DeleteWorker(worker);
            return RedirectToAction("WorkerList");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Domain.Worker worker = Domain.SqlDataFlow.FindWorkerById(id);

            if (worker == null)
            {
                return HttpNotFound();
            }

            //adding available companies to choose as worker working place 
            SelectList companies = new SelectList(Domain.SqlDataFlow.GetCompanyList(), "Id", "Title");
            //sending to View
            ViewBag.Companies = companies;
            return View(worker);
        }

        [HttpPost]
        public ActionResult Edit(Domain.Worker worker)
        {
            Domain.SqlDataFlow.UpdateWorker(worker);
            return RedirectToAction("WorkerList");
        }
    }
}