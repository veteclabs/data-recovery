using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Domain.Models
{
    public class SettingsConfig
    {
        public DatabaseConfig DatabaseConfig { get; set; }
        public Influxdb Influxdb { get; set; }
        public Scada Scada { get; set; }

        public SettingsConfig()
        {
            DatabaseConfig = new DatabaseConfig();
            Influxdb = new Influxdb();
            Scada = new Scada();
        }
    }
}
