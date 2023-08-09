using Domain;
using Repository.Base;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DiskSpaceRepository : Repository<DiskSpace>, IDiskSpaceRepository
    {
        protected override DiskSpace Map(DbDataReader reader)
        {
            var result = new DiskSpace
            {
                id = reader.GetGuid(0),
                name = reader.GetString(1),
                free_space = reader.GetDouble(2),
                total_space = reader.GetDouble(3),
                is_deleted = reader.GetBoolean(4),
                ip_address = reader.GetString(5),
                create_date = reader.GetDateTime(6),
                update_date = !reader.IsDBNull(7) ? reader.GetDateTime(8) : null,
                delete_date = !reader.IsDBNull(8) ? reader.GetDateTime(9) : null
            };
            return result;
        }
    }
}
