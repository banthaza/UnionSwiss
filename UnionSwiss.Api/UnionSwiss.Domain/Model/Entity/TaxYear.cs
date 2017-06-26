using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.Domain.Model.Entity
{
    public class TaxYear : IEntity<long>
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public  List<TaxPeriod> TaxPeriods { get; set; }
        public List<TaxBracket> TaxBrackets { get; set; }

        public string Name
        {
            get { return $"{StartDate.ToString("MMM-yyyy")} to {EndDate.ToString("MMM-yyyy")}"; }
        }

        public TaxYear()
        {
            TaxPeriods = new List<TaxPeriod>();
            TaxBrackets = new List<TaxBracket>();
        }

    }
 
}
