using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
        public Action<string> CloseAction { get; set; }

        private string _selected;

        public IEnumerable<string> ProcessItems
        {
            get => DiagnosticsClient.GetPublishedProcesses().Select(pid => pid.ToString());
        }

        public string Selected
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
}
