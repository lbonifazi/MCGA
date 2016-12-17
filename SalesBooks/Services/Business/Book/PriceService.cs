using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Business.Book
{
    public class PriceService : ServiceBase
    {
        public static decimal GetCurrentPrice(int bookId)
        {
            return DB.Price.Where(p => p.BookId == bookId).OrderByDescending(d => d.Date).Select(s => s.Cost).FirstOrDefault();
        }
    }
}
