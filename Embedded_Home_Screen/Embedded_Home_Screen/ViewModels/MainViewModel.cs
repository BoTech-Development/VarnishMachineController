using Embedded_Home_Screen.ViewModels.Dialogs;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;

using System.Runtime.InteropServices;

namespace Embedded_Home_Screen.ViewModels;

public class MainViewModel : ViewModelBase
{

    public ReactiveCommand<Unit, Unit> RestartCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShutDownCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowAboutDialogCommand { get; set; }

    public Interaction<AboutDialogWindowViewModel, Unit> aboutDialogHandler;
    public MainViewModel()
    {
        ShutDownCommand = ReactiveCommand.Create(ShutDown);
        RestartCommand = ReactiveCommand.Create(Restart);
        ShowAboutDialogCommand = ReactiveCommand.Create(() =>
        {
            new AboutDialogWindow().Show();
        });
    }
    private void ShowAboutDialog()
    {
        
    }
    private void Restart()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start("ShutDown", "/r");
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("reboot", "");
            }
        }
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
