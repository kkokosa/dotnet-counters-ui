using System;
using Splat;

namespace DotnetCountersUi.Extensions;

public static class SplatExtensions
{
    public static T GetRequiredService<T>(this IReadonlyDependencyResolver resolver)
    {
        return resolver.GetService<T>()
               ?? throw new NullReferenceException($"Service of type {typeof(T)} was not registered");
    }
}