using Avalonia.Data.Core;
using DialogHostAvalonia;
using Embedded_Home_Screen.Controller;
using Embedded_Home_Screen.ViewModels.Dialogs;
using Embedded_Home_Screen.ViewModels.Dialogs.Update;
using Embedded_Home_Screen.Views;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;

using System.Runtime.InteropServices;

namespace Embedded_Home_Screen.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _status = "Init...";
    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
    private int _precentage = 0;
    public int Percentage
    {
        get => _precentage;
        set => this.RaiseAndSetIfChanged(ref _precentage, value);
    }
    private bool _isIndeterminate = true;
    public bool IsIndeterminate
    {
        get => _isIndeterminate;
        set => this.RaiseAndSetIfChanged(ref _isIndeterminate, value);
    }

    // Top Flyout Left:
    private string _updateButtonFlyoutTopLeft = "Check For Updates";
    public string UpdateButtonFlyoutTopLeft
    {
        get => _updateButtonFlyoutTopLeft;
        set => this.RaiseAndSetIfChanged(ref _updateButtonFlyoutTopLeft, value);
    }

    public ReactiveCommand<Unit, Unit> RestartCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShutDownCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowAboutDialogCommand { get; set; }
    public ReactiveCommand<Unit, Unit> CheckForUpdatesCommand { get; set; }

    public ReactiveCommand<Unit, Unit> UpdateTopLeftButtonCommand { get; set; }

    public Interaction<AboutDialogWindowViewModel, Unit> aboutDialogHandler;

    public UpdateController UpdateController { get; }
    private MainView mainView;

    private bool canRestartToUpdate = false;

    public MainViewModel()
    {
        
    }
    public MainViewModel(MainView mainView)
    {
        ShutDownCommand = ReactiveCommand.Create(ShutDown);
        RestartCommand = ReactiveCommand.Create(Restart);
        ShowAboutDialogCommand = ReactiveCommand.Create(() =>
        {
            new AboutDialogWindow().Show();
        });
        CheckForUpdatesCommand = ReactiveCommand.Create(CheckForUpdates);

        UpdateTopLeftButtonCommand = ReactiveCommand.Create(() =>
        {
            if(canRestartToUpdate) Restart();
        });

        UpdateController = new UpdateController(this);
        this.mainView = mainView;
    }

    private async void CheckForUpdates()
    {
        if(UpdateController.CheckForUpdates(true)) 
        {
            NewUpdateAvailableDialogViewModel dialogViewModel = new NewUpdateAvailableDialogViewModel()
            {
                CurrentVersion = UpdateController.ThisVersion.ToString(),
                NextVersion = UpdateController.NextVersion.ToString(),
                LatestVersion = UpdateController.LatestVersion.ToString(),
                ReleaseNotes = UpdateController.NextVersion.RelatedRelease.Body.ToString(),
                PublishedAt = UpdateController.NextVersion.RelatedRelease.PublishedAt.ToString()
            };
            mainView.ShowMsgBox_NewUpdateAvailable(dialogViewModel);
            UpdateButtonFlyoutTopLeft = "Restart to Aply Update";
        }
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
