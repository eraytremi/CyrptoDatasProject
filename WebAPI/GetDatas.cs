using System.Net.WebSockets;
using System.Text;

namespace WebAPI
{
    public static class GetDatas
    {
        public static async Task ListenToBinanceWebSocket(WebSocket wSocket)
        {
            var binanceWebSocketUrl = new Uri("wss://stream.binance.com:9443/ws/!ticker@arr");
            using (var client = new ClientWebSocket())
            {
                await client.ConnectAsync(binanceWebSocketUrl, CancellationToken.None);

                var buffer = new byte[1024 * 4];

                while (wSocket.State == WebSocketState.Open)
                {
                    var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await wSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    }
                    else if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var receivedData = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        await wSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(receivedData)), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }
    
}
}
