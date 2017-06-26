using System;
using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.Domain.Model.Entity
{
    public class EmployeePayslip:IEntity<long>
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long TaxPeriodId { get; set; }
        public string FullName { get; set; }
        public int GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public int NetIncome { get; set; }
        public int Pension { get; set; }
        
        public string PayPeriod { get; set; }

        public virtual TaxPeriod TaxPeriod { get; set; }
        public EmployeePayslip()
        {
            PayPeriod = "";
        }
    }
}