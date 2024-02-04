using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ParaTipi
    {
        public int Id { get; set; }
        public string DövizTipi { get; set; }
        public List<Bakiye> UserBakiyeler { get; set; }
    }
}
