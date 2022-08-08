using System;
using System.Globalization;
using Avalonia.Data.Converters;
using OxyPlot;
using OxyPlot.Avalonia;

namespace DotnetCountersUi.Converters;

public class OxyColorToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value switch
        {
            OxyColor color => color.ToBrush(),
            { } => throw new ArgumentException($"Unsupported argument of type {value.GetType()}.",
                nameof(value)),
            null => throw new ArgumentNullException(nameof(value)),
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}