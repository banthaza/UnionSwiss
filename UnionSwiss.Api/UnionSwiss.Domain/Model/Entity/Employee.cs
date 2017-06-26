using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.Domain.Model.Entity
{
    public class Employee: IEntity<long>
    {

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualSalary { get; set; }
        public int PensionContributionPercentage { get; set; }
        
        public DateTime MicrosoftStartDate { get; set; }

        //This ia a complete hack since microsoft dont have a simple date object
        //and the serilisation dose this non-sence yyyy-mm-ddT00:00 with T00:00 causing issues for angular controls
        public string StartDate
        {
            get { return MicrosoftStartDate.ToString("yyyy-MM-dd"); }
            set
            {
                Guard.ArgumentIsValidDate(value, nameof(StartDate));
                MicrosoftStartDate = DateTime.Parse(value);

            }
        }

 
        public List<EmployeePayslip> Payslips { get; set; }

        public Employee()
        {
            Payslips=new List<EmployeePayslip>();
        }



    }
}
