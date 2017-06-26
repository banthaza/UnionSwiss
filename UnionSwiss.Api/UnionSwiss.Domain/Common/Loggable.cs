using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionSwiss.Domain.Common
{
    public class Logable 
    {
        public Logable()
        {
            Log = LogManager.GetLogger(GetType());
        }

        public ILog Log { get; private set; }
    }
}
