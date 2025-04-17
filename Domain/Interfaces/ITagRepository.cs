using DataRecorvery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRecorvery.Domain.Interfaces
{
    public interface ITagRepository
    {
        List<Tags> GetTags();
    }
}
