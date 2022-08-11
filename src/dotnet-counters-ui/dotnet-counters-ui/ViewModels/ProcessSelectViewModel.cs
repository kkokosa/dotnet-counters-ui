using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Microsoft.Diagnostics.NETCore.Client;
using ReactiveUI;

namespace DotnetCountersUi.ViewModels;

public class ProcessSelectViewModel : ReactiveObject
{
    public ReactiveCommand<Window, Unit> CloseDialog { get; }
    
    public CountersProcessViewModel? Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }

    private CountersProcessViewModel? _selected;

    public IEnumerable<CountersProcessViewModel> ProcessItems =>
        DiagnosticsClient.GetPublishedProcesses()
            .Select(GetProcessIfRunning)
            .Where(p => p != null)
            .Select(p => new CountersProcessViewModel(p!));

    public ProcessSelectViewModel()
    {
        CloseDialog = ReactiveCommand.Create<Window>(window => window.Close(Selected));
    }

    private static Process? GetProcessIfRunning(int processId)
    {
        try
        {
            return Process.GetProcessById(processId);
        }
        catch (ArgumentException e) when (e.Message.Contains("is not running"))
        {
            return null;
        }
    }
}