using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DotnetCountersUi.ViewModels
{
  public class CountersProcessViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler? PropertyChanged;

    public int PID { get; set; }
    public string Name { get; set; }
    public string Arguments { get; set; }

    public CountersProcessViewModel(int pid)
    {
      PID = pid;
      try
      {
        var process = Process.GetProcessById(pid);
        Name = process.ProcessName;
        Arguments = process.StartInfo.Arguments; // TODO: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.startinfo does not support this
      }
      catch (Exception)
      {

      }
    }
  }
}