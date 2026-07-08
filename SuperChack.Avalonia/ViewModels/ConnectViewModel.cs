using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SuperChack.Avalonia.Validators;
using SuperChack.Core.Network;

namespace SuperChack.Avalonia.ViewModels;

public partial class ConnectViewModel : ViewModelBase
{
    private readonly ConnectValidator _validator = new();

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _peerIp = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;

    public event Action<string, string>? Connected;

    [RelayCommand]
    private async Task ConnectAsync()
    {
        var result = _validator.Validate(this);
        if (!result.IsValid)
        {
            ErrorMessage = result.Errors.First().ErrorMessage;
            return;
        }

        try
        {
            var sender = App.Services.GetRequiredService<Sender>();
            await sender.ConnectAsync(PeerIp);
            Connected?.Invoke(Name, PeerIp);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Не удалось подключиться: {ex.Message}";
        }
    }
}