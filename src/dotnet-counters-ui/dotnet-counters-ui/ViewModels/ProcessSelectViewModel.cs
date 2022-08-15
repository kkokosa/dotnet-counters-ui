using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
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

    private readonly ObservableAsPropertyHelper<IEnumerable<CountersProcessViewModel>> _processItems;
    public IEnumerable<CountersProcessViewModel> ProcessItems => _processItems.Value;

    public ReactiveCommand<Unit, IEnumerable<CountersProcessViewModel>> LoadProcesses { get; }

    private readonly ICommandLineArgsProvider _argsProvider;

    public ProcessSelectViewModel(ICommandLineArgsProvider? argsProvider = null)
    {
        _argsProvider = argsProvider ?? Locator.Current.GetRequiredService<ICommandLineArgsProvider>();

        LoadProcesses = ReactiveCommand.CreateFromTask<Unit, IEnumerable<CountersProcessViewModel>>(
        async _ => await GetProcesses());

        LoadProcesses.ToProperty(this, vm => vm.ProcessItems, out _processItems);

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

    private async Task<List<CountersProcessViewModel>> GetProcesses()
    {
        var processes = DiagnosticsClient.GetPublishedProcesses()
            .Select(GetProcessIfRunning)
            .Where(p => p != null)
            .ToArray();

        var output = new List<CountersProcessViewModel>(processes.Length);

        foreach (var process in processes)
        {
            var args = await _argsProvider.GetCommandLineArgs(process!.Id);
            
            output.Add(new CountersProcessViewModel(process, args));
        }

        return output;
    }
}