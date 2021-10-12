using System.Collections;
using System.Collections.Generic;
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
            DataContext = new CountersSelectViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void RunTheThing()
        {
        }

        bool CanRunTheThing()
        {
            return true;
        }
    }

    public class CountersSelectViewModel
    {
        public IEnumerable<string> ProcessItems
        {
            get => DiagnosticsClient.GetPublishedProcesses().Select(pid => pid.ToString());
        }
    }
}
