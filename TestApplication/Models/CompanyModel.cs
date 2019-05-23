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
    public class CompanyModel
    {
        public static SelectList GetStatesByCountryId(int countryId = 101)
        {
            return new SelectList(Repository.Instance.Filter<State>(x =>
                        x.CountryId == countryId).OrderBy(x => x.Name), "Id", "Name");
        }

        public static List<SelectListItem> LoadCompanyCategories()
        {
            return ((CompanyCategory[])Enum.GetValues(typeof(CompanyCategory)))
                                       .Select(x=> new SelectListItem() { Text = x.GetDescription(), Value = ((int)x).ToString()}).ToList();
        }

        public static List<CompanyBankAccount> GetCompanyBankAccounts() => Repository.Instance.Filter<CompanyBankAccount>(x => x.IsActive).ToList();
        
    }
}