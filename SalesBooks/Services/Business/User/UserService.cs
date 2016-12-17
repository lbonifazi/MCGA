using DAL.Entities;
using Services;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Services
{
    public class UserService : ServiceBase
    {
        public static User GetUser(string email)
        {
            return DB
                .User
                .AsNoTracking()
                .FirstOrDefault(x => x.Email == email);
        }

        public static User ActivateUser(string activationCode)
        {
            User user = DB
                        .User
                        .Where(u => u.Disable == true && u.ActivationCode == activationCode)
                        .FirstOrDefault();
            if (user != null)
            {
                user.Disable = false;
                DB.SaveChanges();

                return user;
            }
            else return null;
        }

        private static bool ValidEmail (string email)
        {
            var user = DB.User.AsNoTracking().FirstOrDefault(x => x.Email == email);

            if (user == null) return true;
            else return false;
        }

        public static ServiceResponse AddUser(User user)
        {
            var sr = new ServiceResponse();
            try
            {
                if (ValidEmail(user.Email))
                {
                    user.Password = PasswordUtilities.EncryptStringAES(user.Password);
                    user.CreateDt = DateTime.Today;
                    user.UserType = 0;
                    user.PasswordTempInd = false;
                    user.Disable = true;
                    user.ActivationCode = PasswordUtilities.EncryptStringAES(user.Email);

                    DB.User.Add(user);
                    DB.SaveChanges();

                    sr.ReturnValue = user.UserId;
                    sr.ReturnCode = RegisterResultCode.OK;
                }
                else
                {
                    sr.AddError("El email ingresado ya se encuentra registrado.");
                    sr.ReturnCode = RegisterResultCode.EMAIL_EXISTING;
                }

            }
            catch(Exception e)
            {
                sr.AddError(e.Message);
                sr.ReturnCode = RegisterResultCode.FAILED;
            }

            return sr;
        }

        public static ServiceResponse RegisterUser(User user, string RepeatPassword)
        {
            // Validate fields
            var sr = ValidateFields(user, RepeatPassword);

            if (!sr.Status)
                return sr;

            sr = AddUser(user);

            return sr;
        }

        private static ServiceResponse ValidateFields(User user, string repeatPassword)
        {
            var sr = new ServiceResponse();

            if (string.IsNullOrEmpty(user.UserName))
                sr.AddError("Por favor ingrese el nombre de usuario");

            if (string.IsNullOrEmpty(user.Email))
                sr.AddError("Por favor ingrese el email");

            if (string.IsNullOrEmpty(user.Password))
                sr.AddError("Por favor ingrese la contraseña");

            if (user.Password != repeatPassword)
                sr.AddError("Las contraseñas ingresadas deben coincidir");

            return sr;
        }
    }
}
