using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Domain.Models
{
    public class InfluxDataPoint
    {
        public DateTime TimeStamp { get; set; }
        public string TagName { get; set; }
        public double? Value { get; set; }
    }
}
