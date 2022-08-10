using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("Exceptions Count")]
public class ExceptionsCounter : EventCountersBaseCounter
{
    public ExceptionsCounter(IDataRouter? router)
        : base(router, "exception-count", "Exceptions Count", "Increment")
    {
    }
}
