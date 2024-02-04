using AutoMapper;
using Model;
using Model.Dtos.TradeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class TradeMapper : Profile
    {
        public TradeMapper()
        {
            CreateMap<PostTrade, Trade>();
            CreateMap<Trade, GetTrade>();

        }
    }
}
