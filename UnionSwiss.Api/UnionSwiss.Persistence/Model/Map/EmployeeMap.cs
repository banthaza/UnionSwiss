using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UnionSwiss.Domain;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Persistence.Model.Map
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            ToTable("Employees", "HR");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //these two lines are how we deal with the date funky mentioned earlier
            Ignore(x => x.StartDate);
            Property(x => x.MicrosoftStartDate).HasColumnName("StartDate");

            HasMany(x => x.Payslips)
                .WithOptional()
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
