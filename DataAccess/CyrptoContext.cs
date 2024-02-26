using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using Model.PastDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccess
{
    public class CyrptoContext:DbContext
    {
        public CyrptoContext()
        {
        }

        public CyrptoContext(DbContextOptions<CyrptoContext> options) : base(options)
        {
        }

        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ParaTipi> ParaTipi { get; set; }
        public DbSet<Bakiye> Bakiye { get; set; }
        public DbSet<CoinList> CoinList { get; set; }
        public DbSet<GetPastDatas> PastDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-R04PVQ3\\SQLEXPRESS; Database = CyrptoTradeData; Trusted_Connection = true; TrustServerCertificate = true;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User modeli için seed verileri
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Eray",
                    LastName = "Türemiş",
                    Email = "eray@mail.com",
                    Password = "1"

                },
                new User
                {
                    Id = 2,
                    FirstName = "Erdal",
                    LastName = "Türemiş",
                    Email = "erdal@mail.com",
                    Password = "2"
                }
                );

            modelBuilder.Entity<ParaTipi>().HasData(
                new ParaTipi
                {
                    Id = 1,
                    DövizTipi = "TL"
                },
                new ParaTipi
                 {
                     Id = 2,
                     DövizTipi = "USDT"
                 },
                new ParaTipi
                   {
                       Id = 3,
                       DövizTipi = "BTC"
                   },
                new ParaTipi
                  {
                      Id = 4,
                      DövizTipi = "ETH"
                  }
                );
            modelBuilder.Entity<Bakiye>().HasData(
                new Bakiye
            {
                Id = 1,
                ParaTipiId = 1,
                ParaMiktarı = 10000000,
                UserId = 1

            },
            
            new Bakiye
            {
                Id = 2,
                ParaTipiId = 2,
                ParaMiktarı = 10000000,
                UserId = 1

            },
            new Bakiye
            {
                Id = 3,
                ParaTipiId = 1,
                ParaMiktarı = 10000000,
                UserId = 2,

            },
            new Bakiye
            {
                Id = 4,
                ParaTipiId = 2,
                ParaMiktarı = 10000000,
                UserId = 2,

            }
            );
        }

    }
}
