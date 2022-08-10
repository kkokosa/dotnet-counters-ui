using System;
using System.Reactive.Linq;
using DotnetCountersUi.Extensions;
using Splat;

namespace DotnetCountersUi.Counters;

[Counter("Working set")]
public class WorkingSetCounter : EventCountersBaseCounter
{
    public WorkingSetCounter(IDataRouter? router)
        : base(router, "working-set", "Working set", "Mean")
    {
    }
}