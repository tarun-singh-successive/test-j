using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;
using TestApplication.Helpers;

namespace TestApplication.Controllers
{
    //[LoggedOrAuthorized]
    public class CompanyController : BaseController
    {
        // GET: Company
        public ActionResult CompanyProfile()
        {
            ViewBag.AllCountries = Repository.Instance.All<Country>().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).AsEnumerable();
            ViewBag.IndiaStates = Repository.Instance.All<State>().Where(x => x.CountryId == 101)
                                                                  .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
                                                                  .AsEnumerable();
            var compnyProfile = Repository.Instance.All<CompanyProfile>().OrderByDescending(x => x.LastModificationDate).FirstOrDefault();
            return View(compnyProfile ?? new CompanyProfile());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyProfile(CompanyProfile companyProfile)
        {
            if (ModelState.IsValid && companyProfile != null)
            {
                companyProfile.LastModificationDate = DateTime.Now;
                Repository.Instance.Insert<CompanyProfile>(companyProfile, true);
                RedirectToAction("ViewCompanyProfile");
            }
            return View();
        }


        public ActionResult ViewCompanyProfile()
        {
            var compnyProfile = Repository.Instance.All<CompanyProfile>().OrderByDescending(x => x.LastModificationDate).FirstOrDefault();
            return View(compnyProfile);
        }

        public ActionResult AddBranch(int? Id = null, bool? IsView = null)
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
                var tp = Repository.Instance.Find<Branches>(x => x.Id == Id);
                return View(tp);
            }
        }

        public ActionResult Branches()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBranch(Branches branch)
        {
            if (ModelState.IsValid)
            {
                if (branch.Id == 0)
                {
                    Repository.Instance.Insert(branch, true);
                }
                else
                {
                    Repository.Instance.Update(branch);
                    Repository.Instance.Commit();
                }
                return RedirectToAction("Branches");
            }
            return View();
        }


        public ActionResult AddBankAccount(int? Id = null, bool? IsView = null)
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
                var tp = Repository.Instance.Find<CompanyBankAccount>(x => x.Id == Id);
                return View(tp);
            }
        }

        [HttpPost]
        public ActionResult AddBankAccount(CompanyBankAccount companyBankAccount)
        {
            if (ModelState.IsValid)
            {
                if (companyBankAccount.Id == 0)
                {
                    Repository.Instance.Insert(companyBankAccount, true);
                }
                else
                {
                    Repository.Instance.Update(companyBankAccount);
                    Repository.Instance.Commit();
                }
                return RedirectToAction("BankAccounts");
            }
            return View();
        }

        public ActionResult BankAccounts()
        {
            return View();
        }

        public JsonResult GetStatesByCountryId(int countryId = 101)
        {
            return Json(Repository.Instance.Filter<State>(x =>
                       x.CountryId == countryId).OrderBy(x => x.Name)
                       .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public string LoadBranches()
        {
            return LoadGridAjax<Branches>();
        }

        public string LoadBankAccounts()
        {
            var source = Repository.Instance.All<CompanyBankAccount>().ToList();
            return LoadGridAjax(source);
        }

    }
}