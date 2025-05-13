using Plate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Domain.Interfaces
{
    public interface IAccessDbRepository
    {
        List<Project> GetProjects();
        List<Node> GetNodes(int projId);
        List<Comport> GetComports(int nodeId);
        List<Device> GetDevices(int comportId);
        List<Tag> GetTags(long deviceId);
        List<Tag> GetTags();

    }
}
