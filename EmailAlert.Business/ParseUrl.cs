using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Business
{
    public class ParseUrl
    {
        private static string fullURL;

        public static string Parse(StockUrl stock)
        {
            if (stock.TimeSeries == "TIME_SERIES_INTRADAY")
            {
                return fullURL =
                "https://www.alphavantage.co/query?function="
                + stock.TimeSeries +
                "&symbol="
                + stock.Ticker +
                "&interval="
                + stock.IntradayFrequency +
                "&outputsize="
                + stock.AmountOFData +
                "&apikey=1C7TYKVEGLDRJPAL";
            }
            else
            {
                return fullURL =
                "https://www.alphavantage.co/query?function="
                + stock.TimeSeries +
                "&symbol="
                + stock.Ticker +
                "&outputsize="
                + stock.AmountOFData +
                "&apikey=1C7TYKVEGLDRJPAL";
            }
        }
    }
}
//https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&outputsize=compact&apikey=MTJBD9ULVM57D8LQ
