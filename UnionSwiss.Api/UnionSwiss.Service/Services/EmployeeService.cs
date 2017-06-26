using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using UnionSwiss.Domain.Calculators;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Domain.Repository;
using UnionSwiss.Domain.Service;

namespace UnionSwiss.Service.Services
{   
    public class EmployeeService: Service, IEmployeeService
    {
        private readonly IRepository<long> _repositroy;
        private readonly IIncomeCalculator _incomeCalculator;
        public EmployeeService(IRepository<long> repositroy, IIncomeCalculator incomeCalculator)
        {
            Guard.ArgumentNotNull(repositroy, nameof(repositroy));
            Guard.ArgumentNotNull(incomeCalculator, nameof(incomeCalculator));

            _repositroy = repositroy;
            _incomeCalculator = incomeCalculator;
        }

        public Employee GetEmployee(long employeeId)
        {
            Guard.ArgumentNotZero(employeeId, nameof(employeeId));

           var employee= _repositroy.Query<Employee>(x=>x.Id== employeeId)
                        .Include(x=>x.Payslips)        
                        .FirstOrDefault();

            if(employee==null)
                throw  new InvalidDataException($"No employee exists for employee Id {employeeId}");

            return employee;
        }

        public EmployeePayslip GetPayslip(long employeeId, long taxPeriodId)

        {
            Guard.ArgumentNotZero(employeeId, nameof(employeeId));
            Guard.ArgumentNotZero(taxPeriodId, nameof(taxPeriodId));

            var payslip = _repositroy.Get<EmployeePayslip>(x => x.Id == employeeId && x.TaxPeriodId == taxPeriodId);

            if (payslip == null)
                throw new InvalidDataException($"No Payslip exists for employeeId: {employeeId}  taxPeriodId:{taxPeriodId}");


          
            return payslip;
        }

        public List<Employee> ListEmployees()
        {
            return _repositroy.Query<Employee>(x=> true).ToList();
        }

        public Employee SaveEmployee(Employee employee)
        {
            Guard.ArgumentNotNull(employee, nameof(employee));
            Guard.ArgumentNotNullOrEmpty(employee.FirstName, "FirstName");
            Guard.ArgumentNotNullOrEmpty(employee.LastName, "LastName");
            Guard.ArgumentGreaterZero(employee.AnnualSalary, "AnnualSalary");
            Guard.ArgumentBetween(employee.PensionContributionPercentage, -1, 51, "PensionContributionPercentage");
      

            Guard.ArgumentIsValidDate(employee.MicrosoftStartDate, "StartDate");//Start Dates casue we has to hack it due to javascript epoc date issues
            employee.Payslips = null;//Er dont want to update payslips if they are here
            _repositroy.Save<Employee>(employee, () => employee.Id == 0);

            return GetEmployee(employee.Id);
        }
        public void Delete(long employeeId)
        {
            Guard.ArgumentNotZero(employeeId,nameof(employeeId) );
            _repositroy.Delete<Employee>(x => x.Id == employeeId);
        }
        public EmployeePayslip CreatePaySlip(long employeeId, DateTime periodDate)
        {
            Guard.ArgumentNotZero(employeeId, nameof(employeeId));
            Guard.ArgumentIsValidDate(periodDate, nameof(periodDate));

            var employee = _repositroy.Get<Employee>(x => x.Id == employeeId);
            if(employee==null)
                throw new InvalidDataException($"No Employee exists for {employeeId}");
            
            var taxPeriod = _repositroy.Get<TaxPeriod>(x => x.StartDate <= periodDate && x.EndDate >= periodDate) ;
               if(taxPeriod == null)
                throw new InvalidDataException($"No tax period exists for period {periodDate}");

            var exitingPayslip = _repositroy.Get<EmployeePayslip>(x => x.EmployeeId == employeeId && x.TaxPeriodId == taxPeriod.Id);

            if(exitingPayslip!=null)
                throw new InvalidDataException($"A payslip already exists for {employee.FirstName} {employee.LastName} : {periodDate}");

            var taxRule =
                _repositroy.Query<TaxBracket>(
                    x =>
                        x.TaxYearId == taxPeriod.TaxYearId && x.MinQualifyingValue <= employee.AnnualSalary &&
                        x.MaxQualifyingValue >= employee.AnnualSalary).FirstOrDefault();
            
            if(taxRule==null)
                 throw new InvalidDataException($"No TaxBracket  exists for {employee.FirstName} {employee.LastName} for tax period {taxPeriod.Name}");
            var tax= _incomeCalculator.CalculateMonthlyTax(employee.AnnualSalary, taxRule.MinQualifyingValue, taxRule.BaseTaxValue, taxRule.IncrementMultiplier);
            var gross = _incomeCalculator.CalculateMonthlyGross(employee.AnnualSalary);
            var net = _incomeCalculator.CalculateNetIncome(gross, tax);
            var pensionContribution = _incomeCalculator.CalculatePension(gross, employee.PensionContributionPercentage);
            var payslip = new EmployeePayslip()
            {
                EmployeeId = employee.Id,
                FullName = $"{employee.FirstName} {employee.LastName}",
                GrossIncome = gross,
                IncomeTax = tax,
                NetIncome = net,
                Pension = pensionContribution,
                TaxPeriodId = taxPeriod.Id,
                PayPeriod = taxPeriod.Name
            };

             _repositroy.Save(payslip, () => payslip.Id == 0);

            //We get it again to keep stuff consitant.
            return payslip;
        }

    
    }
}
