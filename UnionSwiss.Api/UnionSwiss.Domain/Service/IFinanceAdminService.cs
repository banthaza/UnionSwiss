using System;
using System.Collections.Generic;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Domain.Service
{
    public interface IFinanceAdminService
    {
        TaxYear GetTaxYear(long id);
        TaxYear GenerateTaxYear(DateTime startDate);
        TaxYear SaveTaxYear(TaxYear taxYear);
        List<TaxYear> ListTaxYears();
    }
}