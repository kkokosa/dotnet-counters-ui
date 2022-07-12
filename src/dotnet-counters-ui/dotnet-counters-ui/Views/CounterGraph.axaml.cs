using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotnetCountersUi.Views;

public partial class CounterGraph : UserControl
{
    public CounterGraph()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}