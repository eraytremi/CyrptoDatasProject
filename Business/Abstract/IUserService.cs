using Infrastructure;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
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
        ApiResponse<GetUserDto> Login(string email, string password);
        Task<ApiResponse<Varliklar>> GetUserVarliklar(long UserId);
        Task<ApiResponse<List<GetTrade>>> GetMyTrades(long UserId);
    }
}
