using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UnionSwiss.Domain;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Persistence.Model.Map
{
    public class TaxPeriodMap : EntityTypeConfiguration<TaxPeriod>
    {
        public TaxPeriodMap()
        {
            ToTable("TaxPeriods", "Finance");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(x => x.Name);


        }
    }
}
