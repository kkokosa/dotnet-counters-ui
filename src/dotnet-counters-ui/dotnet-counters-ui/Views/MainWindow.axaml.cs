using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
    public partial class MainWindow : Window
    {
        private int pid;
       
        //private AvaPlot avaPlot1;
        private Thread _collectRoutine;
        private IDataRouter _router; // TODO: dependency injection

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _router = Locator.Current.GetService<IDataRouter>();
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    _renderTimer.Stop();
        //}

        protected override async void OnOpened(EventArgs e)
        {
            var dialog = new CountersSelectDialog();
            var result = await dialog.ShowDialog<CountersProcessViewModel>(this);
            pid = result.PID;

            //avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
            //var s1 = avaPlot1.Plot.AddSignal(liveData1, 1D, Color.BlueViolet, "Gen0");
            //avaPlot1.Plot.AddSignal(liveData2, 1D, Color.DarkOrange, "Gen0");

            if (!Design.IsDesignMode)
            {
                //_collectRoutine = new Thread(CollectRoutine);
                //_collectRoutine.IsBackground = true;
                //_collectRoutine.Start();
                _router.Start(pid);
            }

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
