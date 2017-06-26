using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnionSwiss.Api.Controllers.Api.Filters;
using UnionSwiss.Domain.Common;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Domain.Service;

namespace UnionSwiss.Api.Controllers.Api.v1
{
    [ApiExceptionFilter]
    [RoutePrefix("api/v1/taxYear")]
    public class TaxYearController : ApiController
    {

        private readonly IFinanceAdminService _financeAdminService;

        public TaxYearController(IFinanceAdminService financeAdminService)
        {
            Guard.ArgumentNotNull(financeAdminService, nameof(financeAdminService));
            _financeAdminService = financeAdminService;
        }

        [Route()]
        [HttpGet]
        public IEnumerable<TaxYear> Get()
        {
            return _financeAdminService.ListTaxYears();
        }


        [Route("{id}")]
        [HttpGet]
        public TaxYear Get(long id)
        {
            return _financeAdminService.GetTaxYear(id);
        }

        [HttpGet]
        [Route("")]
        public TaxYear Generate(DateTime date)
        {
            return _financeAdminService.GenerateTaxYear(date);
        }

        [Route("")]
        [HttpPost]
        public void Post([FromBody]TaxYear taxYear)
        {
            _financeAdminService.SaveTaxYear(taxYear);
        }

        [Route("{id}")]
        [HttpPut]
        public void Put(long id, [FromBody]TaxYear taxYear)
        {
            _financeAdminService.SaveTaxYear(taxYear);
        }

        // DELETE: api/FinancialAdmin/5
        public void Delete(long id)
        {
        }
    }
}
