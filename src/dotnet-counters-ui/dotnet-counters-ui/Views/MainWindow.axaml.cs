using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DotnetCountersUi.ViewModels;

namespace DotnetCountersUi.Views
{
    /*
     * https://github.com/AvaloniaCommunity/awesome-avalonia
     *
     * TODO:
     * - XAML component to wrap AvaPlot but accepting "Series" collection with data:
     *     - provider name/counter name
     *     - style
     *     - registers in some central router for updates from specified counters
     * - a central router that listens to diagnostic session and broadcasts updates
     * - it will allow to build also other kind of graphs like histograms 
     */
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        private int pid;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();

#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override async void OnOpened(EventArgs e)
        {
            var dialog = new CountersSelectDialog();
            var result = await dialog.ShowDialog<CountersProcessViewModel>(this);
            pid = result.PID;

            if (!Design.IsDesignMode)
            {
                ViewModel!.AttachRouter(pid);

                ViewModel.AddAndStartGraph("cpu-usage");
                ViewModel.AddAndStartGraph("alloc-rate");
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
