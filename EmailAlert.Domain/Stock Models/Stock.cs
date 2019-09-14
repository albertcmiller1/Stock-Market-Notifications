using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Domain
{
    public class Stock
    {
        public DateTime date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public int volume { get; set; }
    }
}
