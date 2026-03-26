using System;

namespace z4
{
    class StockEventArgs : EventArgs
    {
        public string StockName;
        public double Price;

        public StockEventArgs(string stockName, double price)
        {
            StockName = stockName;
            Price = price;
        }
    }

    class StockMarket
    {
        public event EventHandler<StockEventArgs>? StockPriceUpdated;

        public void UpdateStockPrice(string stockName, double price)
        {
            Console.WriteLine("Акция " + stockName + ": новая цена " + price);
            if (StockPriceUpdated != null)
            {
                StockPriceUpdated(this, new StockEventArgs(stockName, price));
            }
        }
    }

    class Investor
    {
        public void OnStockPriceUpdated(object? sender, StockEventArgs e)
        {
            if (e.Price > 100)
            {
                Console.WriteLine("Инвестор: акция " + e.StockName + " дорогая (" + e.Price + ")");
            }
            else
            {
                Console.WriteLine("Инвестор: акция " + e.StockName + " дешевая (" + e.Price + ")");
            }
        }
    }

    class NewsPublisher
    {
        public void OnStockPriceUpdated(object? sender, StockEventArgs e)
        {
            Console.WriteLine("Новости: цена акции " + e.StockName + " изменилась до " + e.Price);
        }
    }

    class MarketObserver
    {
        public MarketObserver(StockMarket market, Investor investor, NewsPublisher news)
        {
            if (market != null)
            {
                market.StockPriceUpdated += investor.OnStockPriceUpdated;
                market.StockPriceUpdated += news.OnStockPriceUpdated;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StockMarket market = new StockMarket();
            Investor investor = new Investor();
            NewsPublisher news = new NewsPublisher();

            MarketObserver observer = new MarketObserver(market, investor, news);

            market.UpdateStockPrice("БПС-Сбербанк", 95);
            market.UpdateStockPrice("Беларусбанк", 110);
            market.UpdateStockPrice("Приорбанк", 85);

            Console.ReadLine();
        }
    }
}