using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaArt.DbModel;

namespace InstaArt.DataBaseControlClasses
{
    internal class MessagerManager
    {
        public static async Task<List<messages>> GetAllConversationMessages(conversation selectedConversation)
        {
            return await DataBase.GetAllConversationMessages(selectedConversation);
        }
        public static async Task<messages> UploadNewMessage(messages newMessage)
        {
            return await DataBase.UploadNewMessage(newMessage);
        }
    
        public static async Task<bool> RedactMessage(messages redactingMessage, string newMessage)
        {
            return await DataBase.RedactMessage(redactingMessage, newMessage);
        }

        public static async Task<bool> DeleteMessage(List<messages> onDelete)
        {
            return await DataBase.DeleteMessage(onDelete);
        }
    }
}
