using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UnionSwiss.Domain;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Persistence.Model.Map
{
    public class EmployeePayslipMap : EntityTypeConfiguration<EmployeePayslip>
    {
        public EmployeePayslipMap()
        {
            ToTable("EmployeePayslips", "HR");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
      

        }
    }
}
