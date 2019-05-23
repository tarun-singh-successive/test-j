using System;
using System.Linq;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;
using TestApplication.Helpers;
using TestApplication.Models;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Controllers
{
    public class FdController : BaseController
    {
        // GET: Fd
        public ActionResult AddFdScheme(int? Id = null, bool? IsView = false)
        {
            ViewBag.SchemeType = (int)AccountType.Fd;
            ViewData["MinimumAmountText"] = "Min FD/MIS Amount";
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            if (Id.HasValue)
            {
                var scheme = Repository.Instance.Filter<Scheme>(x => x.Id == Id).FirstOrDefault();
                return View(scheme ?? new Scheme());
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddFdScheme(Scheme scheme)
        {
            if (ModelState.IsValid)
            {
                if (scheme.Id == 0)
                {
                    scheme.SchemeTypeId = (int)AccountType.Fd;
                    scheme.CreationDate = DateTime.Now;
                    var newRecord = Repository.Instance.Insert(scheme, true);
                }
                else
                {
                    var oldScheme = Repository.Instance.Filter<Scheme>(x => x.Id == scheme.Id).FirstOrDefault();
                    oldScheme.SchemeCode = scheme.SchemeCode;
                    oldScheme.Name = scheme.Name;
                    oldScheme.MinimumAmount = scheme.MinimumAmount;
                    Repository.Instance.Update(scheme);
                    Repository.Instance.Commit();
                }
                return RedirectToAction("Schemes");
            }
            return View();
        }

        public ActionResult AddFdAccount(int? Id = null, bool? IsView = false)
        {
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            var k = new FdAccount();
            k.FdMemberAccount = k.FdMemberAccount ?? new MemberAccount();
            return View(k);
        }

        [HttpPost]
        public ActionResult AddFdAccount(FdAccount fdAccount)
        {
            ViewData["disablecontrols"] = false;
            if (ModelState.IsValid)
            {
                if (fdAccount.Id == 0)
                {
                    fdAccount.CreationDate = fdAccount.FdMemberAccount.CreationDate = fdAccount.CreationDate = DateTime.Now;
                    fdAccount.FdAmount = fdAccount.FdMemberAccount.AmountTransaction.Amount.Value;
                    fdAccount.FdMemberAccount.AccountTypeId = (int)AccountType.Fd;
                    fdAccount.FdMemberAccount.AmountTransaction.TransactionType = (int)TransactionType.Credit;
                    fdAccount.FdMemberAccount.AmountTransaction.StatusId = (int)AmountTransactionStatus.Pending;
                    fdAccount.FdMemberAccount.AmountTransaction.Id = Common.UniqueTransactionId();
                    fdAccount.FdMemberAccount.AmountTransactions.Add(fdAccount.FdMemberAccount.AmountTransaction);
                    var newRecord = Repository.Instance.Insert(fdAccount, true);
                }
                else
                {
                    Repository.Instance.Update(fdAccount);
                    Repository.Instance.Commit();
                }
                return RedirectToAction("AddFdAccount");
            }
            return View();
        }

        public ActionResult FdAccounts()
        {
            var amt = FdModels.CalculateInterest(6500, 7.3, 1, 365);
            return View();
        }

        public ActionResult CloseFd()
        {
            return View();
        }

        public ActionResult FdProfile(int? Id = null, bool? IsView = false)
        {
            var fd = Repository.Instance.Find<FdAccount>(x => x.Id == Id);
            return View(fd ?? new FdAccount());
        }

        #region Ajax Grid Methods

        public string LoadFdAccounts()
        {
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            var source = Repository.Instance.All<FdAccount>().ToList();
            return base.LoadGridAjax(source, x => x.FdMemberAccount.Member.Name.ToLower().IndexOf(searchValue.ToLower()) != -1
                                                 || x.Scheme.Name.ToLower().IndexOf(searchValue.ToLower()) != -1);
        }

        public string LoadSchemes()
        {
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            return base.LoadGridAjax(Repository.Instance.Filter<Scheme>(x => x.SchemeTypeId == (int)AccountType.Fd),
                                      x => x.SchemeCode.ToLower().IndexOf(searchValue.ToLower()) != -1
                                          || x.Name.ToLower().IndexOf(searchValue.ToLower()) != -1);
        }

        #endregion

        public ActionResult Schemes()
        {
            return View();
        }
    }
}