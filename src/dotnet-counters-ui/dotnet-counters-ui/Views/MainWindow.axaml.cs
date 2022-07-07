using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DotnetCountersUi.Extensions;
using DotnetCountersUi.ViewModels;
using Splat;

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
        
        private readonly IDataRouter _router; // TODO: dependency injection

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();

#if DEBUG
            this.AttachDevTools();
#endif
            _router = Locator.Current.GetRequiredService<IDataRouter>();
        }

        protected override async void OnOpened(EventArgs e)
        {
            var dialog = new CountersSelectDialog();
            var result = await dialog.ShowDialog<CountersProcessViewModel>(this);
            pid = result.PID;

            if (!Design.IsDesignMode)
            {
                _router.Start(pid);
                
                ViewModel!.AddAndStartGraph("alloc-rate");
                ViewModel!.AddAndStartGraph("cpu-usage");
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
