using DAL.Entities;
using Services;
using System;
using System.Web.Mvc;
using Utils;
using ViewModels;
using WebSite.Filters;

namespace WebSite.Controllers
{
    [HeaderFooterFilter]
    public class UserController : Controller
    {        
        public ActionResult Registration()
        {
            UserModel userModel = new UserModel();
            return View("Registration", userModel);
        }

        [AllowAnonymous]
        public ActionResult ConfirmAccount(string token)
        {
            User user = UserService.ActivateUser(token);

            UserModel userModel = new UserModel();
            if (user != null)
            {
                userModel.UserName = user.UserName;
            }
            return View("ConfirmAccount", userModel);
        }

        [HttpPost]
        public ActionResult DoRegistration(User u, string RepeatPassword)
        {
            UserModel userModel = new UserModel();
            userModel.Email = u.Email;
            userModel.UserName = u.UserName;
            userModel.Password = u.Password;

            if (ModelState.IsValid)
            {
                var sr = UserService.RegisterUser(u, RepeatPassword);

                if (sr.Status && sr.ReturnCode == RegisterResultCode.OK)
                {
                    var urlActivation = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                    "/User/ConfirmAccount?token=" + Server.UrlEncode(u.ActivationCode);

                    if (EmailUtilities.SendActivationEmail(u, urlActivation))
                    {
                        TempData["Email"] = u.Email;
                        return RedirectToAction("Login", "Authentication");
                    }
                    else
                    {
                        //No se pudo enviar el password.reintentar.
                        return RedirectToAction("User", "Reintenter");
                    }
                }
                else
                {
                    ModelState.AddModelError("RegistrationError", sr.ToString());

                    return View("Registration", userModel);
                }
            }
            else
            {
                return View("Registration", userModel);
            }
        }
    }
}