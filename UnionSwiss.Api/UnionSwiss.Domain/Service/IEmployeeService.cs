using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Domain.Service
{
    public interface IEmployeeService: IService
    {
        Employee GetEmployee(long employeeId);
        EmployeePayslip GetPayslip(long employeeId, long taxPeriodId);
        List<Employee> ListEmployees();
        EmployeePayslip CreatePaySlip(long employeeId, DateTime periodDate);
        Employee SaveEmployee(Employee employee);
        void Delete(long employeeId);
 

    }
}
