namespace Domain;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;

[Table("metrix", Schema = "public")]
public class Metrix : PersistentObject
{
    [Column("ip_address")]
    public string ip_address { get; set; }
    [Column("cpu_usage")]
    public float cpu_usage { get; set; }
    [Column("ram_total")]
    public int ram_total { get; set; }
    [Column("ram_free")]
    public int ram_free { get; set; }

    public Metrix(string ipAddress, float cpuUsage, int ramTotal, int ramFree)
    {
        ip_address = ipAddress;
        cpu_usage = cpuUsage;
        ram_total = ramTotal;
        ram_free = ramFree;
    }

    public Metrix() { }
}
