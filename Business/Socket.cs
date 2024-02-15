using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Uri uri = new Uri("wss://stream.binance.com:9443/ws/!ticker@arr\""); 

        using (ClientWebSocket clientWebSocket = new ClientWebSocket())
        {
            try
            {
                await clientWebSocket.ConnectAsync(uri, CancellationToken.None);

                Console.WriteLine("WebSocket'e bağlandı.");

                while (clientWebSocket.State == WebSocketState.Open)
                {
                    byte[] buffer = new byte[1024];
                    WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Alınan mesaj: {receivedMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }
    }
}
