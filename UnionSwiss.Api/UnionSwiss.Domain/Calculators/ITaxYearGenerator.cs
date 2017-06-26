using System;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Domain.Calculators
{
    public interface ITaxYearGenerator
    {
        TaxYear GenerteTaxYear(DateTime startDate);
    }
}