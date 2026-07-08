using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace SuperChack.Avalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    public MainWindowViewModel()
    {
        var connectVm = App.Services.GetRequiredService<ConnectViewModel>();
        connectVm.Connected += OnConnected;
        _currentView = connectVm;
    }

    private void OnConnected(string myName, string peerIp)
    {
        var chatVm = App.Services.GetRequiredService<ChatViewModel>();
        chatVm.Initialize(myName, peerIp);
        CurrentView = chatVm;
    }
}