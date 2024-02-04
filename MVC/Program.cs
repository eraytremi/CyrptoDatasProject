using PurchasingSystem.Web.ApiServices.Implementations;
using PurchasingSystem.Web.ApiServices.Interfaces;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IHttpApiService,HttpApiService>();  
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Authentication}/{action=LogIn}/{id?}");


            app.Run();
        }
    }
}
