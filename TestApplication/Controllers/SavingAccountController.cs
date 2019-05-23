using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;
using TestApplication.Helpers;
using TestApplication.Models;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Controllers
{
    public class SavingAccountController : BaseController
    {
        private int _memberAccountId;
        private Uri MemberAccountIdd =>  HttpContext.Request.Url;
        // GET: SavingAccounts
        public ActionResult AddSavingAccountScheme(int? Id = null, bool? IsView = false)
        {
            ViewBag.SchemeType = (int)AccountType.SavingAccount;
            ViewData["MinimumAmountText"] = "Min Opening Balance";
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
        public ActionResult AddSavingAccountScheme(Scheme scheme)
        {
            ViewBag.SchemeType = (int)AccountType.SavingAccount;
            if (ModelState.IsValid)
            {
                if (scheme.Id == 0)
                {
                    scheme.SchemeTypeId = (int)AccountType.SavingAccount;
                    scheme.CreationDate = DateTime.Now;
                    var newRecord = Repository.Instance.Insert(scheme, true);
                }
                else
                {
                    var oldScheme = Repository.Instance.Filter<Scheme>(x => x.Id == scheme.Id).FirstOrDefault();
                    if (scheme != null)
                    {
                        oldScheme.Name = scheme.Name;
                        oldScheme.SchemeCode = scheme.SchemeCode;
                        oldScheme.MinimumMonthlyAverageBalance = scheme.MinimumMonthlyAverageBalance;
                        oldScheme.MinimumAmount = scheme.MinimumAmount;
                        oldScheme.AnnualInterestRate = scheme.AnnualInterestRate;
                        oldScheme.InterestPayType = scheme.InterestPayType;
                        oldScheme.IsActive = scheme.IsActive;
                    }
                    Repository.Instance.Update(oldScheme);
                    Repository.Instance.Commit();

                }
                return RedirectToAction("SavingAccountSchemes");
            }
            return View();
        }

        public ActionResult SavingAccountSchemes()
        {
            return View();
        }

        public ActionResult AddSavingAccount(int? Id = null, bool? IsView = false)
        {
            ViewBag.SchemeType = (int)AccountType.SavingAccount;
            ViewData["MinimumAmountText"] = "Min Opening Balance";
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            if (Id.HasValue)
            {
                var savingAccount = Repository.Instance.Filter<MemberAccount>(x => x.Id == Id).FirstOrDefault();
                return View(savingAccount ?? new MemberAccount());
            }
            return View(new MemberAccount());
        }

        [HttpPost]
        public ActionResult AddSavingAccount(MemberAccount account)
        {
            if (ModelState.IsValid)
            {
                if (account.Id == 0)
                {
                    account.AccountTypeId = (int)AccountType.SavingAccount;
                    var accountTransaction = account.AmountTransaction;
                    accountTransaction.Id = Common.UniqueTransactionId();
                    accountTransaction.TransactionType = (int)TransactionType.Credit;
                    accountTransaction.Amount = account.MinAmount;
                    account.AmountTransactions.Add(accountTransaction);
                    var newRecord = Repository.Instance.Insert(account, true);
                }
                else
                {
                    Repository.Instance.Update(account);
                    Repository.Instance.Commit();
                }
            }
            return View(new MemberAccount());
        }

        public ActionResult SavingAccounts()
        {
            return View();
        }

        public ActionResult ViewTransaction()
        {
            return View();
        }

        public ActionResult AccountDashboard(int? id = 12, bool? isView = false)
        {
            var currentMemberAccount = Repository.Instance.Find<MemberAccount>(x => x.Id == id);
            currentMemberAccount.AmountTransaction.PanNo = currentMemberAccount.Member.PanNo;
            currentMemberAccount.AmountTransaction.MemberAccountId = currentMemberAccount.Id;
            _memberAccountId = int.TryParse(Convert.ToString(RouteData.Values["Id"]), out int memberAccountId) ? memberAccountId : 0;
            var k = currentMemberAccount.AmountTransaction.BalanceLeft;
            return View(currentMemberAccount);
        }

        public ActionResult Load(string id)
        {
            var model = Repository.Instance.Find<AmountTransaction>(x => x.Id == id);
            return PartialView("Accounts/_ViewTransaction", model);
        }

        [HttpPost]
        public bool AddDeposit(AmountTransaction transaction)
        {
            try
            {
                transaction.TransactionType = (int)TransactionType.Credit;
                transaction.StatusId = (int)AmountTransactionStatus.Pending;
                transaction.TransactionPurposeId = SavingAccountModel.GetForcefullyTransactionPurposeIdByName(TransactionPurposeType.Deposit.ToString());
                SavingAccountModel.InsertUpdateAmountTransaction(transaction, _memberAccountId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Withdraw(AmountTransaction transaction)
        {
            try
            {
                
                transaction.TransactionType = (int)TransactionType.Debit;
                transaction.StatusId = (int)AmountTransactionStatus.Pending;
                transaction.TransactionPurposeId = SavingAccountModel.GetForcefullyTransactionPurposeIdByName(TransactionPurposeType.Withdraw.ToString());
                SavingAccountModel.InsertUpdateAmountTransaction(transaction);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool AddOtherCharges(AmountTransaction transaction)
        {
            try
            {
                transaction.TransactionType = (int)TransactionType.Debit;
                transaction.StatusId = (int)AmountTransactionStatus.Pending;
                SavingAccountModel.InsertUpdateAmountTransaction(transaction, _memberAccountId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool CloseAccount(MemberAccount memberAccount)
        {
            try
            {
                var amountTransaction = memberAccount.AmountTransaction;
                amountTransaction.Id = Common.UniqueTransactionId();
                amountTransaction.TransactionType = (int)TransactionType.Debit;
                amountTransaction.StatusId = (int)AmountTransactionStatus.Pending;
                amountTransaction.TransactionPurposeId = SavingAccountModel.GetForcefullyTransactionPurposeIdByName(TransactionPurposeType.Closing.ToString());
                memberAccount.AmountTransactions.Add(amountTransaction);
                Repository.Instance.Update(memberAccount);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult ViewTransaction(Nominees nominees)
        {
            return View();
        }

        #region Load Grid Ajax Methods

        public string LoadSchemes()
        {
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            return base.LoadGridAjax(Repository.Instance.Filter<Scheme>(x => x.SchemeTypeId == (int)AccountType.SavingAccount),
                                      x => x.SchemeCode.ToLower().IndexOf(searchValue.ToLower()) != -1
                                          || x.Name.ToLower().IndexOf(searchValue.ToLower()) != -1);
        }

        public string LoadAccounts()
        {
            var source = Repository.Instance.Filter<MemberAccount>(x => x.AccountTypeId == (int)AccountType.SavingAccount).ToList()
                .Select(x => new
                {
                    MemberName = x.Member.Name,
                    x.Member.Mobile,
                    Branch = "Branch Name",
                    x.Id,
                    Scheme = x.Scheme.Name,
                    x.CreationDate,
                    x.StatusId,
                    TotalBalance = (x.AmountTransactions.Where(y => y.TransactionType == 1).Sum(y => y.Amount) - x.AmountTransactions.Where(y => y.TransactionType == 2).Sum(y => y.Amount))
                }).ToList();


            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            return base.LoadGridAjax(source,
                                      x => x.MemberName.ToLower().IndexOf(searchValue.ToLower()) != -1
                                          || x.Mobile.ToLower().IndexOf(searchValue.ToLower()) != -1);
        }

        public string LoadViewTransactions()
        {
            var source = _memberAccountId == 0 ? null : Repository.Instance.Filter<AmountTransaction>(x => x.MemberAccountId == _memberAccountId).ToList();
            return base.LoadGridAjax<AmountTransaction>(source);
        }

        #endregion
    }
}