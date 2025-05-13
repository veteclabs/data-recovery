using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Domain.Models
{
    public class Scada
    {
        public string FilePath { get; set; }

        public string ProjNodeId { get; set; }

        public string ConnectionPath => $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={FilePath};";

    }
}
