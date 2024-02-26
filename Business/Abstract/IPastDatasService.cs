using Infrastructure;
using Model.PastDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPastDatasService
    {
        Task InsertPastDatasFromCryptoCompare(long currentUserId);
        Task<ApiResponse<CalculatePastAndCurrentValue>> GetCalculatePastAndCurrentValue(DateTimeOffset date, string symbol, double bid);
    }
}
