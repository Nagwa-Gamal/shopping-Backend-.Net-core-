using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Authentication
{
    public class response
    {
        public string Message { get; set; }
        public string Status { get; set; }

        public List<string> Errors { get; set; }
    }
}
