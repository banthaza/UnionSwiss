using System;
using System.Collections.Generic;
using UnionSwiss.Domain.Calculators;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Service.Calculators
{
    public class TaxYearGenerator : ITaxYearGenerator
    {
        public TaxYear GenerteTaxYear(DateTime startDate)
        {
            var taxYear = new TaxYear();
            taxYear.StartDate = startDate;
            taxYear.EndDate = startDate.AddYears(1).AddDays(-1);
         
            return taxYear;
        }

        public List<TaxPeriod> GenerateTaxPeriods(TaxYear taxYear)
        {

            for (var i = 0; i < 12; i++)
            {
                var period = new TaxPeriod();
                period.TaxYearId = taxYear.Id;
                period.StartDate = taxYear.StartDate.AddMonths(i);
                period.EndDate = taxYear.StartDate.AddMonths(1).AddDays(-1);

                taxYear.TaxPeriods.Add(period);
            }
            return taxYear.TaxPeriods;
        } 
    }
}
