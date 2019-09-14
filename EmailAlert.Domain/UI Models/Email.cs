using System;
using System.Collections.Generic;

namespace EmailAlert.Domain
{
    public class Email
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
        //public List<RegisteredUser> Recipiants { get; set; }
        public bool UpFive { get; set; }
        public bool DownFive { get; set; }
        public bool MovingAvg { get; set; }
        public bool Admin { get; set; }
    }
}
