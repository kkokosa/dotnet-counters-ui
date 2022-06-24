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
using DotnetCountersUi.ViewModels;
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
}
