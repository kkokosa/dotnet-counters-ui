using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("GC Heap Size")]
public class GcHeapSizeCounter : EventCountersBaseCounter
{
    public GcHeapSizeCounter(IDataRouter? router)
        : base(router, "gc-heap-size", "GC Heap Size", "Mean")
    {
    }
}

[Counter("GC Committed Size")]
public class GcCommittedCounter : EventCountersBaseCounter
{
    public GcCommittedCounter(IDataRouter? router)
        : base(router, "gc-committed", "GC Committed Size", "Mean")
    {
    }
}

[Counter("GC Fragmentation (%)")]
public class GcFragmentationCounter : EventCountersBaseCounter
{
    public GcFragmentationCounter(IDataRouter? router)
        : base(router, "gc-fragmentation", "GC Fragmentation (%)", "Mean")
    {
    }
}

[Counter("Time in GC (%)")]
public class TimeInGcCounter : EventCountersBaseCounter
{
    public TimeInGcCounter(IDataRouter? router)
        : base(router, "time-in-gc", "Time in GC (%)", "Mean")
    {
    }
}