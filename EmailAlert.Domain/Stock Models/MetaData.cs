using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Domain
{
    public class MetaData
    {
        public string Information { get; set; }
        public string Ticker { get; set; }
        public DateTime LastRefreshed { get; set; }
        public string Interval { get; set; }
        public string OutputSize { get; set; }
        public string TimeZone { get; set; }
    }
}
