using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Domain.Models
{
    public class Influxdb
    {
        public string Url { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Organizations { get; set; }
        public string Buckets { get; set; }
        
    }
}
