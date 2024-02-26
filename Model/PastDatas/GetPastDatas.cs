using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PastDatas
{

    public class GetPastDatas
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        
        public double Price { get; set; }
       
        public double VolumeByCurrencyCount { get; set; }
        
        public double VolumeByParity { get; set; }
        
        public string Symbol { get; set; }
    }
}
