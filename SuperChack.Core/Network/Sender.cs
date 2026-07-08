using System.Net.Sockets;
using System.Text;

namespace SuperChack.Core.Network;

public class Sender
{
    private const int Port = 5000;
    private TcpClient? _tcpClient;
    private NetworkStream? _stream;

    public async Task ConnectAsync(string ip, CancellationToken ct = default)
    {
        _tcpClient = new TcpClient();
        await _tcpClient.ConnectAsync(ip, Port, ct);
        _stream = _tcpClient.GetStream();
    }

    public async Task SendAsync(string message, CancellationToken ct = default)
    {
        if (_stream is null)
            throw new InvalidOperationException("Нет соединения. Сначала вызови ConnectAsync.");

        var data = Encoding.UTF8.GetBytes(message);
        await _stream.WriteAsync(data, ct);
    }

    public void Disconnect()
    {
        _stream?.Dispose();
        _tcpClient?.Dispose();
    }
}