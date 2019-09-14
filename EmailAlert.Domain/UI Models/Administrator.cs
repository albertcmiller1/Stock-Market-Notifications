using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Domain
{
    public class Administrator
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Id { get; set; }
    }
}
