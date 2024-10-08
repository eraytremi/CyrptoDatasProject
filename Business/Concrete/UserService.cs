﻿using AutoMapper;
using Business.Abstract;
using DataAccess;
using Infrastructure;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Dtos.TradeDto;
using Model.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly CyrptoContext _context;
        private readonly IMapper _mapper;
        public UserService(CyrptoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApiResponse<GetUserDto> Login(string email, string password)
        {
            var control = _context.Users.SingleOrDefault(p => p.Email == email && p.Password == password);
            if (control == null)
            {
                return ApiResponse<GetUserDto>.Fail(StatusCodes.Status400BadRequest, "Hatalı giriş işlemi!");
            }
            var mapping = _mapper.Map<GetUserDto>(control);
            return ApiResponse<GetUserDto>.Success(StatusCodes.Status200OK,mapping);
        }

        public async Task<ApiResponse<Varliklar>> GetUserVarliklar(long userId)
        {
            var kullaniciVarliklari = new Varliklar
            {
                UserId = userId,
                Bakiyeler = await _context.Bakiye
                .Include(b => b.ParaTipi) 
                .Where(b => b.UserId == userId)
                .Select(b => new BakiyeDetay
             {
                DovizTipi = b.ParaTipi.DövizTipi, 
                ParaMiktari = b.ParaMiktarı
             })
            .ToListAsync(),
                Coinler = await _context.CoinList
                .Where(c => c.UserId == userId)
                .Select(c => new CoinListDetay { Symbol = c.Symbol, Count = c.Count })
                .ToListAsync()
            };

            return ApiResponse<Varliklar>.Success(StatusCodes.Status200OK,kullaniciVarliklari);
        }

        public async Task<ApiResponse<List<GetTrade>>> GetMyTrades(long UserId)
        {
            var getTrades =  _context.Trades.Where(p => p.UserId == UserId);

            var mapping = await getTrades.
                        Select(t => new GetTrade
                        {
                            Id = t.Id,
                            Price = t.Price,
                            Count = t.Count,
                            Symbol = t.Symbol,
                            Time = t.Time,
                            UserId = UserId
                        }).ToListAsync();
            return ApiResponse<List<GetTrade>>.Success(StatusCodes.Status200OK, mapping);

            //IQueryable<Trade> query = _context.Trades;

            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    query = query.Where(p => EF.Functions.Like(p.Symbol, $"%{searchTerm}%"));
            //}

            //return await PaginatedList<Trade>.ToPagedList(query.OrderBy(p=>p.Id), pageNumber, pageSize);


        }


    }
}
