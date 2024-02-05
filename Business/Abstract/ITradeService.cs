using Infrastructure;
using Model;
using Model.Dtos;
using Model.Dtos.TradeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITradeService
    {
        Task<ApiResponse<NoData>> MarketBuy(PostTrade dto,long currentUserId);
        Task<ApiResponse<NoData>> MarketSell(PostTrade dto,long currentUserId);
        Task LimitBuy(PostTrade dto,long currentUserId);
        Task LimitSell(PostTrade dto, long currentUserId);

    }
}
