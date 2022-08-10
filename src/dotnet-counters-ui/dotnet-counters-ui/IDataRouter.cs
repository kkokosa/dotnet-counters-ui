using System;

namespace DotnetCountersUi;

public interface IDataRouter
{
  void Start(int pid);
  GraphData Register(string graphId, string displayName, string counterDataType, Action<double> action);
  void Unregister(string graphId, Action<double> action);
}