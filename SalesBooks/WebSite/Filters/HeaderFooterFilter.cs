using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace WebSite.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        { 
            ViewResult v = filterContext.Result as ViewResult;
            if (v != null)
            {
                BaseModel bvm = v.Model as BaseModel;

                if (bvm != null)//bvm will be null when we want a view without Header and footer
                {
                    bvm.NavbarData = new NavbarModel();

                    if (filterContext.HttpContext.Session["UserName"] != null)
                    {
                        bvm.NavbarData.IsAdmin = Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]);
                        bvm.NavbarData.UserName = filterContext.HttpContext.Session["UserName"].ToString();
                        bvm.NavbarData.IsLogged = true;
                    }

                    bvm.FooterData = new FooterModel();
                    bvm.FooterData.CompanyName = "MCGAStore";
                    bvm.FooterData.Year = DateTime.Now.Year.ToString();
                }
            }
        }
    }
}