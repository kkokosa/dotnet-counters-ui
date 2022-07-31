using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotnetCountersUi.Views;

public partial class AddCounterDialog : Window
{
    public AddCounterDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}