using Business.Abstract;
using Business.Concrete;
using Business.Concrete.Profiles;
using DataAccess;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IWebSocketBinance,WebSocketBinance>();
builder.Services.AddScoped<ClientWebSocket>();
builder.Services.AddScoped<ITickerResultService, TickerResultService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IPastDatasService, PastDatasService>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<CyrptoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connectionstring")));
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.UseCustomException();
app.Run();


