using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Runtime.InteropServices;

namespace Embedded_Home_Screen.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public ReactiveCommand<Unit, Unit> RestartCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShutDownCommand { get; set; }
    public MainViewModel()
    {
        ShutDownCommand = ReactiveCommand.Create(ShutDown);
    }
    private void ShutDown()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start("ShutDown", "/s");
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("shutdown", "now");
            }
        }
    }
}
