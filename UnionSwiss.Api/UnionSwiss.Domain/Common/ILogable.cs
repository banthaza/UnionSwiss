using log4net;

namespace UnionSwiss.Domain.Common
{
   public interface ILogable
    {
         ILog Log { get; }
    }
}
