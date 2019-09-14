using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Domain
{
    public class StockUrl
    {
        public string Ticker { get; set; }                //MSFT
        public string IntradayFrequency { get; set; }     //5min
        public string AmountOFData { get; set; }          //full
        public string TimeSeries { get; set; }            //TIME_SERIES_INTRADAY
        public string Description { get; set; }           //"Time Series (5min)"
    }
}


//https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&outputsize=compact&apikey=MTJBD9ULVM57D8LQ
