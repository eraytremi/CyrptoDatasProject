using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Trade
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime Time { get; set; }  
        public bool isBuy { get; set; } 
        public bool isSell { get; set; } 
        public double Count { get; set; } 
        public string Symbol { get; set; }
        public bool WaitingTrades { get; set; } 
        public decimal Price { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        

    }
}
