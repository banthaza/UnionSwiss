using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Domain.Calculators
{
    public interface IIncomeCalculator
    {
        int CalculateMonthlyTax(int anualSalary, int minQualifyingValue, int baseTaxValue,
            decimal incrementMultiplier);
        int CalculateNetIncome(int monthlyGross, int tax);
        int CalculatePension(int monthlyGross, int pensionPercentage);
        int CalculateMonthlyGross(int annualSalary);

    }
}