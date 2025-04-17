using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Infrastructure.InfluxDb
{
    public class InfluxTagRepository : IInfluxTagRepository
    {
        private readonly string _url;
        private readonly string _token;
        private readonly string _org;
        private readonly string _bucket;

        public InfluxTagRepository(string url, string token, string org, string bucket)
        {
            _url = url;
            _token = token;
            _org = org;
            _bucket = bucket;
        }

        public List<InfluxDataPoint> GetTagData(List<string> tagNames, DateTime start, DateTime end)
        {
            var result = new List<InfluxDataPoint>();

            using (var client = new InfluxDBClient(_url, _token))
            {

                string range = $"range(start: {start.ToUniversalTime().AddDays(1):yyyy-MM-ddTHH:mm:ss}Z, stop: {end.ToUniversalTime().AddDays(1):yyyy-MM-ddTHH:mm:ss}Z)";

                string tagFilter;
                if (tagNames.Count == 1)
                {
                    tagFilter = $"r[\"tagname\"] == \"{tagNames[0]}\"";
                }
                else
                {
                    tagFilter = string.Join(" or ", tagNames.Select(t => $"r[\"tagname\"] == \"{t}\""));
                    tagFilter = $"({tagFilter})"; // 괄호로 감싸기
                }

                string flux = $@"
  from(bucket: ""history"")
  |> {range}  
  |> filter(fn: (r) => r[""_measurement""] == ""minute"")
  |> filter(fn: (r) => r[""_field""] == ""value"")
  |> filter(fn: (r) => {tagFilter})
  |> aggregateWindow(
      fn: last, 
      every: 15m, 
      createEmpty: true
  )
  |> fill(column: ""_value"", value: 0.0)
  |> yield(name: ""fixed"")
//|> timeShift(duration: -15m)
                     ";


                var queryApi = client.GetQueryApi();
                var tables = queryApi.QueryAsync(flux, _org).GetAwaiter().GetResult();

                foreach (var table in tables)
                {
                    foreach (var record in table.Records)
                    {
                        result.Add(new InfluxDataPoint
                        {
                            TimeStamp = (DateTime)record.GetTimeInDateTime().GetValueOrDefault(),
                            TagName = record.Values["tagname"]?.ToString(),  // ✅ 핵심 수정
                            Value = Convert.ToDouble(record.GetValue())
                        });
                    }
                }
            }

            return result;
        }

        private string BuildTagFilter(List<string> tagNames)
        {
            if (tagNames == null || tagNames.Count == 0)
                throw new ArgumentException("tagNames 리스트가 비어 있습니다.");

            return string.Join(" or ", tagNames.Select(tag => $"r._field == \"{tag}\""));
        }

    }
}
