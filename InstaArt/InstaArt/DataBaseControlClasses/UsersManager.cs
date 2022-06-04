using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaArt.DbModel;

namespace InstaArt.DataBaseControlClasses
{
    public static class UsersManager
    {
        public static async Task<bool> ChangeUserInfo(users newInfo)
        {
            return await DataBase.ChangeUserInfo(newInfo);
        }
        public static async Task<List<users>> GetAllUsers()
        {
            return await DataBase.GetAllUsers();
        }
        public static async Task<List<users_photo>> GetAllUserPhoto(int selectedUser)
        {
            return await DataBase.GetAllUserPhoto(selectedUser);
        }

        public static async Task<List<users_photo>> GetUserPhotosInFolder(int selectedUser, int? folder)
        {
            return await DataBase.GetUserPhotosInFolder(selectedUser, folder);
        }

        public static async Task<List<conversation>> GetAllUsersConversations(users selectedUser)
        {
            return await DataBase.GetAllUsersConversations(selectedUser);
        }
    }
}
