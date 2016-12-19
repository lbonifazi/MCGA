using DAL.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSite.Filters;

namespace WebSite.Controllers
{
    [HeaderFooterFilter]
    public class BookController : Controller
    {
        public ActionResult Admin()
        {
            BookModel bookModel = new BookModel();
            bookModel.Book = new Books();
            bookModel.Book.Authors = new List<Author>();
            bookModel.Book.Subjects = new List<string>();
            bookModel.AuthorAllList = AuthorService.GetAll();
            bookModel.SubjectAllList = SubjectService.GetAll();
            bookModel.PublisherAllList = PublisherService.GetAll();
            bookModel.BookSearchList = BookService.GetBookByName("");

            return View("Admin", bookModel);
        }

        [HttpPost]
        public ActionResult SaveBook(Books book, List<int> authors, List<int> subjects)
        {
            if (book.BookId == 0)
            {
                BookService.NewBook(book, authors, subjects);
                return Admin();
            }
            else
            {
                BookService.UpdateBook(book, authors, subjects);
                return Admin();
            }
        }

        [HttpGet]
        public ActionResult SearchByTitle(string SearchByTitle)
        {
            BookModel bookModel = new BookModel();
            bookModel.Book = new Books();
            bookModel.Book.Title = SearchByTitle;
            bookModel.BookSearchList = BookService.GetBookByName(SearchByTitle);

            return PartialView("SearchBookSection", bookModel);
        }

        [HttpGet]
        public ActionResult EditBook(string bookId)
        {
            BookModel bookModel = new BookModel();
            if(bookId != null) bookModel.Book = BookService.GetBook(Convert.ToInt32(bookId));
            bookModel.AuthorAllList = AuthorService.GetAll();
            bookModel.SubjectAllList = SubjectService.GetAll();
            bookModel.PublisherAllList = PublisherService.GetAll();

            return PartialView("BookDataSection", bookModel);
        }
    }
}