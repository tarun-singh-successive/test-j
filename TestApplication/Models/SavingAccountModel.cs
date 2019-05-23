using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;
using TestApplication.Helpers;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Models
{
    public class SavingAccountModel
    {
        public static int GetForcefullyTransactionPurposeIdByName(string name)
        {
            var transactionPurpose = Repository.Instance.Find<TransactionPurpose>(x => x.Name.ToLower() == name.ToLower());
            return transactionPurpose != null
                    ? transactionPurpose.Id
                    : Repository.Instance.Insert(new TransactionPurpose() { IsActive = false, Name = name }, true).Id;
        }

        public static SelectList GetTransactionPurposes()
        {
            var systemTransactionTypes = ((TransactionType[])Enum.GetValues(typeof(TransactionType))).Select(x => x.ToString().ToLower()).ToList();
            return new SelectList(Repository.Instance.Filter<TransactionPurpose>(x => x.IsActive)
                                                     .Where(x => systemTransactionTypes.Any(y => y == x.Name.ToLower()))
                                                     .ToList(), "Id", "Name");
        }

        public static void CheckDebitTransaction(AmountTransaction amountTransaction)
        {
            if (amountTransaction.TransactionType == (int)TransactionType.Debit && amountTransaction.BalanceLeft > amountTransaction.Amount)
            {
                throw new Exception();
            }
        }

        public static void InsertUpdateAmountTransaction(AmountTransaction amountTransaction, int? _memberAccountId = null)
        {
            CheckDebitTransaction(amountTransaction);

            amountTransaction.Id = Common.UniqueTransactionId();
            if (_memberAccountId.HasValue)
                amountTransaction.MemberAccountId = amountTransaction.MemberAccountId == 0 ? _memberAccountId.Value : amountTransaction.MemberAccountId;
            Repository.Instance.Update(amountTransaction);
        }
    }
}