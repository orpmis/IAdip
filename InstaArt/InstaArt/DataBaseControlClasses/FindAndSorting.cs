using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaArt.DbModel;

namespace InstaArt.DataBaseControlClasses
{
    public static class FindAndSorting
    {
        public static async Task<List<users_photo>> FindUserPhotoByParametrs(int userId, int? folder, string photoName = "", DateTime? needDate = null, List<users_photo> onSearch = null)
        {
            onSearch = await DataBase.FindUserPhotoByName(photoName, userId, folder, onSearch);
            onSearch = await DataBase.FindUserPhotoByDate(needDate, userId, folder, onSearch);

            return onSearch;
        }

        public static async Task<List<group_photo>> FindGroupPhotoByParametrs(int groupId, int? folder, string photoName = "", DateTime? needDate = null, List<group_photo> onSearch = null)
        {
            onSearch = await DataBase.FindGroupPhotoByName(photoName, groupId, folder, onSearch);
            onSearch = await DataBase.FindGroupPhotoByDate(needDate, groupId, folder, onSearch);

            return onSearch;
        }

        public static async Task<List<users>> SortUsersByName(List<users> users, string nickname)
        {
            return await Task.Run(
                () =>
                users.
                Where(finding =>
                finding.nickname.ToLower().
                Contains(nickname.ToLower())).
                ToList()
            );
        }

        public static async Task<List<group>> SortGroupsByName(List<group> groups, string name)
        {
            return await Task.Run(
                () =>
                groups.
                Where(finding =>
                finding.name.ToLower().
                Contains(name.ToLower())).
                ToList()
                );
        }


    }
}
