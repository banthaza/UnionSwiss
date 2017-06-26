using System;
using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.Domain.Model.Entity
{
    public class TaxBracket:IEntity<long>
    {
        public long Id { get; set; }
        public long TaxYearId { get; set; }
        public int MinQualifyingValue { get; set; }
        public int MaxQualifyingValue { get; set; }
  
        public int BaseTaxValue { get; set; }
        public Decimal IncrementMultiplier { get; set; }


    }
}