using System;
using System.Globalization;
using Avalonia.Data.Converters;
using OxyPlot;
using OxyPlot.Avalonia;

namespace DotnetCountersUi.Converters;

public class OxyColorToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var oxyColor = (OxyColor) (value ?? throw new ArgumentNullException(nameof(value)));
        return oxyColor.ToBrush();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}