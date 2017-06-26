using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnionSwiss.Domain.Calculators;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Domain.Repository;
using UnionSwiss.Domain.Service;
using UnionSwiss.Service.Calculators;

namespace UnionSwiss.Service.Services
{
    public class FinanceAdminService : Service, IFinanceAdminService
    {
        private readonly IRepository<long> _repositroy;
        private readonly ITaxYearGenerator _taxYearGenerator;

        public FinanceAdminService(IRepository<long> repositroy, ITaxYearGenerator taxYearGenerator )
        {
            Guard.ArgumentNotNull(repositroy, nameof(repositroy));
            Guard.ArgumentNotNull(taxYearGenerator, nameof(taxYearGenerator));

            _repositroy = repositroy;
            _taxYearGenerator = taxYearGenerator;
        }

        public TaxYear GetTaxYear(long id)
        {
           return _repositroy.Get<TaxYear>(x=>x.Id== id);
        }

        public List<TaxYear> ListTaxYears()
        {
            return _repositroy.Query<TaxYear>(x=> true)
                   .Include(x=>x.TaxPeriods)
                   .Include(x=>x.TaxBrackets)  
                .ToList();
        }

        public TaxYear GenerateTaxYear(DateTime startDate)
        {
            var taxYear = _repositroy.Get<TaxYear>(x => x.StartDate == startDate) ;
            if (taxYear == null)
            {
                taxYear =  new TaxYear();
                taxYear.StartDate = startDate;
                taxYear.EndDate = startDate.AddYears(1).AddDays(-1);
                _repositroy.Save(taxYear, () => taxYear.Id == 0);

                for (var i = 0; i < 12; i++)
                {
                    var period = new TaxPeriod();
                    period.TaxYearId = taxYear.Id;
                    period.StartDate = startDate.AddMonths(i);
                    period.EndDate = startDate.AddMonths(i+1).AddDays(-1);
                    _repositroy.Save<TaxPeriod>(period, () => period.Id==0);
                    taxYear.TaxPeriods.Add(period);
                }
            }

            return taxYear;
        }

        public TaxYear SaveTaxYear(TaxYear taxYear)
        {
            _repositroy.Save(taxYear, () => taxYear.Id == 0);
            return taxYear;
        }

    }
}
