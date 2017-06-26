using UnionSwiss.Persistence.Factory;
using UnionSwiss.Persistence.Repository;

namespace UnionSwiss.PersistenceTests
{
    public class TestBaseRepository : BaseRepository<TestDbContext, long>
    {
        public TestBaseRepository(IDbContextFactory<TestDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

   
    }
}