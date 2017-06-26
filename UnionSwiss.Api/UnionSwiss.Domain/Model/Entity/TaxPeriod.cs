using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.Domain.Model.Entity
{
    public class TaxPeriod : IEntity<long>
    {
        public long Id { get; set; }
        public long TaxYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string Name
        {
            get { return $"{StartDate.ToString("dd-MMM")} to {EndDate.ToString("dd MMM")}"; }
        }

    }
 
}
