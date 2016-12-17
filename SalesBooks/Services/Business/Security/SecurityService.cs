using Services.Base;
using Utils;

namespace Services.Security
{
    public class SecurityService : ServiceBase
    {
        #region Login
        public static ServiceResponse Login(string email, string password)
        {
            // Validate fields
            var sr = ValidateLogin(email, password);

            if (!sr.Status)
                return sr;

            // Find user by login
            var user = UserService.GetUser(email);

            if (user == null)
            {
                sr.AddError("El email no se encuentra registrado. Por favor, intente nuevamente.");
                sr.ReturnCode = LoginResultCode.FAILED;
                sr.UserStatus = UserStatus.NonAuthenticatedUser;
                return sr;
            }

            // Valid login and password
            if (PasswordUtilities.ComparePassword(password, user.Password))
            {
                if (user.Disable)
                {
                    sr.AddError("El usuario se encuentra inactivo. Por favor, contactese con el administrador para activar nuevamente el mismo.");
                    sr.ReturnCode = LoginResultCode.INACTIVE;
                    sr.UserStatus = UserStatus.NonAuthenticatedUser;
                }

                sr.ReturnCode = LoginResultCode.OK;
                sr.ReturnName = user.UserName;
                if (user.UserType == 1) sr.UserStatus = UserStatus.AuthenticatedAdmin;
                else sr.UserStatus = UserStatus.AuthenticatedUser;           

                return sr;
            }
            else
            {
                sr.AddError("La contraseña no es correcta. Por favor, intente nuevamente.");
                sr.ReturnCode = LoginResultCode.FAILED;
                sr.UserStatus = UserStatus.NonAuthenticatedUser;
            }

            return sr;
        }

        private static ServiceResponse ValidateLogin(string email, string password)
        {
            var sr = new ServiceResponse();

            if (string.IsNullOrEmpty(email))
                sr.AddError("Por favor ingrese el email");

            if (string.IsNullOrEmpty(password))
                sr.AddError("Por favor ingrese la contraseña");

            return sr;
        }

        //public static ServiceResponse GetDecryptedUserParams(string hash)
        //{
        //    // Decrypt parameters from encrypted email link
        //    // Check if the link has expired
        //    // Check if the user exist
        //    var sr = new ServiceResponse();

        //    try
        //    {
        //        // If there is a hash...
        //        if (string.IsNullOrEmpty(hash))
        //        {
        //            sr.AddError("The Link has expired or it is corrupted.");
        //            return sr;
        //        }

        //        // Try to decrypt the hash
        //        var decryptedParams = CustomEncrypt.Decrypt(hash);

        //        // Get decripted parameters
        //        var login = decryptedParams.Split(new string[] { "@@@@" }, StringSplitOptions.None)[0].ToString();
        //        var timeStamp = decryptedParams.Split(new string[] { "@@@@" }, StringSplitOptions.None)[1].ToDateTime();

        //        if (!timeStamp.HasValue)
        //        {
        //            sr.AddError("The Link has expired or it is corrupted.");
        //            return sr;
        //        }

        //        // Check if the link has expired
        //        var maxAllowedDate = timeStamp.Value.AddDays(Config.Security.PasswordExpirationDays);
        //        if (maxAllowedDate < DateTime.Now)
        //        {
        //            sr.AddError("The link has expired or it is corrupted.");
        //            return sr;
        //        }
        //        else // If not expired check if user valid
        //        {
        //            var user = UserService.GetUser(login);

        //            if (user == null)
        //            {
        //                sr.AddError("The link is not valid or corrupted");
        //                return sr;
        //            }
        //        }

        //        //TODOESTEBAN
        //        sr.ReturnCode = login;
        //    }
        //    catch
        //    {
        //        sr.AddError("The link has expired or it is corrupted.");
        //    }

        //    return sr;
        //}
        #endregion

        #region ChangePassword
        //public static ServiceResponse ChangePassword(string login, string oldPassword, string newPassword, string confirmNewPassword)
        //{
        //    // Validate fields
        //    var sr = ValidateChangePassword(login, oldPassword, newPassword, confirmNewPassword);

        //    if (!sr.Status)
        //        return sr;

        //    try
        //    {
        //        using (var dbContextTransaction = DB.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Find user by login
        //                var user = DB.User.Find(login);
        //                if (user == null || user.Password != oldPassword)
        //                {
        //                    sr.AddError("The User ID and/or Password are not recognized. The password is CASE sensitive. Please ensure that the Caps Lock setting is not enabled. Please re-enter your User Name and Password or contact your administrator to have your password reset.");
        //                    return sr;
        //                }

        //                //// Validate that the login name is equals to ActiveUser.Logn. (when we are changing password in first login passwordOld is null)
        //                //if (oldPassword == null && login != ActiveUser.Login)
        //                //    sr.AddError("You are not allowed to change password for a different user.");

        //                user.Password = newPassword;
        //                user.ChangePassword = false;
        //                user.EditDate = DateTime.Today;
        //                user.EditBy = login;

        //                DB.SaveChanges(login);

        //                dbContextTransaction.Commit();
        //            }
        //            catch (Exception exception)
        //            {
        //                dbContextTransaction.Rollback();
        //                sr.AddError(exception);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sr.AddError(string.Format("Password could not be changed: {0}", ex.Message));
        //    }

        //    return sr;
        //}

        //private static ServiceResponse ValidateChangePassword(string login, string oldPassword, string newPassword, string confirmNewPassword)
        //{
        //    var sr = new ServiceResponse();

        //    if (string.IsNullOrEmpty(login))
        //        sr.AddError("Please enter Username");

        //    if (string.IsNullOrEmpty(oldPassword))
        //        sr.AddError("Please enter the old password");

        //    if (string.IsNullOrEmpty(newPassword))
        //        sr.AddError("Please enter a new password");

        //    if (string.IsNullOrEmpty(confirmNewPassword))
        //        sr.AddError("Please confirm the new password");

        //    if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmNewPassword)
        //    && newPassword != confirmNewPassword)
        //        sr.AddError("The new password does not match with the confirmation password.");

        //    string malformedPasswordMsg = "The password must be a minimum of eight (8) characters and must have at least one capital letter, one number and one special character.  For example:  Password8#  Please try again.";

        //    // Validate new password length
        //    if (newPassword.Length < 8)
        //        sr.AddError(malformedPasswordMsg);

        //    // Validate if password contains uppercase, number and special chars
        //    bool hasUpper = false, hasNumber = false, hasSpecial = false;

        //    char[] specialChars = new char[] {'!', '@', '#', '$', '%', '^', '&', '*', '.', '?',
        //        '"', '·', '/', '(', ')', '=', '+', '-', '_', '.', ':', ',', ';', '<', '>', '\\',
        //        '|', '~', '€', '£', '¬', '{', '}', '[', ']', '`', '\''};
        //    ArrayList specialCharsList = new ArrayList(specialChars);

        //    foreach (char c in newPassword)
        //    {
        //        if (char.IsLower(c))
        //            continue;
        //        else if (char.IsUpper(c))
        //            hasUpper = true;
        //        else if (char.IsDigit(c))
        //            hasNumber = true;
        //        else if (specialCharsList.Contains(c))
        //            hasSpecial = true;
        //    }

        //    if (!hasUpper || !hasNumber || !hasSpecial)
        //        sr.AddError(malformedPasswordMsg);

        //    return sr;
        //}

        #endregion

        #region ResetPassword
        /// <summary>
		/// Resets the password of the specified user and send the new password by Email
		/// </summary>
		//public static ServiceResponse ResetPassword(string login)
  //      {
  //          // Validate fields
  //          var sr = ValidateResetPassword(login);

  //          if (!sr.Status)
  //              return sr;

  //          using (var dbContextTransaction = DB.Database.BeginTransaction())
  //          {
  //              try
  //              {
  //                  // Find user by login
  //                  var user = DB.User.Find(login);

  //                  if (user == null || user.Deleted || !user.Active)
  //                  {
  //                      sr.AddError("The User ID and/or Password are not recognized. The password is CASE sensitive. Please ensure that the Caps Lock setting is not enabled. Please re-enter your User Name and Password or contact your administrator to have your password reset.");
  //                      return sr;
  //                  }

  //                  // Validate that the user has an Email address to send the new password
  //                  if (string.IsNullOrWhiteSpace(user.Email))
  //                  {
  //                      sr.AddError("User does not have an Email address. Add user's Email address and the new password will be sent there.");
  //                      return sr;
  //                  }

  //                  // Generate new password
  //                  PasswordUtilities pu = new PasswordUtilities(true, true, true, true);
  //                  string newPassword = pu.GeneratePassword(8, 8);

  //                  user.Password = newPassword;
  //                  user.ChangePassword = true;
  //                  user.EditDate = DateTime.Today;
  //                  user.EditBy = login;

  //                  DB.SaveChanges(login);

  //                  // Send an Email with the new password
  //                  string subject = new AppSettings().ApplicationName + " Password Reset";
  //                  string body = string.Format("Dear User,\n\nYour previous password has been reset. The new one is: {0}\n\nYou must change it after your first login.\n\nThanks!\n" + new AppSettings().ApplicationName + " Support Team.", newPassword);

  //                  //TODO: Add Emailer Service
  //                  //Emailer.MailResult mres = Emailer.SendMail(new AppSettings().SmtpFromAddress, user.Email, subject, body);
  //                  //if (!mres.OK)
  //                  //{
  //                  //    sr.AddError("The Email with the new password could not be sent");
  //                  //    return sr;
  //                  //}

  //                  dbContextTransaction.Commit();
  //              }
  //              catch (Exception exception)
  //              {
  //                  dbContextTransaction.Rollback();
  //                  sr.AddError(exception);
  //              }
  //          }

  //          return sr;
  //      }
        #endregion
    }
}
