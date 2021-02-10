using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkersRegistrationApp.Controllers
{
    /// <summary>
    /// The <c>CompanyController</c> class.
    /// Contains methods for passing results of SQL-requests to/from table "Company"
    /// </summary>
    public class CompanyController : Controller
    {
        /// <summary>
        /// Sends list of companies to the View
        /// </summary>
        public ActionResult CompaniesList()
        {
            return View(Domain.SqlDataFlow.GetCompanyList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Domain.Company company)
        {
            Domain.SqlDataFlow.AddCompany(company);

            return RedirectToAction("CompaniesList");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Domain.Company company = Domain.SqlDataFlow.FindCompanyById(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Domain.Company company = Domain.SqlDataFlow.FindCompanyById(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            Domain.SqlDataFlow.DeleteCompany(company);

            return RedirectToAction("CompaniesList");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Domain.Company company = Domain.SqlDataFlow.FindCompanyById(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost]
        public ActionResult Edit(Domain.Company company)
        {
            Domain.SqlDataFlow.UpdateCompany(company);
            return RedirectToAction("CompaniesList");
        }
    }


}