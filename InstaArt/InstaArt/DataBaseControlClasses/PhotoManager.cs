using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaArt.DbModel;

namespace InstaArt.DataBaseControlClasses
{
    public static class PhotoManager
    {
        public static async Task<string> DeletePhoto(photos onDelete)
        {
            return await DataBase.DeletePhoto(onDelete);
        }
        public static async Task<likes> GetUsersLike(int userId, int photoId)
        {
            return await DataBase.GetUsersLike(userId, photoId);
        }

        public static async Task<bool> AddLike(int userId, int photoId)
        {
            return await DataBase.AddLike(userId, photoId);
        }

        public static async Task<bool> RemoveLike(likes aLike)
        {
            return await DataBase.RemoveLike(aLike);
        }

        public static async Task<List<comments>> GetAllComments(int photoID)
        {
            return await DataBase.GetAllComments(photoID);
        }
        public static async Task<comments> UploadNewComment(comments newComment)
        {
            return await DataBase.UploadNewComment(newComment);
        }

        public static async Task<bool> RedactComment(comments redactingComment, string newMessage)
        {
            return await DataBase.RedactComment(redactingComment, newMessage);
        }
        public static async Task<bool> DeleteComment(comments onDelete)
        {
            return await DataBase.DeleteComment(onDelete);
        }
    }
}
