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
        Task<ApiResponse<NoData>> LimitBuy(PostTrade dto,long currentUserId);
        Task<ApiResponse<NoData>> LimitSell(PostTrade dto, long currentUserId);
        Task<ApiResponse<Dictionary<string, decimal>>> OrtalamaMaliyet(long currentUserId);

        Task<ApiResponse<CoinMarketCapResponse>> CoinMarketCap(long currentUserId);
    }
}
