using Business.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class WebSocketBinance : IWebSocketBinance
    {

        private readonly ClientWebSocket _clientWebSocket;
        public WebSocketBinance(ClientWebSocket clientWebSocket)
        {
            _clientWebSocket = clientWebSocket;
        }
        public async Task CloseAsync()
        {
            await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);

        }

        public async Task ConnectAsync(Uri serverUri)
        {
            await _clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);
        }


        public async Task<T> ReceiveAsync<T>()
        {
            WebSocketReceiveResult result;
            var messageBuffer = new ArraySegment<byte>(new byte[4096]);
            var message = "";
            do
            {
                result = await _clientWebSocket.ReceiveAsync(messageBuffer, CancellationToken.None);
                var messageBytes = messageBuffer.Skip(messageBuffer.Offset).Take(result.Count).ToArray();
                message += Encoding.UTF8.GetString(messageBytes);
            }
            while (!result.EndOfMessage);
            return JsonConvert.DeserializeObject<T>(message); 
        }

        public async Task SendAsync(string message)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(message);
            await _clientWebSocket.SendAsync(new ArraySegment<byte>(encodedMessage), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
