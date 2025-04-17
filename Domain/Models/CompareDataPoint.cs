using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Domain.Models
{
    public class CompareDataPoint
    {
        public DateTime Timestamp { get; set; }
        public string TagName { get; set; }
        public double? ValueInflux { get; set; }
        public double? ValueMaria { get; set; }
        public double? Diff => ValueMaria.HasValue && ValueInflux.HasValue ? ValueInflux - ValueMaria : null;
        public double? Interpolated { get; set; }
    }
}
