using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaArt.DbModel;

namespace InstaArt.DataBaseControlClasses
{
    public static class UserSessionManager
    {
        public static async Task<users> Authorization(string nick, string pass)
        {
            return await DataBase.Authorization(nick, pass);
        }

        public static async Task<users> Registartion(string nick, string pass, string mail)
        {
            return await DataBase.Registartion(nick, pass, mail);
        }
        public static void SignIn(users signedUser)
        {
            DataBase.SignIn(signedUser);
        }
        public static void SignOut(users exitingUser)
        {
            DataBase.SingOut(exitingUser);
        }
        public static void UserDelete(users onDelete)
        {
            DataBase.UserDelete(onDelete);
        }
    }
}
