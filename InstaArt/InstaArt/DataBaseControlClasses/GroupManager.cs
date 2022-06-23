using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaArt.DbModel;

namespace InstaArt.DataBaseControlClasses
{
    public static class GroupManager
    {
        public static async Task<List<group>> GetAllGroups()
        {
            return await DataBase.GetAllGroups();
        }
        public static async Task<List<group_photo>> GetGroupPhotosInFolder(int selectedGroup, int? folder)
        {
            return await DataBase.GetGroupPhotosInFolder(selectedGroup, folder);
        }

        public static async Task<subs> GetUserSubscribeRole(group selectedGroup, users selectedUser)
        {
            return await DataBase.GetUserSubscribeRole(selectedGroup, selectedUser);
        }

        public static async Task<bool> AddSubscriber(group selectedGroup, users selectedUser)
        {
            return await DataBase.AddSubscriber(selectedGroup, selectedUser);
        }

        public static async Task<bool> RemoveSubscriber(subs selectedSub)
        {
            return await DataBase.RemoveSubscriber(selectedSub);
        }
    }
}
