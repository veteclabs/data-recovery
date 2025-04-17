using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Domain.Models
{
    public class TagComparisonPoint
    {
        public DateTime TimeStamp { get; set; }
        public string TagName { get; set; }
        public double? Value_Influx { get; set; }
        public double? Value_Maria { get; set; }  // 누락 가능성 있음
        public double? Diff { get; set; }

        // 🔥 추가: 보간값
        public double? Interpolated { get; set; }
        public string InterpolationStatus { get; set; }
    }

}
