using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkersRegistrationApp.Controllers
{
    public class WorkerController : Controller
    {
        public ActionResult WorkerList()
        {
            return View(Domain.SqlDataFlow.GetWorkerList());
        }

        [HttpGet]
        public ActionResult Create()
        {
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