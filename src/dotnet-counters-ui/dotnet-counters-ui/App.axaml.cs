using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DotnetCountersUi.Views;
using Splat;

namespace DotnetCountersUi
{
    public class App : Application
    {
        public override void Initialize()
        {
            // https://github.com/codingseb/Avalonia.EventSetter#getting-started
            GC.KeepAlive(typeof(Avalonia.Styling.EventSetter).Assembly);
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Locator.CurrentMutable.RegisterLazySingleton<IDataRouter>(() => new DataRouter());
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
