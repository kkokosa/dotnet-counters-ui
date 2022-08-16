using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DotnetCountersUi.Native;
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

            Locator.CurrentMutable.RegisterLazySingleton<ICommandLineArgsProvider>(() =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return new LinuxCommandLineArgsProvider();
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return new MacOsCommandLineArgsProvider();
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return new WindowsCommandLineArgsProvider();
                }

                return new DummyCommandLineArgsProvider();
            });

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
