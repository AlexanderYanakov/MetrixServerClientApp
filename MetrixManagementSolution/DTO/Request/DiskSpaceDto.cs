using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public class DiskSpaceDto
    {
        public string Name { get; set; }
        public double FreeSpace { get; set; }
        public double TotalSpace { get; set; }
        public Guid UserMetrixId { get; set; }

        public DiskSpaceDto(string name, double freeSpace, double totalSpace, Guid userMetrixId)
        {
            Name = name;
            FreeSpace = freeSpace;
            TotalSpace = totalSpace;
            UserMetrixId = userMetrixId;
        }
    }
}
