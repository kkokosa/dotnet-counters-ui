using System;
using OxyPlot;

namespace DotnetCountersUi.Utils;

public static class OxyColorUtils
{
    public static unsafe OxyColor GetRandomOxyColor()
    {
        Span<byte> colors = stackalloc byte[3];
        
        Random.Shared.NextBytes(colors);

        return OxyColor.FromRgb(colors[0], colors[1], colors[2]);
    }
}