using Domain;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class WinMetrixService/* : IMetrixService*/
    {
        public void /*MetrixInfo*/ GetMetrix()
        {
            PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", "OUTLOOK", true);

            Console.WriteLine("Press the any key to stop...\n");
            while (!Console.KeyAvailable)
            {
                double pct = myAppCpu.NextValue();
                Console.WriteLine("OUTLOOK'S CPU % = " + pct);
                Thread.Sleep(250);
            }
        }
    }
}
