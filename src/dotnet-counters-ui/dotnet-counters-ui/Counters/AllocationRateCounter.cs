using System;
using System.Reactive.Linq;
using DotnetCountersUi.Extensions;
using Splat;

namespace DotnetCountersUi.Counters;

[Counter("Allocation rate")]
public class AllocationRateCounter : ICounter
{
    public IObservable<double> Data { get; }

    public AllocationRateCounter(IDataRouter? router)
    {
        router ??= Locator.Current.GetRequiredService<IDataRouter>();

        Data = Observable.Create<double>(observer =>
        {
            router.Register("alloc-rate", observer.OnNext);

            return () => router.Unregister("alloc-rate");
        });
    }
}