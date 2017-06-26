using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Domain.Calculators;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Service.Calculators
{
    public class IncomeCalculator : IIncomeCalculator
    {
     

        public int CalculateMonthlyGross(int annualSalary)
        {
            return (int)Math.Floor(annualSalary/12.00m);
        }

        public int CalculatePension(int monthlyGross, int pensionPercentage)
        {
            Guard.ArgumentBetween(pensionPercentage, 0, 50, nameof(pensionPercentage));
            return (int) Math.Floor(monthlyGross * (pensionPercentage/100.00));
        }

        public int CalculateNetIncome(int monthlyGross, int tax)
        {
            return monthlyGross - tax;
        }

        public int CalculateMonthlyTax(int anualSalary, int minQualifyingValue , int baseTaxValue, decimal incrementMultiplier )
        {
            var multiplied = (anualSalary - (minQualifyingValue-1)) * incrementMultiplier;
            return (int)Math.Ceiling((baseTaxValue + multiplied) /12);

        }
    }
}
