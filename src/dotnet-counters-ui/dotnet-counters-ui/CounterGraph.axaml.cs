using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ScottPlot.Avalonia;
using Splat;

namespace DotnetCountersUi
{
    /* MVVM notes: while I'd love to make this graph control MVVM compatible and store the underlying data
       as CounterGraphViewModel, it's not trivial because of https://scottplot.net/faq/mvvm/.
       In desire of good performance, I'm sacrificing MVVM too. Sorry clean coders!
     */
    public partial class CounterGraph : UserControl
    {
        public string GraphId { get; set; }
        private IDataRouter _router;
        private AvaPlot _plot;
        double[] _data = new double[400];
        private DispatcherTimer _renderTimer;

        public CounterGraph()
        {
            InitializeComponent();

            // I hate Service Locator but don't know yet how to inject dependency into control without using
            // ViewModel (which we don't use because of above MVVM notes)
            _router = Locator.Current.GetService<IDataRouter>();

            _renderTimer = new DispatcherTimer();
            _renderTimer.Interval = TimeSpan.FromMilliseconds(500);
            _renderTimer.Tick += Render;
            _renderTimer.Start();
        }

        private void OnNewData(double value)
        {
            Array.Copy(_data, 1, _data, 0, _data.Length - 1);
            _data[_data.Length - 1] = value;
        }

        protected override void OnInitialized()
        {
            _plot = this.Find<AvaPlot>("AvaPlot1");
            _plot.Plot.AddSignal(_data, 1D, Color.DarkOrange, "Gen0");

            _router.Register(GraphId, OnNewData);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void Render(object sender, EventArgs e)
        {
            _plot.Plot.AxisAuto();
            _plot.Refresh();
        }
    }
}
