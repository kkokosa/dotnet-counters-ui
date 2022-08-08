using System;

namespace DotnetCountersUi.Counters;

public interface ICounter
{
    IObservable<double> Data { get; }
}