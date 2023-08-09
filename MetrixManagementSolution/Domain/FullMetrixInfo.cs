using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FullMetrixInfo
    {
        public string IPAddress { get; set; }
        public float CPUUsage { get; set; }
        public RAM RAM { get; set; }
        public List<DiskSpace> DiskSpaces { get; set; }

        public FullMetrixInfo(string ipAddress, float cpuUsage, RAM ram, List<DiskSpace> diskSpaces)
        {
            IPAddress = ipAddress;
            CPUUsage = cpuUsage;
            RAM = ram;
            DiskSpaces = diskSpaces;
        }
        public FullMetrixInfo() { }
    }
}
