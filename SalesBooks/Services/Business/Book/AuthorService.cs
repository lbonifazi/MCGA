using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthorService : ServiceBase
    {
        public static List<Author> GetAll()
        {
            return DB.Author.OrderByDescending(a => a.LastName).ToList();
        }

        internal static List<string> GetAuthors(int bookId)
        {
            return DB.
                   Author.
                   Join(DB.BookAuthor, a => a.AuthorID, ba => ba.AuthorId, (a, ba) => new { a, ba }).
                   Where(w => w.ba.BookId == bookId).Select(s => s.a.LastName + ", " + s.a.FirstName).ToList();
        }
    }
}
