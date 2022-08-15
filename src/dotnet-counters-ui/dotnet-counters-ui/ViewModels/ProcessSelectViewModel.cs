using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DotnetCountersUi.Extensions;
using DotnetCountersUi.Native;
using Microsoft.Diagnostics.NETCore.Client;
using ReactiveUI;
using Splat;

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

    public IEnumerable<CountersProcessViewModel> ProcessItems { get; }

    public ProcessSelectViewModel(ICommandLineArgsProvider? argsProvider = null)
    {
        argsProvider ??= Locator.Current.GetRequiredService<ICommandLineArgsProvider>();

        ProcessItems =
        DiagnosticsClient.GetPublishedProcesses()
            .Select(GetProcessIfRunning)
            .Where(p => p != null)
            .Select(p =>
            {
                argsProvider.TryGetCommandLineArgs(p!.Id, out var args);

                return new CountersProcessViewModel(p, args);
            });

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