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

        public static void NewBook(Books book, List<int> authors, List<int> subjects)
        {
            Book newBook = new Book();
            newBook.Title = book.Title;
            newBook.ISBN = book.ISBN;
            newBook.Abstract = book.Abstract;
            newBook.Edition = book.Edition;
            newBook.Volume = book.Volume;
            newBook.PublishYear = book.PublishYear;
            newBook.PublisherId = book.PublisherId;

            DB.Book.Add(newBook);
            DB.SaveChanges();

            Price bookPrice = new Price();
            bookPrice.BookId = newBook.BookId;
            bookPrice.Cost = book.Price;
            bookPrice.Date = DateTime.Now.Date;
            DB.Price.Add(bookPrice);

            foreach (int authorId in authors)
            {
                DB.BookAuthor.Add(new Book_Author() { BookId = newBook.BookId, AuthorId = authorId });
            }

            foreach (int subId in subjects)
            {
                DB.BookSubject.Add(new Book_Subject() { BookId = newBook.BookId, SubjectId = subId });
            }

            DB.SaveChanges();
        }

        public static bool UpdateBook(Books book,List<int> authors, List<int> subjects)
        {
            var DBBook = DB.Book.SingleOrDefault(b => b.BookId == book.BookId);
            decimal currentPrice = DB.Price.Where(p => p.BookId == book.BookId).OrderByDescending(o => o.Date).First().Cost;

            if (DBBook != null)
            {
                DBBook.Title = book.Title;
                DBBook.ISBN = book.ISBN;
                DBBook.Abstract = book.Abstract;
                DBBook.Edition = book.Edition;
                DBBook.Volume = book.Volume;
                DBBook.PublishYear = book.PublishYear;
                DBBook.PublisherId = book.PublisherId;

                List<int> currentAutorList = DB.BookAuthor.Where(b => b.BookId == book.BookId).Select(s=> s.AuthorId).ToList();

                foreach (int authorId in authors)
                {  
                    if (!currentAutorList.Contains(authorId))
                    {
                        DB.BookAuthor.Add(new Book_Author() { BookId = book.BookId, AuthorId = authorId });
                    }
                }
                foreach (int au in currentAutorList)
                {
                    if (!authors.Contains(au))
                    {
                        Book_Author ba = DB.BookAuthor.Where(b => b.BookId == book.BookId && b.AuthorId == au).FirstOrDefault();
                        DB.BookAuthor.Remove(ba);
                    }
                }

                foreach (Book_Subject ba in DB.BookSubject.Where(b => b.BookId == book.BookId).ToList())
                {
                    DB.BookSubject.Remove(ba);
                }
                foreach (int subId in subjects)
                {
                    DB.BookSubject.Add(new Book_Subject() { BookId = book.BookId, SubjectId = subId });
                }

                if (currentPrice != book.Price)
                {
                    var DBPrice = DB.Price.ToList();
                    DBPrice.Add(new Price { BookId = book.BookId, Cost = currentPrice, Date = DateTime.Now.Date });
                }

                if (DB.SaveChanges() > 0) return true;
                else return false;
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
