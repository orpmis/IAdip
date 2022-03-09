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
        private static readonly object alock = new object();

        private DataBase() { }

        public static MyAppET2 GetContext()
        {
            if (Context == null)
            {
                lock (alock)
                {
                    if (Context == null)
                    {
                        Context = new MyAppET2();
                    }
                }
            }
            return Context;
        }
        public static void SaveChanges()
        {
            GetContext().SaveChanges();
        }
        public static async Task<users> Authorization(string nick, string pass)
        {
            return await Task.Run(() => GetContext().users.FirstOrDefault(aut => aut.nickname == nick && aut.password == pass));
        }
        public static List<users> SortUsersBy(List<users> users, string nickname)
        {
            return users.Where(finding => finding.nickname.ToLower().Contains(nickname.ToLower())).ToList();
        }

        public static List<group> SortGroupsBy(List<group> groups, string name)
        {
            return new List<group> ( groups.Where(finding => finding.name.ToLower().Contains(name.ToLower())).ToList() );
        }

        public static async Task<List<users_photo>> GetUserPhotos(int selectedUser, int? folder)
        {
            return await Task.Run(() => GetContext().users_photo.Where(Finding => Finding.id_user == selectedUser && Finding.photos.root == folder).OrderByDescending(list => list.id).ToList());
        }

        public static async Task<List<group_photo>> GetGroupPhotos(int selectedGroup, int? folder)
        {
            return await Task.Run(() => GetContext().group_photo.Where(finding => finding.id_group == selectedGroup && finding.photos.root == folder).OrderByDescending(list => list.id).ToList());
        }

        public static async Task<int?> GoBackFrom(int? folder)
        {
            if (folder != null)
            {
                return await Task.Run(() => GetContext().photos.Where(finding => finding.id == folder).FirstOrDefault().root);
            }
            else return null;
        }

        public static async Task<List<users_photo>> FindUserPhotoByName(string name, int userId, int? folder,List<users_photo> onSearch = null)
        {
            if (onSearch == null) return await Task.Run(() => GetContext().users_photo.Where(finding => finding.id_user == userId && finding.photos.name.ToLower().Contains(name.ToLower()) && finding.photos.root == folder).ToList());
            else return await Task.Run(() => onSearch.Where(finding => finding.id_user == userId && finding.photos.name.ToLower().Contains(name.ToLower()) && finding.photos.root == folder).ToList());
        }

        public static async Task<List<users_photo>> FindUserPhotoByDate(DateTime date, int userId, int? folder, List<users_photo> onSearch = null)
        {
            if(onSearch == null) return await Task.Run(() => GetContext().users_photo.Where(finding => finding.id_user == userId && finding.photos.date == date && finding.photos.root == folder).ToList());
            else return await Task.Run(() => onSearch.Where(finding => finding.id_user == userId && finding.photos.date == date && finding.photos.root == folder).ToList());
        }

    }
}
