using System.Threading;
using OxyPlot;

namespace DotnetCountersUi.Utils;

public static class OxyColorUtils
{
    // Credits http://tsitsul.in/blog/coloropt/
    private static readonly OxyColor[] Colors = {
        OxyColor.FromRgb(235, 172, 35),
        OxyColor.FromRgb(184, 0, 88),
        OxyColor.FromRgb(0, 140, 249),
        OxyColor.FromRgb(0, 110, 0),
        OxyColor.FromRgb(0, 187, 173),
        OxyColor.FromRgb(209, 99, 230),
        OxyColor.FromRgb(178, 69, 2),
        OxyColor.FromRgb(255, 146, 135),
        OxyColor.FromRgb(89, 84, 214),
        OxyColor.FromRgb(0, 198, 248),
        OxyColor.FromRgb(135, 133, 0),
        OxyColor.FromRgb(0, 167, 108),
        OxyColor.FromRgb(189, 189, 189)
    };

    private static int _index = -1;
    
    public static OxyColor GetNextQualitativeColor() 
        => Colors[Interlocked.Increment(ref _index) % Colors.Length];
}