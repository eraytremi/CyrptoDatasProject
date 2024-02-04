using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWebSocketBinance
    {
        Task ConnectAsync(Uri serverUri);
        Task SendAsync(string message);
        Task<T> ReceiveAsync<T>();
        Task CloseAsync();
    }
}
