using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Domain.Models
{
    public class Project
    {
        public int ProjIdbw { get; set; }
        public string ProjName { get; set; }
    }

    public class Node
    {
        public int ProjNodeIdbw { get; set; }
        public int ProjIdbw { get; set; }
        public string NodeName { get; set; }
    }

    public class Comport
    {
        public int ComportIdbw { get; set; }
        public int ProjNodeIdbw { get; set; }
        public string InterfaceName { get; set; }
    }

    public class Device
    {
        public long DeviceIdbw { get; set; }
        public int ComportIdbw { get; set; }
        public string DeviceName { get; set; }
    }

    public class Tag
    {
        public long TagIdbw { get; set; }
        public long DeviceIdbw { get; set; }
        public int TagType { get; set; }
        public int ComportIdbw { get; set; }
        public string TagName { get; set; }
    }
}
