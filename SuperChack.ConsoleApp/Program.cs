using SuperChack.Core.Network;
using SuperChack.Core.Storage;
using SuperChack.ConsoleApp.UI;

var sender = new Sender();
var dbContext = new AppDbContext();
var chatRepository = new ChatRepository(dbContext);
var ui = new UserInterface(sender, chatRepository);
var listener = new Listener();
var cts = new CancellationTokenSource();

await chatRepository.InitializeAsync();

ui.ShowWelcome();

await ui.ShowHistoryAsync(cts.Token);

listener.MessageReceived += ui.ShowIncomingMessage;
listener.ClientConnected += () => Console.WriteLine("Кто-то подключился...");

_ = listener.StartAsync(cts.Token);

string? myName = await ui.SetupAsync(cts.Token);
if (myName is null) return;

await ui.RunChatLoopAsync(myName, cts.Token);

sender.Disconnect();
cts.Cancel();
ui.ShowGoodbye();