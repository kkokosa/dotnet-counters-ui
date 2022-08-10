using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCountersUi.Counters;

[Counter("Monitor Lock Contention Count")]
public class MonitorLockContentionCounter : EventCountersBaseCounter
{
    public MonitorLockContentionCounter(IDataRouter? router)
        : base(router, "monitor-lock-contention-count", "Monitor Lock Contention Count", "Increment")
    {
    }
}

[Counter("ThreadPool Thread Count")]
public class ThreadPoolThreadCountCounter : EventCountersBaseCounter
{
    public ThreadPoolThreadCountCounter(IDataRouter? router)
        : base(router, "threadpool-thread-count", "ThreadPool Thread Count", "Mean")
    {
    }
}

[Counter("ThreadPool Queue Length")]
public class ThreadPoolQueueLengthCounter : EventCountersBaseCounter
{
    public ThreadPoolQueueLengthCounter(IDataRouter? router)
        : base(router, "threadpool-queue-length", "ThreadPool Queue Length", "Mean")
    {
    }
}

[Counter("ThreadPool Completed Items Count")]
public class ThreadPoolCompletedItemsCounter : EventCountersBaseCounter
{
    public ThreadPoolCompletedItemsCounter(IDataRouter? router)
        : base(router, "threadpool-completed-items-count", "ThreadPool Completed Items Count", "Increment")
    {
    }
}

[Counter("Active Timers Count")]
public class ActiveTimersCounter : EventCountersBaseCounter
{
    public ActiveTimersCounter(IDataRouter? router)
        : base(router, "active-timer-count", "Active Timers Count", "Mean")
    {
    }
}