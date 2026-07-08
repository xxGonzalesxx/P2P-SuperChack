using SuperChack.Core.Models;
using SuperChack.Core.Network;
using SuperChack.Core.Storage;

namespace SuperChack.ConsoleApp.UI;

public class UserInterface
{
    private readonly Sender _sender;
    private readonly ChatRepository _chatRepository;

    public UserInterface(Sender sender, ChatRepository chatRepository)
    {
        _sender = sender;
        _chatRepository = chatRepository;
    }

    public void ShowWelcome()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Title = "SuperChack P2P Мессенджер";
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║   SuperChack P2P Мессенджер  ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.WriteLine();
    }

    public async Task<string?> SetupAsync(CancellationToken ct = default)
    {
        Console.Write("Введи своё имя: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) name = "Аноним";

        Console.Write("Введи IP друга (Radmin VPN): ");
        string? ip = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ip))
        {
            Console.WriteLine("IP не указан, выход.");
            return null;
        }

        try
        {
            await _sender.ConnectAsync(ip, ct);
            Console.WriteLine($"Подключились к {ip}");
            Console.WriteLine();
            Console.WriteLine("Можно писать! Для выхода напиши 'exit'.");
            Console.WriteLine("──────────────────────────────────");
            return name;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось подключиться: {ex.Message}");
            return null;
        }
    }

    public async Task RunChatLoopAsync(string myName, CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            Console.Write($"{myName}: ");
            string? text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text)) continue;
            if (text.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

            try
            {
                await _sender.SendAsync(text, ct);
                await _chatRepository.SaveAsync(new Message 
                { 
                    Sender = myName, 
                    Text = text, 
                    SentAt = DateTime.Now 
                }, ct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
    
    public async Task ShowHistoryAsync(CancellationToken ct = default)
    {
        var messages = await _chatRepository.LoadAsync(ct);
    
        if (messages.Count == 0)
        {
            System.Console.WriteLine("История пуста.");
            return;
        }

        System.Console.WriteLine("── История сообщений ──────────────");
        foreach (var m in messages)
            System.Console.WriteLine($"{m.SentAt:HH:mm:ss} {m.Sender}: {m.Text}");
        System.Console.WriteLine("───────────────────────────────────");
        System.Console.WriteLine();
    }

    public void ShowIncomingMessage(string message) =>
        Console.WriteLine($"\n[Входящее]: {message}");

    public void ShowGoodbye() =>
        Console.WriteLine("Пока!");
}