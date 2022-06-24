using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.Diagnostics.NETCore.Client;

namespace DotnetCountersUi.ViewModels
{
  public class CountersSelectViewModel : INotifyPropertyChanged
  {
    public Action<CountersProcessViewModel> CloseAction { get; set; }

    private CountersProcessViewModel _selected;

    public ObservableCollection<CountersProcessViewModel> ProcessItems =>
      new(DiagnosticsClient.GetPublishedProcesses()
        .Where(IsProcessOfPidRunning)
        .Select(p => new CountersProcessViewModel(p)));

    public CountersProcessViewModel Selected
    {
      get => _selected;
      set
      {
        if (_selected != value)
        {
          _selected = value;
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
        }
      }
    }

    public void RunTheThing()
    {
      CloseAction(_selected);
    }

    private static bool IsProcessOfPidRunning(int processId)
    {
      try
      {
        Process.GetProcessById(processId);
        return true;
      }
      catch (ArgumentException e) when (e.Message.Contains("is not running"))
      {
        return false;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }

}