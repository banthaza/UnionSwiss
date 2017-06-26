using UnionSwiss.Persistence.Context;

namespace UnionSwiss.Persistence.Factory
{
    public interface IDbContextFactory<out T> where T : IBaseContext
    {
        T CreateContext();
    }
}