using Domain;
using DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CreateMetricsDto
    {
        public string IPAddress { get; set; }
        public List<DiskSpace> DiskSpaces { get; set; }
        public double FreeRAM { get; set; }
        public double TotalRAM { get; set; }
        public double CPUUsage { get; set; }

        public CreateMetricsDto(string iPAddress, List<DiskSpace> diskSpaces, double freeRAM, double totalRAM, double cPUUsage)
        {
            IPAddress = iPAddress;
            DiskSpaces = diskSpaces;
            FreeRAM = freeRAM;
            TotalRAM = totalRAM;
            CPUUsage = cPUUsage;
        }
    }
}
