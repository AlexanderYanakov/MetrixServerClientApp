using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DiskSpace
    {
        public string Name { get; set; }
        public double FreeSpace { get; set; }
        public double TotalSpace { get; set; }
        public string IPAddress { get; set; }

        public DiskSpace(string name, double freeSpace, double totalSpace, string iPAddress)
        {
            Name = name;
            FreeSpace = freeSpace;
            TotalSpace = totalSpace;
            IPAddress = iPAddress;
        }
    }
}
