using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCountersUi.Extensions;
using Splat;

namespace DotnetCountersUi.Counters
{
    public abstract class EventCountersBaseCounter : ICounter
    {
        public IObservable<double> Data { get; }

        public EventCountersBaseCounter(IDataRouter? router, string counterName, string displayName, string counterDataType)
        {
            router ??= Locator.Current.GetRequiredService<IDataRouter>();
            Data = Observable.Create<double>(observer =>
            {
                router.Register(counterName, displayName, counterDataType, observer.OnNext);

                return () => router.Unregister(counterName, observer.OnNext);
            });
        }
    }
}
