using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Base;

namespace Domain
{
    [Table("disk_space", Schema = "public")]
    public class DiskSpace : PersistentObject
    {
        [Column("name")]
        [JsonPropertyName("Name")]
        public string name { get; set; }
        [Column("free_space")]
        [JsonPropertyName("FreeSpace")]
        public double free_space { get; set; }
        [Column("total_space")]
        [JsonPropertyName("TotalSpace")]
        public double total_space { get; set; }
        [Column("ip_address")]
        [JsonPropertyName("IPAddress")]
        public string ip_address { get; set; }

        public DiskSpace(string Name, double freeSpace, double totalSpace, string ipAddress)
        {
            name = Name;
            free_space = freeSpace;
            total_space = totalSpace;
            ip_address = ipAddress;
        }

        public DiskSpace() { }
    }
}
