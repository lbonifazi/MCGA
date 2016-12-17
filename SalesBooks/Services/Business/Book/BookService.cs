using DAL.Entities;
using Services.Business.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public class BookService : ServiceBase
    {
        public static IList<Books> GetAllBooks(string subject)
        {
            List<Books> bmList = new List<Books>();
            List<Book> bookList;
            if (subject != null)
            {
                int sub = Convert.ToInt32(subject);
                bookList = DB.Book.Join(DB.BookSubject, b => b.BookId, bs => bs.BookId, (b, bs) => new { b, bs }).Where(w => w.bs.SubjectId == sub).Select(s => s.b).ToList();
            }
            else
            {
                bookList = DB.Book.ToList();
            }

            foreach (Book book in bookList)
            {
                Books bm = new Books();
                bm.Abstract = book.Abstract;
                bm.BookId = book.BookId;
                bm.Edition = book.Edition;
                bm.ISBN = book.ISBN;
                bm.PublisherId = book.PublisherId;
                bm.PublishYear = book.PublishYear;
                bm.Title = book.Title;
                bm.Volume = book.Volume;

                bm.Price = PriceService.GetCurrentPrice(bm.BookId);
                bm.Stars = ReviewService.GetStars(bm.BookId);
                bm.Reviews = ReviewService.GetAmountReview(bm.BookId);

                bmList.Add(bm);
            }

            return bmList;
        }

        public static bool Save(Books book)
        {
            var result = DB.Book.SingleOrDefault(b => b.BookId == book.BookId);
            if (result != null)
            {
                result.Abstract = book.Abstract;
                result.Edition = book.Edition;
                result.PublishYear = book.PublishYear;

                DB.SaveChanges();
                return true;
            }
            return false;
        }

        public static Books GetBook(int bookId)
        {
            Book book = DB.Book.Where(b => b.BookId == bookId).FirstOrDefault();

            Books bm = new Books();
            bm.Abstract = book.Abstract;
            bm.BookId = book.BookId;
            bm.Edition = book.Edition;
            bm.ISBN = book.ISBN;
            bm.PublisherId = book.PublisherId;
            bm.PublishYear = book.PublishYear;
            bm.Title = book.Title;
            bm.Volume = book.Volume;

            bm.Subjects = SubjectService.GetSubject(bm.BookId);
            bm.Authors = AuthorService.GetAuthors(bm.BookId);
            bm.Price = PriceService.GetCurrentPrice(bm.BookId);
            bm.Stars = ReviewService.GetStars(bm.BookId);
            bm.Reviews = ReviewService.GetAmountReview(bm.BookId);

            return bm;
        }

        public static IList<Book> GetBookByName(string bookName)
        {
            List<Book> bookList = new List<Book>();
            if (bookName != "")
            {
                bookList = DB.Book.Where(b => b.Title.Contains(bookName)).OrderByDescending(o => o.Title).ToList();
            }            
            return bookList;
        }

        public static List<Subject> GetAllSubjects()
        {
            return DB
                .Subject.ToList();
        }
    }
}
