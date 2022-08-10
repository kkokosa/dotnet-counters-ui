using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("IL Bytes Jitted")]
public class IlBytesJittedCounter : EventCountersBaseCounter
{
    public IlBytesJittedCounter(IDataRouter? router)
        : base(router, "il-bytes-jitted", "IL Bytes Jitted", "Mean")
    {
    }
}

[Counter("Methods Jitted Count")]
public class MethodsJittedCounter : EventCountersBaseCounter
{
    public MethodsJittedCounter(IDataRouter? router)
        : base(router, "methods-jitted-count", "Methods Jitted Count", "Mean")
    {
    }
}

[Counter("Time In JIT (%)")]
public class TimeInJitCounter : EventCountersBaseCounter
{
    public TimeInJitCounter(IDataRouter? router)
        : base(router, "time-in-jit", "Time In JIT (%)", "Increment")
    {
    }
}
