using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DotnetCountersUi.ViewModels;

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

    private void CounterItem_OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
        ((AddCounterViewModel) DataContext!).CloseDialog.Execute(this).Subscribe();
    }
}