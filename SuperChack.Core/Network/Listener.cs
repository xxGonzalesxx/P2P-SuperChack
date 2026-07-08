using System.Net.Sockets;
using System.Text;

namespace SuperChack.Core.Network;

public class Listener
{
    private const int Port = 5000;
    private TcpListener _listener = TcpListener.Create(Port);

    public event Action<string>? MessageReceived;
    public event Action? ClientConnected;

    public async Task StartAsync(CancellationToken ct = default)
    {
        _listener.Start();

        while (!ct.IsCancellationRequested)
        {
            TcpClient tcpClient = await _listener.AcceptTcpClientAsync(ct);
            ClientConnected?.Invoke();
            _ = HandleClientAsync(tcpClient, ct);
        }
    }

    private async Task HandleClientAsync(TcpClient tcpClient, CancellationToken ct)
    {
        using NetworkStream stream = tcpClient.GetStream();
        var buffer = new byte[1024];

        while (!ct.IsCancellationRequested)
        {
            int bytesRead = await stream.ReadAsync(buffer, ct);
            if (bytesRead == 0) break;
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            MessageReceived?.Invoke(message);
        }
    }
}