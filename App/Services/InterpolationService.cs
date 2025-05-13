using Plate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.App.Services
{
    public class InterpolationService
    {
        public List<TagComparisonPoint> Interpolate(List<TagComparisonPoint> input)
        {
            // 1) 원본 보호를 위한 깊은 복사
            var list = input.Select(p => p.Clone()).ToList();

            // 2) 태그별로 그룹핑
            foreach (var group in list.GroupBy(p => p.TagName))
            {
                var pts = group.OrderBy(p => p.TimeStamp).ToList();
                
                // 3a) 사용자가 입력한 ManualValue 우선 반영
                foreach (var pt in pts)
                {
                    if (pt.ManualValue.HasValue)
                    {
                        pt.Interpolated = pt.ManualValue;
                        pt.InterpolationStatus = "Manual entry";
                    }
                }
                // 3b) 먼저 Influx missing → Maria used 처리
                foreach (var pt in pts)
                {
                    if ((pt.Value_Influx == null || pt.Value_Influx == 0) && pt.Value_Maria.HasValue)
                    {
                        pt.Interpolated = pt.Value_Maria;
                        pt.InterpolationStatus = "Influx missing → Maria used";
                    }
                }
                
                // 4) Anchor(수동 또는 Maria) 리스트 수집
                var anchors = new List<int>();
                for (int i = 0; i < pts.Count; i++)
                {
                    var pt = pts[i];
                    if (pt.ManualValue.HasValue || pt.Value_Maria.HasValue)
                    {
                        anchors.Add(i);                       
                    }
                }

                // 5) Anchor가 2개 이상 있을 때만 보간
                for (int a = 0; a < anchors.Count - 1; a++)
                {
                    int s = anchors[a], e = anchors[a + 1];
                    double? vStart = pts[s].ManualValue ?? pts[s].Value_Maria ?? pts[s].Value_Influx;
                    double? vEnd = pts[e].ManualValue ?? pts[e].Value_Maria;

                    if (!vStart.HasValue || !vEnd.HasValue)
                        continue;

                    int span = e - s;
                    double step = (vEnd.Value - vStart.Value) / span;

                    for (int k = 1; k < span; k++)
                    {
                        int idx = s + k;
                        // Maria나 Manual이 원래 있든 없든 모두 덮어쓰기
                        pts[idx].Interpolated = Math.Round(vStart.Value + step * k, 2);
                        pts[idx].InterpolationStatus = "Interpolated (auto)";
                    }
                }
            }

            return list;
        }

    }

}
