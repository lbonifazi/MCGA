using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSite.Controllers;
using DAL.Entities;
using System.Web.Mvc;
using Services;
using DAL;
using System.Runtime.Remoting.Messaging;
using System.Configuration;

namespace WebSite.Tests
{
    [TestClass]
    public class Login
    {
        [TestMethod]
        public void Login_MissingFields()
        {
            User user = new User();
            AuthenticationController authenticationController = new AuthenticationController();
            ActionResult result;

            //Validate missing password
            user.Email = "leonel.bonifazi@gmail.com";
            user.Password = "";
            result = authenticationController.DoLogin(user);
            Assert.IsFalse(authenticationController.ModelState.IsValid);

            //Validate missing email
            user.Email = "";
            user.Password = "aaa";
            result = authenticationController.DoLogin(user);
            Assert.IsFalse(authenticationController.ModelState.IsValid);
        }

        [TestMethod]
        public void Login_WrongPassword()
        {
            User user = new User();
            AuthenticationController authenticationController = new AuthenticationController();
            ServiceConfig.RegisterConfig();

            user.Email = "leonel.bonifazi@gmail.com";
            user.Password = "aasdadsaa";
            ActionResult result = authenticationController.DoLogin(user);

            Assert.IsFalse(authenticationController.ModelState.IsValid);
        }

        [TestMethod]
        public void Login_WrongEmail()
        {
            User user = new User();
            AuthenticationController authenticationController = new AuthenticationController();
            ServiceConfig.RegisterConfig();

            user.Email = "leonel.bonifazi";
            user.Password = "aaa";
            ActionResult result = authenticationController.DoLogin(user);

            Assert.IsFalse(authenticationController.ModelState.IsValid);
        }
    }
}
