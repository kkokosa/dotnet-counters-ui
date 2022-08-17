using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DotnetCountersUi.ViewModels;

namespace DotnetCountersUi.Views
{
    public partial class ProcessSelectDialog : Window
    {
        public ProcessSelectDialog()
        {
            InitializeComponent();

            DataContext = new ProcessSelectViewModel();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void ProcessItem_OnDoubleTapped(object? sender, RoutedEventArgs e)
        {
            ((ProcessSelectViewModel) DataContext!).CloseDialog.Execute(this).Subscribe();
        }

        private void TopLevel_OnOpened(object? sender, EventArgs e)
        {
            ((ProcessSelectViewModel) DataContext!).LoadProcesses.Execute().Subscribe();
        }
    }
}
