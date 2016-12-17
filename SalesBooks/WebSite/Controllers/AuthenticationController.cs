using DAL.Entities;
using Services;
using Services.Security;
using System.Web.Mvc;
using System.Web.Security;
using ViewModels;
using WebSite.Filters;

namespace WebSite.Controllers
{
    [HeaderFooterFilter]
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            UserModel userModel = new UserModel();

            if (TempData["Email"] != null)
            {
                userModel.Email = TempData["Email"].ToString();
            }

            return View("Login", userModel);
        }

        public ActionResult Registration()
        {
            UserModel userModel = new UserModel();
            return RedirectToAction("Registration", "User", userModel);
        }

        [HttpPost]
        public ActionResult DoLogin(User u)
        {
            UserModel userModel = new UserModel();
            userModel.Email = u.Email;
            userModel.Password = u.Password;

            if (ModelState.IsValid)
            {
                var sr = SecurityService.Login(u.Email, u.Password);

                if (sr.Status && sr.ReturnCode == LoginResultCode.OK)
                {
                    bool IsAdmin = false;
                    if (sr.UserStatus == UserStatus.AuthenticatedAdmin)
                    {
                        IsAdmin = true;
                    }
                    else
                    {
                        IsAdmin = false;
                    }
                    
                    FormsAuthentication.SetAuthCookie(u.Email, false);
                    Session["IsAdmin"] = IsAdmin;
                    Session["UserName"] = sr.ReturnName;
                    

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("AuthenticationError", sr.ToString());

                    return View("Login", userModel);
                }
            }
            else
            {
                return View("Login", userModel);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}