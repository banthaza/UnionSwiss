using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UnionSwiss.Domain;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Persistence.Model.Map
{
    public class TaxYearMap : EntityTypeConfiguration<TaxYear>
    {
        public TaxYearMap()
        {
            ToTable("TaxYears", "Finance");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(x => x.Name);

            HasMany(x => x.TaxPeriods)
            .WithOptional()
            .HasForeignKey(x => x.TaxYearId);

            HasMany(x => x.TaxBrackets)
             .WithOptional()
             .HasForeignKey(x => x.TaxYearId);
        }
    }
}
