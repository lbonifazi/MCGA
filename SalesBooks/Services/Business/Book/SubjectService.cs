using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SubjectService : ServiceBase
    {

        public static List<Subject> GetAll()
        {
            return DB.Subject.OrderByDescending(s => s.Name).ToList();
        }

        internal static List<string> GetSubject(int bookId)
        {
            return DB.
                   Subject.
                   Join(DB.BookSubject, s => s.SubjectId, bs => bs.SubjectId, (s, bs) => new { s, bs }).
                   Where(w => w.bs.BookId == bookId).Select(se => se.s.Name).ToList();
        }
    }
}
