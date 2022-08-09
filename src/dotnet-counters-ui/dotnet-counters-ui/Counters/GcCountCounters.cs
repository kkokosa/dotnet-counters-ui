using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("Gen 0 GCs")]
public class Gen0GcCounterCounter : EventCountersBaseCounter
{
    public Gen0GcCounterCounter(IDataRouter? router)
        : base(router, "gen-0-gc-count", "Gen 0 GCs", "Increment")
    {
    }
}

[Counter("Gen 1 GCs")]
public class Gen1GcCounterCounter : EventCountersBaseCounter
{
    public Gen1GcCounterCounter(IDataRouter? router)
        : base(router, "gen-1-gc-count", "Gen 1 GCs", "Increment")
    {
    }
}

[Counter("Gen 2 GCs")]
public class Gen2GcCounterCounter : EventCountersBaseCounter
{
    public Gen2GcCounterCounter(IDataRouter? router)
        : base(router, "gen-2-gc-count", "Gen 2 GCs", "Increment")
    {
    }
}