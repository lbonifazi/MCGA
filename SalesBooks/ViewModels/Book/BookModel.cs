using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BookModel : BaseModel
    {
        public Books Book;
        public IList<Author> AuthorAllList;
        public IList<Subject> SubjectAllList;
        public IList<Publisher> PublisherAllList;
        public IList<Book> BookSearchList;
    }
}
