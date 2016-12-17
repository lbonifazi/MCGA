using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Consts
    {

    }

    public class LoginResultCode
    {
        public const string OK = "OK";
        public const string FAILED = "FAILED";
        public const string PASSWORD_EXPIRED = "PASSWORD_EXPIRED";
        public const string INACTIVE = "INACTIVE";
        public const string LOCKED = "LOCKED";
    }

    public class RegisterResultCode
    {
        public const string OK = "OK";
        public const string FAILED = "FAILED";
        public const string EMAIL_EXISTING = "EMAIL_EXISTING";
    }

    public enum UserStatus
    {
        AuthenticatedAdmin,
        AuthenticatedUser,
        NonAuthenticatedUser
    }
}
