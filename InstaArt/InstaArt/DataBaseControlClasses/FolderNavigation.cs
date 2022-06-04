using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaArt.DataBaseControlClasses
{
    public static class FolderNavigation
    {
        public static async Task<int?> GoBackFrom(int? folder)
        {
            return await DataBase.GoBackFrom(folder);
        }
    }
}
