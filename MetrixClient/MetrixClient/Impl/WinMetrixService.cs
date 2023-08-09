
using Domain;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text.Json;

namespace Metrixclient.Impl
{
    public class WinMetrixService : IMetrixService
    {
        public static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
        public MetrixInfo GetMetrix()
        {
            var ipaddress = GetIPAddress();
            var cpuUsage = GetCPUUsage();
            var ram = GetRAM();
            var diskSpaces = GetDiskSpaces();

            return new MetrixInfo(ipaddress, cpuUsage, ram, diskSpaces);
        }
        public static string GetIPAddress()
        {
            var IPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList
            .Where(ips => !ips.IsIPv6LinkLocal && !ips.IsIPv6Multicast && !ips.IsIPv6SiteLocal)
            .First()
            .ToString();

            return IPAddress;
        }

        public static RAM GetRAM()
        {
            ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);

            RAM ram = new RAM();
            foreach (ManagementObject item in searcher.Get())
            {
                var memoryKb = Convert.ToInt32(item["TotalVisibleMemorySize"].ToString());
                
                ram.Free = (int)ramCounterAvailable.NextValue();
                ram.Total = memoryKb / 1024;
            }

            return ram;
        }

        public static List<DiskSpace> GetDiskSpaces()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<DiskSpace> diskSpaces = new List<DiskSpace>();

            foreach (DriveInfo Drive in allDrives)
            {
                if (Drive.IsReady == true)
                {
                    diskSpaces.Add(new DiskSpace(Drive.Name, Drive.TotalFreeSpace / 1024 / 1024 / 1024, Drive.TotalSize / 1024 / 1024 / 1024, GetIPAddress()));

                }
            }

            return diskSpaces;
        }
        public static float GetCPUUsage()
        {
            return cpuCounter.NextValue();
        }
    }
}
