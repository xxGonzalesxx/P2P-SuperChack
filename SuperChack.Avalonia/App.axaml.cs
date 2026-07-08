using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SuperChack.Avalonia.ViewModels;
using SuperChack.Avalonia.Views;
using SuperChack.Core.Network;
using SuperChack.Core.Storage;

namespace SuperChack.Avalonia;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>();
        services.AddScoped<ChatRepository>();
        services.AddSingleton<Sender>();
        services.AddSingleton<Listener>();
        services.AddTransient<ConnectViewModel>();
        services.AddTransient<ChatViewModel>();
        services.AddSingleton<MainWindowViewModel>();

        Services = services.BuildServiceProvider();

        await Services.GetRequiredService<ChatRepository>().InitializeAsync();

        var listener = Services.GetRequiredService<Listener>();
        _ = listener.StartAsync();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainWindowViewModel>()
            };
        }

        Log.Information("Приложение запущено");
        base.OnFrameworkInitializationCompleted();
    }
}