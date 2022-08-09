using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using DotnetCountersUi.Extensions;
using Splat;

namespace DotnetCountersUi.Counters;

[Counter("Allocation rate")]
public class AllocationRateCounter : EventCountersBaseCounter
{
    public AllocationRateCounter(IDataRouter? router)
        : base(router, "alloc-rate", "Allocation rate", "Increment")
    {
    }
}