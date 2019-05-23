using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;
using TestApplication.Models;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Controllers
{
    public class ApprovalController : BaseController
    {
        // GET: Approval
        public ActionResult PendingTransactions()
        {
            ViewBag.CompanyAccounts = CompanyModel.GetCompanyBankAccounts().Select(x => new { x.Id, x.Name }).ToArray();
            return View();
        }

        public bool UpdateTransactionById(string id, int statusId, int? bankAccountId, DateTime? chqDate, string approvedNote = null)
        {
            try
            {
                var k = Repository.Instance.Find<AmountTransaction>(x => x.Id == id);
                k.StatusId = statusId;
                if (bankAccountId.HasValue)
                    k.CompanyBankAccountId = bankAccountId;
                if (chqDate.HasValue)
                    k.ChequeClearingDate = chqDate;
                if (!string.IsNullOrWhiteSpace(approvedNote))
                    k.ApprovedNote = approvedNote;
                Repository.Instance.Update(k);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string LoadPendingTransactions()
        {
            var pendingStatus = (int)AmountTransactionStatus.Pending;
            var source = Repository.Instance.All<AmountTransaction>().Where(x => x.StatusId == pendingStatus).ToList();
            var t = base.LoadGridAjax<AmountTransaction>(source, null, nameof(MemberAccount) + "." + nameof(Branches),
                                                                     nameof(MemberAccount) + "." + nameof(Member),
                                                                     nameof(MemberAccount) + "." + nameof(Scheme));
            return t;
        }


    }
}