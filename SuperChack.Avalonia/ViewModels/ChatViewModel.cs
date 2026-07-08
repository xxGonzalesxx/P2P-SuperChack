using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using SuperChack.Core.Models;
using SuperChack.Core.Network;
using SuperChack.Core.Storage;

namespace SuperChack.Avalonia.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    private readonly Sender _sender;
    private readonly ChatRepository _repository;
    private string _myName = string.Empty;

    [ObservableProperty] private string _messageText = string.Empty;

    public ObservableCollection<Message> Messages { get; } = new();

    public ChatViewModel(Sender sender, Listener listener, ChatRepository repository)
    {
        _sender = sender;
        _repository = repository;

        listener.MessageReceived += OnMessageReceived;
    }

    public async void Initialize(string myName, string peerIp)
    {
        _myName = myName;
        var history = await _repository.LoadAsync();
        foreach (var m in history)
            Messages.Add(m);
    }

    private void OnMessageReceived(string text)
    {
        Log.Information("Входящее сообщение: {Text}", text);
        var message = new Message { Sender = "Собеседник", Text = text, SentAt = DateTime.Now };
        Messages.Add(message);
        _ = _repository.SaveAsync(message);
    }

    [RelayCommand]
    private async Task SendAsync()
    {
        if (string.IsNullOrWhiteSpace(MessageText)) return;

        var message = new Message { Sender = _myName, Text = MessageText, SentAt = DateTime.Now };
        Log.Information("Отправка сообщения: {Text}", MessageText);
        await _sender.SendAsync(MessageText);
        await _repository.SaveAsync(message);
        Messages.Add(message);
        MessageText = string.Empty;
    }
}