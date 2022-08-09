using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("Assembly Count")]
public class AssemblyCounter : EventCountersBaseCounter
{
    public AssemblyCounter(IDataRouter? router)
        : base(router, "assembly-count", "Assembly Count", "Mean")
    {
    }
}