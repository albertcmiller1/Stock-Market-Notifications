using System;
using System.Collections.Generic;
using System.Text;
using EmailAlert.Domain;
using EmailAlert.Data;
using EmailAlert.Data.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace EmailAlert.Business
{
    public class AnalyzeStocks
    {
        private IStockService _stocks;
        public ParseUsersStocks ParseUsersStocks { get; }

        public AnalyzeStocks(IStockService Stocks, ParseUsersStocks parseUsersStocks)
        {
            _stocks = Stocks;
            ParseUsersStocks = parseUsersStocks;
        }

        public async Task<List<Stock>> GetStockData(StockUrl urlParts)
        {
            var fullUrl = ParseUrl.Parse(urlParts);
            return await _stocks.CallStockApi(fullUrl, urlParts);
        }

        public async Task<List<string>> CheckUpFive()
        {
            //gets the tickers of the stocks we should check
            //would save some time here to check if there are multiple stocks with the same ticker and not have to run each one
            var StocksToCheck = ParseUsersStocks.getUpFiveStocks();
            var listToReturn = new List<string>();

            foreach (var stock in StocksToCheck)
            {
                StockUrl currentURL = new StockUrl
                {
                    Ticker = stock,
                    IntradayFrequency = null,
                    AmountOFData = "full",
                    TimeSeries = "TIME_SERIES_DAILY",
                    Description = "Time Series (Daily)"
                };

                var data = await this.GetStockData(currentURL);

                var first = 0;
                var firstStock = data.ElementAt(first);
                var secondStock = data.ElementAt(first + 1);

                var percentChange = (firstStock.close - secondStock.close) / secondStock.close;

                if (percentChange >= 0.05)
                {
                    listToReturn.Add(stock);
                }
            }
            return listToReturn;
        }

        public async Task<List<string>> CheckDownFive()
        {
            var StocksToCheck = ParseUsersStocks.getUpFiveStocks();
            var listToReturn = new List<string>();

            foreach (var stock in StocksToCheck)
            {
                StockUrl currentURL = new StockUrl
                {
                    Ticker = stock,
                    IntradayFrequency = null,
                    AmountOFData = "full",
                    TimeSeries = "TIME_SERIES_DAILY",
                    Description = "Time Series (Daily)"
                };

                var data = await this.GetStockData(currentURL);

                var first = 0;
                var firstStock = data.ElementAt(first);
                var secondStock = data.ElementAt(first + 1);

                var percentChange = (firstStock.close - secondStock.close) / secondStock.close;

                if (percentChange <= -0.05)
                {
                    listToReturn.Add(stock);
                }
            }
            return listToReturn;
        }

        public async Task<List<string>> CheckMovingAverage()
        {
            var StocksToCheck = ParseUsersStocks.getUpFiveStocks();
            var listToReturn = new List<string>();

            foreach (var stock in StocksToCheck)
            {
                StockUrl currentURL = new StockUrl
                {
                    Ticker = stock,
                    IntradayFrequency = null,
                    AmountOFData = "full",
                    TimeSeries = "TIME_SERIES_DAILY",
                    Description = "Time Series (Daily)"
                };

                var data = await this.GetStockData(currentURL);

                var first = 0;
                var firstPlusTwenty = first + 20;

                List<double> myList = new List<double> { };

                for (int i = 0; i < firstPlusTwenty; i++)
                {
                    var currentStock = data.ElementAt(i);

                    myList.Add(currentStock.close);
                }

                var avg = myList.Sum() / 20;

                var todaysStock = data.ElementAt(first).close;
                var yesterdayStock = data.ElementAt(first + 1).close;
                
                if (todaysStock > avg && yesterdayStock < avg)
                {
                    listToReturn.Add(stock);
                }
                else if (todaysStock < avg && yesterdayStock > avg)
                {
                    listToReturn.Add(stock);
                }
            }
            return listToReturn;
        }
    }
}
