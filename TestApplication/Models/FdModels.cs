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
    public class FdModels
    {
        public static List<SelectListItem> GetTenureTypes()
        {
            return ((TenureType[])Enum.GetValues(typeof(TenureType)))
                                       .Select(x => new SelectListItem() { Text = x.ToString(), Value = ((int)x).ToString() }).ToList();
        }

        public static List<SelectListItem> GetInterestPayoutCycle()
        {
            return ((InterestPayoutType[])Enum.GetValues(typeof(InterestPayoutType)))
                                       .Select(x => new SelectListItem() { Text = x.GetDescription(), Value = ((int)x).ToString() }).ToList();
        }

        public static List<SelectListItem> GetSavingAccountSchemes() => Repository.Instance.Filter<Scheme>(x => x.SchemeTypeId == (int)AccountType.SavingAccount)
                                                                        .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

        public static List<SelectListItem> GetFdAccountSchemes() => Repository.Instance.Filter<Scheme>(x => x.SchemeTypeId == (int)AccountType.Fd)
                                                                        .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

        public static double CalculateInterest(double principalAmount, double rate, int payOutTimes, int days)
        {
            return Math.Round(((principalAmount * rate * (days + 1)) / (100 * 365)) * 100) / 100;
        }

        public double CalculateInt(double DepositeAmount, double Interest, int MonthDuration, int DayDuration, double TDS, double Senior, int IntCapitalized, DateTime StartDate, DateTime EndDate)
        {
            double amount = 0.00;
            try
            {
                var close = 0;
                var dy = Convert.ToInt32(StartDate.Day);
                var yr = StartDate.Year;
                var mon = StartDate.Month;
                var fomdate = new DateTime(yr, mon, 1);
                var todate = new DateTime(yr, mon, 1).AddMonths(1).AddDays(-1);
                fomdate = StartDate;

                var enddate = new DateTime(yr, mon, dy).AddMonths(MonthDuration);



                var roun = Convert.ToInt32(MonthDuration / IntCapitalized);
                var diff = MonthDuration % 3;
                if (diff > 0)
                {
                    diff = (roun * (IntCapitalized + 1)) + (diff + 1);
                }

                var diffe = todate.Subtract(StartDate).Days;  // new Date(todate - StartDate);
                var days = diffe;


                var intamt = 0.00;
                var counmon = 1;

                for (var i = 1; i <= diff; i++)
                {
                    if (i == 1)
                    {
                        fomdate = StartDate;
                    }
                    else
                    {
                        fomdate = new DateTime(yr, mon, 1).AddMonths(counmon - 1);
                    }
                    todate = new DateTime(yr, mon, 1).AddMonths(counmon).AddDays(-1);



                    if (i == diff)
                    {
                        todate = new DateTime(yr, mon, dy - 1).AddMonths(counmon - 1);
                        // diffe = todate.Subtract(fomdate).Days;   /// new Date(todate - fomdate);
                    }
                    else if ((i % (IntCapitalized + 1)) == 0)
                    {
                        todate = new DateTime(yr, mon, dy - 1).AddMonths(counmon - 1);
                        // diffe = todate.Subtract(fomdate).Days;   /// new Date(todate - fomdate);
                    }
                    else if ((i > 1) && ((i % (IntCapitalized + 1)) == 1))
                    {
                        fomdate = new DateTime(yr, mon, dy).AddMonths(counmon - 1);
                        // diffe = todate.Subtract(fomdate).Days;   /// new Date(todate - fomdate);
                        counmon += 1;
                    }
                    else
                    {
                        //diffe = todate.Subtract(fomdate).Days;  // new Date(todate - fomdate);
                        counmon += 1;
                    }
                    if (EndDate < todate)
                    {
                        todate = EndDate;
                        close = 1;
                    }

                    diffe = todate.Subtract(fomdate).Days;   /// new Date(todate - fomdate);

                    if (i == 1)
                    {
                        days = diffe + 1;
                    }  /// / 1000 / 60 / 60 / 24;
                    else
                    {
                        days = diffe;
                    }

                    var lintamt = Math.Round(((DepositeAmount * Interest * (days + 1)) / (100 * 365)) * 100) / 100;
                    intamt += lintamt;
                    if (((i % (IntCapitalized + 1)) == 0) || (i == diff))
                    {
                        DepositeAmount += Math.Round(intamt);
                    }


                    if (((i % (IntCapitalized + 1)) == 0) || (i == diff))
                    {
                        intamt = 0;
                    }
                    else
                    {
                    }

                    amount = DepositeAmount;
                    if (close == 1)
                    {
                        break;
                    }
                    ////   return DepositeAmount;

                }

            }
            catch (Exception ex)
            {

            }
            //var years = StartDate.Year;
            //var months = StartDate.Month;
            //var monendDate = new DateTime(years, months, 1);
            //monendDate = monendDate.AddMonths(1).AddDays(-1);
            //var monstartdate = new DateTime(years, months, 1);
            //var daydiffend = monendDate.Subtract(StartDate).Days;
            //var daydiffstart = StartDate.Subtract(monstartdate).Days;


            return amount;
        }


    }
}