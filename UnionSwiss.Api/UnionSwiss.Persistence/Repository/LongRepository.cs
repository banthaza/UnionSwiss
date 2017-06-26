using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnionSwiss.Persistence.Context;
using UnionSwiss.Persistence.Factory;

namespace UnionSwiss.Persistence.Repository
{
    public class LongRepositroy: BaseRepository<IUnionSwissContext, long>
    {
        public LongRepositroy(IDbContextFactory<IUnionSwissContext> dbContextFactory) : base(dbContextFactory)
        {


        }
    }
}
