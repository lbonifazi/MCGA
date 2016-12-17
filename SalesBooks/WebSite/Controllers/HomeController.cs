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
    public class HomeController : Controller
    {
        [HeaderFooterFilter]
        public ActionResult Index(string SubjectId)
        {
            HomeModel homeModel = new HomeModel();

            homeModel.BookList = BookService.GetAllBooks(SubjectId);
            homeModel.Subjects = BookService.GetAllSubjects();

            if (Session["UserName"] != null)
            {
                homeModel.IsAdmin = Convert.ToBoolean(Session["IsAdmin"] != null) ? true : false;
                homeModel.UserName = Session["UserName"].ToString();
                homeModel.IsLogged = true;
            }

            return View("Index", homeModel);
        }
    }
}