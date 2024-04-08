using System;
using WebSocketSharp;
using Newtonsoft.Json;
using ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        decimal quoteVolume = 0;
        decimal quoteBase = 0;
        // WebSocket bağlantısını oluştur
        var ws = new WebSocket("wss://stream.binance.com:9443/ws/!ticker@arr");

        // Bağlantı açıldığında çalışacak event
        ws.OnOpen += (sender, e) =>
        {
            Console.WriteLine("Bağlantı açıldı.");
        };

        // Yeni bir mesaj geldiğinde çalışacak event
        ws.OnMessage += (sender, e) =>
        {
            // JSON verisini TickerData modeline dönüştür
            var tickerData = JsonConvert.DeserializeObject<TickerData[]>(e.Data);
            var getCurrency = tickerData.SingleOrDefault(p => p.Symbol == "BTCUSDT");
            // Tüm verileri konsola yazdır


            Console.WriteLine($"Sembol: {getCurrency.Symbol}");
            Console.WriteLine($"Fiyat: {getCurrency.LastPrice}");
            Console.WriteLine($"Event Zamanı: {UnixTimeStampToDateTime(getCurrency.EventTime)}");
            Console.WriteLine($"Fiyat Değişimi: {getCurrency.PriceChange}");
            Console.WriteLine($"Fiyat Değişim Yüzdesi: {getCurrency.PriceChangePercent}%");
            Console.WriteLine($"Toplam Yapılan İşlemler : {getCurrency.TotalNumberOfTrades}");
            Console.WriteLine($"Hacim ($) : {getCurrency.TotalTradedQuoteAssetVolume}");
            Console.WriteLine($"Hacim (Adet) : {getCurrency.TotalTradedBaseAssetVolume} Adet");
            Console.WriteLine($"Hacim Değişimi ($) : {getCurrency.TotalTradedQuoteAssetVolume-quoteVolume}$");
            Console.WriteLine($"Hacim Değişimi (Adet) : {getCurrency.TotalTradedBaseAssetVolume - quoteBase} Adet");
            quoteVolume = getCurrency.TotalTradedQuoteAssetVolume;
            quoteBase = getCurrency.TotalTradedBaseAssetVolume;

            Console.WriteLine();
           
        };

        // Bağlantı kapandığında çalışacak event
        ws.OnClose += (sender, e) =>
        {
            Console.WriteLine("Bağlantı kapandı.");
        };

        // Bağlantıyı başlat
        ws.Connect();

        // Konsoldan çıkmak için bir tuşa basılmasını bekleyin
        Console.ReadKey(true);

        // Bağlantıyı kapat
        ws.Close();
    }

    // Unix zaman damgasını DateTime nesnesine dönüştürme
    static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).UtcDateTime;
    }
}
