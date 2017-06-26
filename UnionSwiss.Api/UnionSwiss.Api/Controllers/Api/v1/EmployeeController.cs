using System;
using System.Collections.Generic;
using System.Web.Http;
using UnionSwiss.Api.Controllers.Api.Filters;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Domain.Service;

namespace UnionSwiss.Api.Controllers.Api.v1
{
    [ApiExceptionFilter]
    [RoutePrefix("api/v1/Employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
    
        public EmployeeController(IEmployeeService employeeService)
        {
            Guard.ArgumentNotNull(employeeService, nameof(employeeService));
            _employeeService = employeeService;
        }

        [Route()]
        [HttpGet]

        public IEnumerable<Employee> Get()
        {
           return  _employeeService.ListEmployees();
        }

        [Route("{id}")]
        [HttpGet]
        // GET: api/v1/Employees/1
        public Employee Get(long id)
        {
            return _employeeService.GetEmployee(id);
        }

        [Route("{employeeId}/payslip/{periodDateString}")]
        [HttpPost]
        public EmployeePayslip Get(long employeeId, string periodDateString)
        {
            var date = DateTime.Parse(periodDateString);
            return _employeeService.CreatePaySlip(employeeId, date);
        }

        [Route("")]
        [HttpPost]
        // POST: api/v1/Employees
        public Employee Post([FromBody]Employee employee)
        {
          return   _employeeService.SaveEmployee(employee);
        }

        [Route("{id}")]
        [HttpPut]
        public Employee Put(int id,[FromBody]Employee value)
        {
           return  _employeeService.SaveEmployee(value);
        }

    }
}
