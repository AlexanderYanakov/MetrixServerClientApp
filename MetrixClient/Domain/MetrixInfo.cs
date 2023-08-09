using System;
using System.Collections.Generic;

namespace Domain
{
    public class MetrixInfo
    {
        public string IPAddress { get; set; }
        public float CPUUsage { get; set; }
        public RAM RAM { get; set; }
        public List<DiskSpace> DiskSpaces { get; set; }

        public MetrixInfo(string iPAddress, float cPUUsage, RAM ram, List<DiskSpace> diskSpaces)
        {
            IPAddress = iPAddress;
            CPUUsage = cPUUsage;
            RAM = ram;
            DiskSpaces = diskSpaces;
        }
        public MetrixInfo() { }
    }
}
