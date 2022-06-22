using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using Microsoft.Diagnostics.NETCore.Client;

namespace DotnetCountersUi
{
    public partial class CountersSelectDialog : Window
    {
        public CountersSelectDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new CountersSelectViewModel()
            {
                CloseAction = str => Close(str)
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    public class CountersSelectViewModel : INotifyPropertyChanged
    {
        public Action<CountersProcessViewModel> CloseAction { get; set; }

        private CountersProcessViewModel _selected;

        public ObservableCollection<CountersProcessViewModel> ProcessItems
        {
            get => new(DiagnosticsClient.GetPublishedProcesses().Select(pid => new CountersProcessViewModel(pid)));
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
    }

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
