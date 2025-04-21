using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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

        public void WriteInterpolatedData(IEnumerable<TagComparisonPoint> points)
        {
            var toWrite = points
       .Where(p => p.Interpolated.HasValue)
       .Select(p =>
       {
           var utc = p.TimeStamp.ToUniversalTime();
           // 초 이하 제거 (밀리초 단위로 맞춤)
           var ts = new DateTime(
               utc.Year, utc.Month, utc.Day,
               utc.Hour, utc.Minute, 0, DateTimeKind.Utc);

           return PointData
               .Measurement("minute")
               .Tag("tagname", p.TagName)
               .Field("value", p.Interpolated.Value)
               .Timestamp(ts, WritePrecision.Ms);
       })
       .ToList();

            // 동기(블로킹) 쓰기: GetWriteApi().WritePoints(...)
            using (var writeApi = _client.GetWriteApi())
            {
                writeApi.WritePoints(toWrite, _bucket, _org);
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
                            |> timeShift(duration: -15m)
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
