using Domain;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class LinMetrixService : IMetrixService
    {
        public MetrixInfo GetMetrix()
        {
            throw new NotImplementedException();
        }

        private static void GetMemory()
        {
            long totalMemory = 0;
            try
            {
                var lines = System.IO.File.ReadAllLines("/proc/meminfo");
                foreach (var line in lines)
                {
                    if (line.StartsWith("MemTotal"))
                    {
                        var values = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        totalMemory = Convert.ToInt64(values[values.Length - 2]) / 1024;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            long usageMemory = 0;
            Process.GetProcesses().ToList().ForEach(p =>
            {
                usageMemory += p.WorkingSet64 / 1024 / 1024;
            });

            Console.WriteLine("Total memory usage is " + totalMemory + "MB");
            Console.WriteLine("Usage " + usageMemory);
            Console.WriteLine("Free " + (totalMemory - usageMemory));
        }

        private static void GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addresses = hostEntry.AddressList;

            foreach (var address in addresses)
            {
                Console.WriteLine("IP address: " + address);
            }
        }

        private static void GetProcessorTime()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            Console.WriteLine(cpuUsageTotal * 100);
        }


        private static void GetDiskSpace()
        {
            var a = DriveInfo.GetDrives();
            var name = a.First().Name;
            var total = a.First().TotalSize;
            var free = a.First().TotalFreeSpace / 1024 / 1024 / 1024;

            var totalDiskSize = DriveInfo.GetDrives().Sum(d => d.TotalSize);
            var totalDiskFreeSpace = DriveInfo.GetDrives().Sum(d => d.TotalFreeSpace);
            var totalDiskAvailiableFreeSpace = DriveInfo.GetDrives().Sum(d => d.AvailableFreeSpace);
            Console.WriteLine("Total totalDiskSize: " + totalDiskSize / 1024 / 1024 / 1024);
            Console.WriteLine("Total totalDiskFreeSpace: " + totalDiskFreeSpace / 1024 / 1024 / 1024);
            Console.WriteLine("Total totalDiskAvailiableFreeSpace: " + totalDiskAvailiableFreeSpace / 1024 / 1024 / 1024);
        }
    }
}
