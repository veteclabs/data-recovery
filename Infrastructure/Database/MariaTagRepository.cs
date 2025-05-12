using Dapper;
using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
using MySqlConnector;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Infrastructure.Database
{
    public class MariaTagRepository : IMariaTagRepository
    {
        private readonly string _connectionString;

        public MariaTagRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<MariaDataPoint> GetTagDataPerDay(string logDate,  string tagName, int projNodeId)
        {
            string sql = @"
                            SELECT
                                STR_TO_DATE(CONCAT(bt.LogDate, ' ', bt.LogTime), '%y/%m/%d %H:%i:%s') AS TimeStamp,
                                bt.TagName AS TagName,
                                COALESCE(bt.LastValue, 0) AS Value
                            FROM
                                bwanalogtable bt
                            WHERE
                                bt.ProjNodeId = @ProjNodeId
                                AND bt.TagName = @TagName
                                AND bt.LogDate = @LogDate;";

            using (var conn = new MySqlConnection(_connectionString))
            {
                return conn.Query<MariaDataPoint>(sql, new
                {
                    ProjNodeId = projNodeId,
                    TagName = tagName,
                    LogDate = logDate
                }).ToList();
            }
        }
        public List<MariaDataPoint> GetTagData(List<string> tagNames, DateTime start, DateTime end, int projNodeId)
        {
            
            var result = new ConcurrentBag<MariaDataPoint>();

            Parallel.ForEach(tagNames, tagName =>
            {
                for (DateTime currentDay = start.Date; currentDay <= end.Date; currentDay = currentDay.AddDays(1))
                {
                    string logDate = currentDay.ToString("yy-MM-dd").Replace("-", "/");

                    var dailyPoints = GetTagDataPerDay(logDate, tagName, projNodeId);

                    foreach (var dp in dailyPoints)
                    {
                        dp.TagName = tagName;
                        result.Add(dp);
                    }
                }
            });

            return result
                .Where(x => x.TimeStamp != default) // 혹시 모르니 안전망
                .OrderBy(x => x.TimeStamp)
                .ToList();
        }

    }
}
