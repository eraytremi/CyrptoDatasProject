using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos.UserDto
{
   
        public class Varliklar
        {
            public long UserId { get; set; }
            public List<BakiyeDetay> Bakiyeler { get; set; }
            public List<CoinListDetay> Coinler { get; set; }
        }

        public class BakiyeDetay
        {
            public string DovizTipi { get; set; }
            public decimal ParaMiktari { get; set; }
        }

        public class CoinListDetay
        {
            public string Symbol { get; set; }
            public double Count { get; set; }
        }


    
}
