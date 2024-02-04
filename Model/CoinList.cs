using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CoinList
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Symbol { get; set; }
        public double  Count { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

    }
}
