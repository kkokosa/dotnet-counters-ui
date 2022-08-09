using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("Gen 0 size")]
public class Gen0SizeCounter : EventCountersBaseCounter
{
    public Gen0SizeCounter(IDataRouter? router)
        : base(router, "gen-0-size", "Gen 0 size", "Mean")
    {
    }
}

[Counter("Gen 1 Size")]
public class Gen1SizeCounter : EventCountersBaseCounter
{
    public Gen1SizeCounter(IDataRouter? router)
        : base(router, "gen-1-size", "Gen 1 Size", "Mean")
    {
    }
}

[Counter("Gen 2 Size")]
public class Gen2SizeCounter : EventCountersBaseCounter
{
    public Gen2SizeCounter(IDataRouter? router)
        : base(router, "gen-2-size", "Gen 2 Size", "Mean")
    {
    }
}

[Counter("LOH Size")]
public class LohSizeCounter : EventCountersBaseCounter
{
    public LohSizeCounter(IDataRouter? router)
        : base(router, "loh-size", "LOH Size", "Mean")
    {
    }
}

[Counter("POH Size")]
public class PohSizeCounter : EventCountersBaseCounter
{
    public PohSizeCounter(IDataRouter? router)
        : base(router, "poh-size", "POH Size", "Mean")
    {
    }
}