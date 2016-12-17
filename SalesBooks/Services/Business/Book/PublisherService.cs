using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PublisherService : ServiceBase
    {
        public static List<Publisher> GetAll()
        {
            return DB.Publisher.OrderByDescending(p => p.Name).ToList();
        }
    }
}
