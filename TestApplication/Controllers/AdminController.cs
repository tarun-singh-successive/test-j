using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;

namespace TestApplication.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTransactionPurpose(int? Id, bool? IsView)
        {
            ViewBag.IsView = IsView;
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            if (!Id.HasValue)
            {
                return View();
            }
            else
            {
                var tp = Repository.Instance.Find<TransactionPurpose>(x => x.Id == Id);
                return View(tp);
            }
        }

        [HttpPost]
        public ActionResult AddTransactionPurpose(TransactionPurpose transactionPurpose)
        {
            if (ModelState.IsValid)
            {
                if (transactionPurpose.Id == 0)
                {
                    Repository.Instance.Insert(transactionPurpose, true);
                }
                else
                {
                    Repository.Instance.Update(transactionPurpose);
                    Repository.Instance.Commit();
                }
                return RedirectToAction("TransactionPurposes");
            }
            return View();
        }

        public ActionResult TransactionPurposes()
        {
            return View();
        }

        public string LoadTranscationPurposes()
        {
            return base.LoadGridAjax<TransactionPurpose>();
        }

    }
}