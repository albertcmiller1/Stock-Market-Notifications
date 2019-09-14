using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Domain
{
    public class RegisteredUser
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Ticker> Stocks { get; set; }
        public bool UpFive { get; set; }
        public bool DownFive { get; set; }
        public bool MovingAvg { get; set; }
        public bool Admin { get; set; }
    }
}

