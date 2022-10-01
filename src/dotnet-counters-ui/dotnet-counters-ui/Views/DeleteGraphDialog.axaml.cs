using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DotnetCountersUi.Views;

public partial class DeleteGraphDialog : Window
{
    public DeleteGraphDialog()
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

    private void YesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void NoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}