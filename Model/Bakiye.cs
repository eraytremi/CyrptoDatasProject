using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bakiye
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int ParaTipiId { get; set; }
        public Decimal ParaMiktarı { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("ParaTipiId")]
        public ParaTipi? ParaTipi { get; set; }

    }
}
