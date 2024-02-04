using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Dtos.TradeDto;
using Model.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        GetUserDto Login(string email, string password);
        Task<Varliklar> GetUserVarliklar(long UserId);
        Task<List<GetTrade>> GetMyTrades(long UserId);
    }
}
