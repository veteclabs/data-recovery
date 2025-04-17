using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Infrastructure.Scada
{
    public class AccessTagRepository : ITagRepository
    {
        private readonly DataRecorvery.Domain.Models.Scada _scadaConfig;

        public AccessTagRepository(DataRecorvery.Domain.Models.Scada scada)
        {
            _scadaConfig = scada;
        }

        public List<Tags> GetTags()
        {
            var result = new List<Tags>();

            string connStr = _scadaConfig.ConnectionPath;
            using (var conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT [TagName] FROM [pTag]";

                using (var cmd = new OleDbCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Tags
                        {
                            TagName = reader["TagName"].ToString()
                        });
                    }
                }
            }

            return result;
        }
    }
}
