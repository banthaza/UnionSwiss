using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Domain.Service
{
    public interface IPaySlipService: IService
    {

        EmployeePayslip GeneratePayslip(Employee employee, DateTime startDate, DateTime endDate);
        long CalculateIncomeTax(long anualSalary, List<TaxBracket> taxRules);
        long CalculateGrossIncome(long anualSalary);
        long CalculateNetIncome(long grossIncome,long incomeTax);
        long CalculatePensionContrabution(long grossIncome, int pensionContrabution);
    }
}
