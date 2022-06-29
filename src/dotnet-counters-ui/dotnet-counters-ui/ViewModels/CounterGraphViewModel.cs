using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace DotnetCountersUi.ViewModels
{
  public class CounterGraphViewModel: ReactiveObject
  {
    [Reactive] public IEnumerable<DataPoint> Points { get; set; }

    public string Init
    {
      set =>
        Locator.Current.GetService<IDataRouter>()
          !.Register(value, OnNewData);
    }

    private double[] _points = new double[200];
    
    private readonly IDataRouter? _router = Locator.Current.GetService<IDataRouter>();

    public CounterGraphViewModel()
    {

    }
    
    
    private void OnNewData(double value)
    {
      Array.ConstrainedCopy(_points, 1, _points, 0, _points.Length - 1);
      _points[^1] = value;

      var p = 
        new DataPoint[200]
          .Select((_, i)=>new DataPoint(i/100f, _points[i]));
      Points = p;

    }
  }
}