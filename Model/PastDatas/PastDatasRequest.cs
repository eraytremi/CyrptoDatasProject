using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Model.PastDatas
{
    public class PastDatasRequest
    {
        public string Response { get; set; }
        public int Type { get; set; }
        public bool Aggregated { get; set; }
        public long TimeTo { get; set; }
        public long TimeFrom { get; set; }
        public bool FirstValueInArray { get; set; }
        public ConversionType ConversionType { get; set; }
        public List<Data> Data { get; set; }
    }
    public class ConversionType
    {
        public string type { get; set; }
        public string conversionSymbol { get; set; }
    }

    public class Data
    {
        public long time { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double open { get; set; }
        public double close { get; set; }
        public double volumefrom { get; set; }
        public double volumeto { get; set; }
        public string conversionType { get; set; }
        public string conversionSymbol { get; set; }
    }

}
