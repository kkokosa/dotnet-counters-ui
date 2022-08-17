using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotnetCountersUi.Views;

public partial class CounterGraph : UserControl
{
    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    
    public static readonly StyledProperty<object> HeaderProperty = 
        AvaloniaProperty.Register<CounterGraph, object>(nameof(Header));

    public CounterGraph()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}