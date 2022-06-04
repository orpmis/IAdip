using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaArt.DataBaseControlClasses
{
    public static class UniqueDataControl
    {
        public static async Task<bool> IsNicknameFree(string nick)
        {
            return await DataBase.IsNicknameFree(nick);
        }

        public static async Task<bool> IsMailFree(string mail)
        {
            return await DataBase.IsMailFree(mail);
        }
    }
}
