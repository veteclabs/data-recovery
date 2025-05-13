using Plate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Domain.Interfaces
{
    public interface IInfluxTagRepository
    {
        List<InfluxDataPoint> GetTagData(List<string> tagNames, DateTime start, DateTime end);
    }
}
