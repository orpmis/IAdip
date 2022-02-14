using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaArt
{
     public sealed class DataBase
    {
        private static MyAppET2 Context;

        private DataBase() { }

        public static MyAppET2 GetContext()
        {
            if (Context == null)
            {
                Context = new MyAppET2();
            }
            return Context;
        }

        public static void SaveChanges()
        {
            GetContext().SaveChanges();
        }

        public static List<users> SortUsersBy(List<users> users, string nickname)
        {
            return users.Where(finding => finding.nickname.ToLower().Contains(nickname.ToLower())).ToList();
        }

        public static List<group> SortGroupsBy(List<group> groups, string name)
        {
            return new List<group> ( groups.Where(finding => finding.name.ToLower().Contains(name.ToLower())).ToList() );
        }
    }
}
