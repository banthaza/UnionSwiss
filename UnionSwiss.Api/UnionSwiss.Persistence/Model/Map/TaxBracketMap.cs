using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Domain.Model.Entity;

namespace UnionSwiss.Persistence.Model.Map
{
    public class TaxBracketMap : EntityTypeConfiguration<TaxBracket>
    {
        public TaxBracketMap()
        {
            ToTable("TaxBrackets", "Finance");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


        }
    }
}
