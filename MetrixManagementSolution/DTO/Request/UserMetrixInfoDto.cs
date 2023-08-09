using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public class UserMetrixInfoDto
    {
        public long IPAddress { get; set; }
        public double CPUUsage { get; set; }
        public double RAMFree { get; set; }
        public double RAMTotal { get; set; }

        public UserMetrixInfoDto(long iPAddress, double cPUUsage, double rAMFree, double rAMTotal)
        {
            IPAddress = iPAddress;
            CPUUsage = cPUUsage;
            RAMFree = rAMFree;
            RAMTotal = rAMTotal;
        }
    }
}
