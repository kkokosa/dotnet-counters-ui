using System;

namespace DotnetCountersUi.Counters;

[AttributeUsage(AttributeTargets.Class)]
public class CounterAttribute : Attribute
{
    public string Name { get; }
    
    public CounterAttribute(string name)
    {
        Name = name;
    }
}