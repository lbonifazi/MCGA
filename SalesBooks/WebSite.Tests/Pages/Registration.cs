using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSite.Controllers;

namespace WebSite.Tests
{
    [TestClass]
    public class Registration
    {
        [TestMethod]
        public void Registration_MissingFields()
        {
            List<User> userlist = new List<User>();
            List<string> repeatPassword = new List<string>();
            UserController userController = new UserController();

            //Fill fields
            for (int i = 0; i < 4; i++)
            {
                User user = new User();
                user.UserName = "user abc";
                user.Email = "abc@gmail.com";
                user.Password = "abc";
                string rp = "abc";

                userlist.Add(user);
                repeatPassword.Add(rp);
            }

            //Clear one field
            for (int i = 0; i < 4; i++)
            {
                if (i == 0) userlist[i].UserName = "";
                if (i == 1) userlist[i].Email = "";
                if (i == 2) userlist[i].Password = "";
                if (i == 3) repeatPassword[i] = "";
            }

            //Validation
            for (int i = 0; i < 4; i++)
            {
                ActionResult result = userController.DoRegistration(userlist[i], repeatPassword[i]);
                Assert.IsFalse(userController.ModelState.IsValid);
            }
        }

        [TestMethod]
        public void Registration_WrongEmail()
        {
            User user = new User();
            string repeatPassword;
            UserController userController = new UserController();
            ActionResult result;

            //Validate wrong email
            user.UserName = "user abc";
            user.Email = "abc@gmail.com";
            user.Password = "abc";
            repeatPassword = "abc";
            result = userController.DoRegistration(user, repeatPassword);
            Assert.IsFalse(userController.ModelState.IsValid);

            //Validate existing email
            ServiceConfig.RegisterConfig();
            user.UserName = "user abc";
            user.Email = "leonel.bonifazi@gmail.com";
            user.Password = "abc";
            repeatPassword = "abc";
            result = userController.DoRegistration(user, repeatPassword);
            Assert.IsFalse(userController.ModelState.IsValid);
        }
    }
}
