using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class CompanyProfile
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "About Company")]
        public string AboutCompany { get; set; }

        [Display(Name = "CIN No.")]
        public string CIN { get; set; }

        [Display(Name = "PAN No.")]
        public string PAN { get; set; }

        [Display(Name = "TAN No.")]
        public string TAN { get; set; }

        [Display(Name = "Company Category")]
        public int CompanyCategoryId { get; set; }

        [Display(Name = "Company Class")]
        public string CompanyClass { get; set; }

        [Display(Name = "Authorized Capital")]
        public decimal AuthorizedCapital { get; set; }

        [Display(Name = "Paid-up Capital")]
        public decimal PaidupCapital { get; set; }

        [Display(Name = "Share Nominal Value")]
        public decimal ShareNominalValue { get; set; }

        [Display(Name = "State Of Registration")]
        public int StateOfRegistration { get; set; }

        [Display(Name = "Incorporation Country")]
        public int IncorporationCountryId { get; set; }

        [Display(Name = "Incorporation State")]
        public int IncorporationStateId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Incorporation Date")]
        public DateTime IncorporationDate { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Landline")]
        public string Landline { get; set; }

        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "Pincode")]
        public int Pincode { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public DateTime LastModificationDate { get; set; }

        [Display(Name ="Address")]
        public string Address => Address1 + ", " + Address2;

        public  virtual Country Country { get; set; }

        public virtual State State { get; set; }

    }
}