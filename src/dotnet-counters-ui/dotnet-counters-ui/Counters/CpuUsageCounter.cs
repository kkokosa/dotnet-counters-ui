using System;
using System.Reactive.Linq;
using DotnetCountersUi.Extensions;
using Splat;

namespace DotnetCountersUi.Counters;

[Counter("CPU Usage (%)")]
public class CpuUsageCounter : ICounter
{
    public IObservable<double> Data { get; }

    public CpuUsageCounter(IDataRouter? router)
    {
        router ??= Locator.Current.GetRequiredService<IDataRouter>();

        Data = Observable.Create<double>(observer =>
        {
            router.Register("cpu-usage", observer.OnNext);

            return () => router.Unregister("cpu-usage");
        });
    }
}