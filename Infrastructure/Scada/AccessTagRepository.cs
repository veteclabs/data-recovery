using Plate.Domain.Interfaces;
using Plate.Domain.Models;
using DevExpress.XtraRichEdit.Import.Html;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Infrastructure.Scada
{
    public class AccessTagRepository : IAccessDbRepository
    {

        private readonly string _connStr;

        public AccessTagRepository(Plate.Domain.Models.Scada scada)
        {
            // scada.ConnectionPath 에는 "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=…mdb;…" 형식의 문자열이 들어있다고 가정
            _connStr = scada.ConnectionPath;
        }

        public List<Project> GetProjects()
        {
            var list = new List<Project>();
            using (var conn = new OleDbConnection(_connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT ProjIdbw, ProjName FROM pProject";
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new Project
                        {
                            ProjIdbw = (int)rdr["ProjIdbw"],
                            ProjName = rdr["ProjName"].ToString()
                        });
            }
            return list;
        }

        public List<Node> GetNodes(int projId)
        {
            var list = new List<Node>();
            using (var conn = new OleDbConnection(_connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT ProjNodeIdbw, NodeName, ProjIdbw FROM pNode WHERE ProjIdbw = ?";
                cmd.Parameters.AddWithValue("?", projId);
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new Node
                        {
                            ProjNodeIdbw = (int)rdr["ProjNodeIdbw"],
                            ProjIdbw = (int)rdr["ProjIdbw"],
                            NodeName = rdr["NodeName"] as string
                        });
            }
            return list;
        }

        public List<Comport> GetComports(int nodeId)
        {
            var list = new List<Comport>();
            using (var conn = new OleDbConnection(_connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT ComportIdbw, ProjNodeIdbw, InterfaceName FROM pComport WHERE ProjNodeIdbw = ?";
                cmd.Parameters.AddWithValue("?", nodeId);
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new Comport
                        {
                            ComportIdbw = (int)rdr["ComportIdbw"],
                            ProjNodeIdbw = (int)rdr["ProjNodeIdbw"],
                            InterfaceName = rdr["InterfaceName"].ToString()
                        });
            }
            return list;
        }

        public List<Device> GetDevices(int comportId)
        {
            var list = new List<Device>();
            using (var conn = new OleDbConnection(_connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT DeviceIdbw, ComportIdbw, DeviceName FROM pDevice WHERE ComportIdbw = ?";
                cmd.Parameters.AddWithValue("?", comportId);

                using (var rdr = cmd.ExecuteReader())
                {
                    // ordinals 미리 얻어두기 (약간의 성능 향상)
                    int ordDeviceId = rdr.GetOrdinal("DeviceIdbw");
                    int ordComport = rdr.GetOrdinal("ComportIdbw");
                    int ordName = rdr.GetOrdinal("DeviceName");

                    while (rdr.Read())
                    {
                        // Access COUNTER 는 .NET 상에 Int32 로 반환됨을 고려
                        long devId = Convert.ToInt64(rdr.GetValue(ordDeviceId));
                        int prtId = Convert.ToInt32(rdr.GetValue(ordComport));
                        string name = rdr.GetString(ordName);

                        list.Add(new Device
                        {
                            DeviceIdbw = devId,
                            ComportIdbw = prtId,
                            DeviceName = name
                        });
                    }
                }
            }
            return list;
        }

        public List<Domain.Models.Tag> GetTags(long deviceId)
        {
            var list = new List<Domain.Models.Tag>();
            using (var conn = new OleDbConnection(_connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT TagIdbw, DeviceIdbw, TagName,TagType, ComportIdbw FROM pTag WHERE DeviceIdbw = ?";
                cmd.Parameters.AddWithValue("?", deviceId);

                using (var rdr = cmd.ExecuteReader())
                {
                    int ordTagId = rdr.GetOrdinal("TagIdbw");
                    int ordDevId = rdr.GetOrdinal("DeviceIdbw");
                    int ordTagType = rdr.GetOrdinal("TagType");
                    int ordComportId = rdr.GetOrdinal("ComportIdbw");
                    int ordTagName = rdr.GetOrdinal("TagName");

                    while (rdr.Read())
                    {
                        // Access COUNTER 타입은 .NET 상에 Int32 로 반환되므로 Convert 사용
                        long tagId = Convert.ToInt64(rdr.GetValue(ordTagId));
                        long devId = Convert.ToInt64(rdr.GetValue(ordDevId));
                        string name = rdr.IsDBNull(ordTagName)
                                        ? string.Empty
                                        : rdr.GetString(ordTagName);

                        list.Add(new Domain.Models.Tag
                        {
                            TagIdbw = tagId,
                            DeviceIdbw = devId,
                            TagName = name,
                            TagType = ordTagType,
                            ComportIdbw = ordComportId
                        });
                    }
                }
            }
            return list;
        }
        public List<Domain.Models.Tag> GetTags()
        {
            var result = new List<Domain.Models.Tag>();

            string connStr = _connStr;
            using (var conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT [TagName] FROM [pTag]";

                using (var cmd = new OleDbCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Domain.Models.Tag
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
