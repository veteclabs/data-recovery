using DataRecorvery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.App.Services
{
    public class InterpolationService
    {
        public List<TagComparisonPoint> Interpolate(List<TagComparisonPoint> input)
        {
            string currentTag = null;
            int? startIdx = null;
            int? endIdx = null;

            for (int i = 0; i < input.Count; i++)
            {
                var point = input[i];

                if (!string.Equals(currentTag, point.TagName))
                {
                    currentTag = point.TagName;
                    startIdx = null;
                    endIdx = null;
                }

                // ✅ [1] Maria → Influx 보정
                if ((point.Value_Influx == null || point.Value_Influx == 0) &&
                    point.Value_Maria.HasValue && point.Value_Maria > 0)
                {
                    point.Interpolated = point.Value_Maria;
                    point.InterpolationStatus = "Influx missing → Maria used";
                    continue;
                }

                // ✅ [2] Influx → Maria 보정 (기존 로직)
                if (point.Value_Maria.HasValue)
                {
                    if (startIdx == null)
                        startIdx = i;
                    else
                    {
                        endIdx = i;

                        var startVal = input[startIdx.Value].Value_Influx;
                        var endVal = input[endIdx.Value].Value_Influx;
                        int count = endIdx.Value - startIdx.Value - 1;

                        bool isInvalid = !startVal.HasValue || !endVal.HasValue
                                         || startVal < 0 || endVal < 0
                                         || endVal <= startVal
                                         || (endVal - startVal) > 10000;

                        if (count > 0 && !isInvalid)
                        {
                            double step = (endVal.Value - startVal.Value) / (count + 1);
                            for (int j = 1; j <= count; j++)
                            {
                                int idx = startIdx.Value + j;

                                if (input[idx].Value_Maria.HasValue)
                                    continue; // 값 있는 경우 skip

                                input[idx].Interpolated = Math.Round(startVal.Value + step * j, 2);
                                input[idx].InterpolationStatus = "Interpolated from Influx";
                            }
                        }
                        else if (count > 0)
                        {
                            for (int j = 1; j <= count; j++)
                            {
                                int idx = startIdx.Value + j;
                                input[idx].Interpolated = null;
                                input[idx].InterpolationStatus = "Skipped due to invalid pattern";
                            }
                        }

                        startIdx = endIdx;
                        endIdx = null;
                    }
                }
            }

            return input;
        }
    }

}
