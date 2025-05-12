using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
using InfluxData.Net.Common.Constants;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataRecorvery.Infrastructure.InfluxDb
{
    public class InfluxTagRepository : IDisposable
    {
        private readonly InfluxDBClient _client;
        private readonly string _bucket;
        private readonly string _org;

        public InfluxTagRepository(string url, string token, string org, string bucket)
        {
            // 한 번만 client 생성
            _client = new InfluxDBClient(url, token);
            _org = org;
            _bucket = bucket;
        }
        public async Task RunAggregationAndTimeShiftAsync(string tagName, DateTime startDate)
        {
            // 시작일~1년 후 종료일 계산
            var startTime = startDate.Date;
            var endTime = startTime.AddYears(1);

            // UTC 포맷으로 변환
            var start = startTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + "Z";
            var end = endTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            List<string> timeUnits = new List<string>();
            timeUnits.AddRange(new[] { "hour", "day", "month", "year" });
           
            var queryApi = _client.GetQueryApi();

            foreach (var unit in timeUnits)
            {
                string every, duration;
                switch (unit)
                {
                    case "hour":
                        every = "1h"; duration = "-1h"; break;
                    case "day":
                        every = "1d"; duration = "-1d"; break;
                    case "month":
                        every = "1mo"; duration = "-1mo"; break;
                    case "year":
                        every = "1y"; duration = "-1y"; break;
                    default:
                        throw new ArgumentException("Invalid time unit.");
                }

                // Flux 쿼리 문자열 구성
                var fluxQuery = string.Format(@"
from(bucket: ""history"")
  |> range(start: {0}, stop: {1})
  |> filter(fn: (r) => r[""_measurement""] == ""minute"")
  |> filter(fn: (r) => r[""_field""] == ""value"")
  |> filter(fn: (r) => r[""tagname""] == ""{2}"")
  |> aggregateWindow(every: {3}, fn: last, createEmpty: false)
  |> timeShift(duration: {4})
  |> set(key: ""_measurement"", value: ""{5}"")
  |> to(bucket: ""history"", org: ""{6}"")
", start, end, tagName, every, duration, unit, _org);

                try
                {
                    Console.WriteLine(fluxQuery);
                    await queryApi.QueryAsync(fluxQuery, _org);
                }
                catch (Exception ex)
                {
                    // 예외 처리 및 로그
                    MessageBox.Show("Flux 쿼리 실행 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
        public async Task WriteInterpolatedData(IEnumerable<TagComparisonPoint> points)
        {
            var toWrite = new List<PointData>();
            foreach (var p in points)
            {
                if (!p.Interpolated.HasValue)
                    continue;

                var ts = DateTime.SpecifyKind(
                            p.TimeStamp.AddMinutes(-1) ,    
                            DateTimeKind.Utc);               
                
                var pt = PointData
                    .Measurement("minute")
                    .Tag("tagname", p.TagName)
                    .Field("value", p.Interpolated.Value)
                    .Timestamp(ts, WritePrecision.S);

                toWrite.Add(pt);
            }
            try
            {
                using (var writeApi = _client.GetWriteApi())
                {
                    foreach (var item in toWrite)
                    {
                        writeApi.WritePoint(item, _bucket, _org);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Flux 쿼리 실행 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            // 3) 내부에서 startDate 계산 (보간된 데이터의 최소 날짜 기준)
            var startDate = points
                .Where(p => p.Interpolated.HasValue)
                .Min(p => p.TimeStamp)
                .Date;

            // —2) 원본 points 에서 태그 목록 뽑기—
            var distinctTags = points
                .Where(p => p.Interpolated.HasValue)
                .Select(p => p.TagName)
                .Distinct();

            // —3) 태그별로 다운샘플링/타임시프트 수행—
            foreach (var tag in distinctTags)
            {
                await RunAggregationAndTimeShiftAsync(tag, startDate);
            }
        }

        public List<InfluxDataPoint> GetTagData(
            List<string> tagNames, DateTime start, DateTime end)
        {
            var result = new List<InfluxDataPoint>();

            // range 파라미터
            string range = $"range(start: {start:yyyy-MM-ddTHH:mm:ss}Z, stop: {end:yyyy-MM-ddTHH:mm:ss}Z)";

            // tagname 필터 생성
            string tagFilter = BuildTagFilter(tagNames);

            // Flux 쿼리
            string flux = $@"
                            from(bucket: ""{_bucket}"")
                            |> {range}
                            |> filter(fn: (r) => r[""_measurement""] == ""minute"")
                            |> filter(fn: (r) => r[""_field""] == ""value"")
                            |> filter(fn: (r) => {tagFilter})
                            |> aggregateWindow(fn: last, every: 15m, createEmpty: true)
                            |> fill(column: ""_value"", value: 0.0)
                            //|> timeShift(duration: -15m)
                            ";

            var queryApi = _client.GetQueryApi();
            var tables = queryApi.QueryAsync(flux, _org).GetAwaiter().GetResult();

            // 결과 파싱
            foreach (var table in tables)
                foreach (var record in table.Records)
                    result.Add(new InfluxDataPoint
                    {
                        TimeStamp = record.GetTimeInDateTime().GetValueOrDefault().ToUniversalTime(),
                        TagName = record.Values["tagname"]?.ToString(),
                        Value = Convert.ToDouble(record.GetValue())
                    });

            return result;
        }

        private string BuildTagFilter(List<string> tagNames)
        {
            if (tagNames == null || tagNames.Count == 0)
                throw new ArgumentException("tagNames 리스트가 비어 있습니다.");

            // r["tagname"] 필드를 or 연결
            return string.Join(" or ",
                tagNames.Select(t => $"r[\"tagname\"] == \"{t}\""));
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }

}
