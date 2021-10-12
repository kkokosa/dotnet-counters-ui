using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using ScottPlot;
using ScottPlot.Avalonia;

namespace DotnetCountersUi
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
        double[] liveData1 = new double[400];
        double[] liveData2 = new double[400];
        private AvaPlot avaPlot1;

        private DispatcherTimer _renderTimer;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
        }

        protected override async void OnOpened(EventArgs e)
        {
            var dialog = new CountersSelectDialog();
            await dialog.ShowDialog(this);

            avaPlot1 = this.Find<AvaPlot>("AvaPlot1");

            var s1 = avaPlot1.Plot.AddSignal(liveData1, 1D, Color.BlueViolet, "Gen0");

            avaPlot1.Plot.AddSignal(liveData2, 1D, Color.DarkOrange, "Gen0");

            if (!Design.IsDesignMode)
            {
                var thread = new Thread(CollectRoutine);
                thread.Start();

                _renderTimer = new DispatcherTimer();
                _renderTimer.Interval = TimeSpan.FromMilliseconds(200);
                _renderTimer.Tick += Render;
                _renderTimer.Start();
            }

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CollectRoutine()
        {
            var providers = new List<EventPipeProvider>()
            {
                new EventPipeProvider("System.Runtime",
                    EventLevel.Informational,
                    (long)ClrTraceEventParser.Keywords.None,
                    new Dictionary<string, string>(){
                        { "EventCounterIntervalSec", "1" }
                    })
            };
            
            var client = new DiagnosticsClient(39104);
            using (var session = client.StartEventPipeSession(providers, false))
            {
                var source = new EventPipeEventSource(session.EventStream);
                source.Dynamic.All += DynamicOnAll;
                source.Process();
            }
        }

        private void DynamicOnAll(TraceEvent obj)
        {
            if (obj.EventName.Equals("EventCounters"))
            {
                IDictionary<string, object> payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
                IDictionary<string, object> payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);
                if (payloadFields["Name"].ToString().Equals("alloc-rate"))
                {
                    double allocRate = Double.Parse(payloadFields["Increment"].ToString());
                    Array.Copy(liveData1, 1, liveData1, 0, liveData1.Length - 1);
                    liveData1[liveData1.Length - 1] = allocRate;
                }
            }
        }

        void Render(object sender, EventArgs e)
        {
            avaPlot1.Plot.AxisAuto();
            avaPlot1.Refresh();
        }
    }
}
