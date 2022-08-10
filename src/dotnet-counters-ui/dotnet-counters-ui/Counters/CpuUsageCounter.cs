using System;
using System.Reactive.Linq;
using DotnetCountersUi.Extensions;
using Splat;

namespace DotnetCountersUi.Counters;

[Counter("CPU Usage (%)")]
public class CpuUsageCounter : EventCountersBaseCounter
{
    public CpuUsageCounter(IDataRouter? router)
        : base(router, "cpu-usage", "CPU Usage (%)", "Mean")
    {
    }
}