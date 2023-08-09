using Domain;
using Repository.Base;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MetricRepository : Repository<Metrix>, IMetricRepository
    {
        protected override Metrix Map(DbDataReader reader)
        {
            var result = new Metrix
            {
                id = reader.GetGuid(0),
                ip_address = reader.GetString(1),
                cpu_usage = reader.GetFloat(2),
                ram_free = reader.GetInt32(3),
                ram_total = reader.GetInt32(4),
                is_deleted = reader.GetBoolean(5),
                create_date = reader.GetDateTime(6),
                update_date = !reader.IsDBNull(7) ? reader.GetDateTime(8) : null,
                delete_date = !reader.IsDBNull(8) ? reader.GetDateTime(9) : null
            };
            return result;
        }
    }
}
