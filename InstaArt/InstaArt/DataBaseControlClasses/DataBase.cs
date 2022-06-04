using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InstaArt.DbModel;

namespace InstaArt
{
    public sealed class DataBase
    {
        private static inArEntities Context;
        private static readonly object alock = new object();

        private DataBase() { }

        public static inArEntities GetContext()
        {
            if (Context == null)
            {
                lock (alock)
                {
                    if (Context == null)
                    {
                        Context = new inArEntities();
                    }
                }
            }
            return Context;
        }

        public static void SaveChanges()
        {
            GetContext().SaveChangesAsync();
        }
        public static async Task<users> Authorization(string nick, string pass)
        {
            try
            {
                return await Task.Run(() => GetContext().users.FirstOrDefault(aut => aut.nickname == nick && aut.password == pass));
            }
            catch
            {
                MessageBox.Show("Произошла ошибка обращения к базе, попробуйте еще раз");
                return null;
            }
        }

        public static async Task<users> Registartion(string nick, string pass, string mail)
        {
            users reg = await Task.Run(
            () => GetContext().users.Add(new users
            {
                nickname = nick,
                password = pass,
                email = mail,
                registration = DateTime.Now
            }));

            if (reg != null)
            {
                SaveChanges();
                return reg;
            }
            else return null;
        }
        public static void UserDelete(users onDelete)
        {
            GetContext().
                users.
                Attach(onDelete);

            GetContext().
            users.
            Remove(onDelete);

            SaveChanges();
        }
        public static void SignIn(users signedUser)
        {
            GetContext().
                users.
                Attach(signedUser);

            signedUser.isOnline = 1;

            SaveChanges();
        }
        public static void SingOut(users exitingUser)
        {
            GetContext().users.
                Attach(exitingUser);
            exitingUser.last_online = DateTime.Now;
            SaveChanges();
        }

        public static async Task<bool> IsNicknameFree(string nick)
        {
            bool flag = await Task.Run(() => GetContext().users.FirstOrDefault(someuser => someuser.nickname == nick) is null ? true : false);
            return flag;
        }

        public static async Task<bool> IsMailFree(string mail)
        {
            bool flag = await Task.Run(() => GetContext().users.FirstOrDefault(someuser => someuser.email == mail) is null ? true : false);
            return flag;
        }
        public static async Task<bool> ChangeUserInfo(users newInfo)
        {
            try
            {
                await Task.Run
                    (
                    () => GetContext().users.Attach(SessionManager.currentUser)
                    );
                SessionManager.currentUser.nickname = newInfo.nickname;
                SessionManager.currentUser.email = newInfo.email;
                SessionManager.currentUser.status = newInfo.status;
                SessionManager.currentUser.photos1 = newInfo.photos1;
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public static async Task<List<users>> GetAllUsers()
        {
            return await Task.Run(
                () =>
                GetContext().users.
                ToList()
                );
        }
        public static async Task<List<users_photo>> GetAllUserPhoto(int selectedUser)
        {
            return await Task.Run(
                () => GetContext().
                users_photo.
                Where(Finding => Finding.id_user == selectedUser
                && Finding.photos.isFolder == 0).
                OrderByDescending(ord => ord.id).
                ToList()
                );
        }
        public static async Task<List<users_photo>> GetUserPhotosInFolder(int selectedUser, int? folder)
        {
            await Task.Delay(200);
            return await Task.Run(
                () => GetContext().
                users_photo.
                Where(Finding => Finding.id_user == selectedUser
                && Finding.photos.root == folder).
                OrderByDescending(ord => ord.id).
                ToList()
                );
        }

        public static async Task<List<group>> GetAllGroups()
        {
            return await Task.Run(
                () =>
                GetContext().group.
                ToList()
                );
        }
        public static async Task<List<group_photo>> GetGroupPhotosInFolder(int selectedGroup, int? folder)
        {
            return await Task.Run(
                () => GetContext().
                group_photo.
                Where(finding => finding.id_group == selectedGroup
                && finding.photos.root == folder).
                OrderByDescending(ord => ord.id).
                ToList()
                );
        }

        public static async Task<int?> GoBackFrom(int? folder)
        {
            if (folder != null)
            {
                return await Task.Run(
                    () =>
                    GetContext().
                    photos.
                    Where(finding =>
                    finding.id == folder).
                    FirstOrDefault().
                    root);
            }
            else return null;
        }

        public static async Task<List<users_photo>> FindUserPhotoByName(string name, int userId, int? folder, List<users_photo> onSearch = null)
        {
            if (name != string.Empty && name != null)
            {
                if (onSearch == null)
                    return await Task.Run(
                        () => GetContext().
                        users_photo.
                        Where(finding =>
                        finding.id_user == userId
                        && finding.photos.name.
                        ToLower().
                        Contains(name.ToLower())
                        && finding.photos.root == folder).
                        ToList());
                else
                    return await Task.Run(
                    () =>
                    onSearch.
                    Where(finding =>
                    finding.id_user == userId
                    && finding.photos.name.
                    ToLower().
                    Contains(name.ToLower())
                    && finding.photos.root == folder).
                    ToList());
            }
            else return onSearch;
        }

        public static async Task<List<users_photo>> FindUserPhotoByDate(DateTime? date, int userId, int? folder, List<users_photo> onSearch = null)
        {
            if (date != null)
            {
                if (onSearch == null)
                    return await Task.Run(
                        () =>
                        GetContext().
                        users_photo.
                        Where(finding =>
                        finding.id_user == userId
                        && finding.photos.date == date
                        && finding.photos.root == folder).
                        ToList());
                else
                    return await Task.Run(
                        () =>
                        onSearch.
                        Where(finding =>
                        finding.id_user == userId
                        && finding.photos.date == date
                        && finding.photos.root == folder).
                        ToList());
            }
            else return onSearch;
        }

        public static async Task<List<group_photo>> FindGroupPhotoByName(string name, int groupId, int? folder, List<group_photo> onSearch = null)
        {
            if (name != string.Empty && name != null)
            {
                if (onSearch == null)
                    return await Task.Run(
                        () => GetContext().
                        group_photo.
                        Where(finding => finding.id_group == groupId
                        && finding.photos.name.
                        ToLower().
                        Contains(name.ToLower())
                        && finding.photos.root == folder).
                        ToList());

                else
                    return await Task.Run(
                        () => onSearch.
                        Where(finding => finding.id_group == groupId
                        && finding.photos.name.
                        ToLower().
                        Contains(name.ToLower())
                        && finding.photos.root == folder).
                        ToList());
            }
            else return onSearch;
        }

        public static async Task<List<group_photo>> FindGroupPhotoByDate(DateTime? date, int groupId, int? folder, List<group_photo> onSearch = null)
        {
            if (date != null)
            {
                if (onSearch == null)
                    return await Task.Run(
                        () =>
                        GetContext().
                        group_photo.
                        Where(finding =>
                        finding.id_group == groupId
                        && finding.photos.date == date
                        && finding.photos.root == folder).
                        ToList()
                        );
                else
                    return await Task.Run(
                    () =>
                    onSearch.
                    Where(finding =>
                    finding.id_group == groupId
                    && finding.photos.date == date
                    && finding.photos.root == folder).
                    ToList()
                    );
            }
            else return onSearch;
        }

        public static async Task<likes> GetUsersLike(int userId, int photoId)
        {
            await Task.Delay(200);

            using (
                var ope = new Task<likes>(
                () => GetContext().likes.
                Where(finding =>
                finding.id_user == userId &&
                finding.id_photo == photoId).
                FirstOrDefault())
                  )
            {
                likes like;
                ope.Start();
                like = await ope;
                return like;
            }
        }

        public static async Task<bool> AddLike(int userId, int photoId)
        {
            likes newLike = new likes
            {
                id_photo = photoId,
                id_user = userId
            };

            var adding = Task.Run(() => GetContext().likes.Add(newLike));

            if (await adding != null)
            {
                SaveChanges();
                return true;
            }
            else return false;
        }

        public static async Task<bool> RemoveLike(likes aLike)
        {
            await Task.Run(() => GetContext().likes.Attach(aLike));
            var removing = Task.Run(() => GetContext().likes.Remove(aLike));

            if (await removing != null)
            {
                SaveChanges();
                return true;
            }
            else return false;
        }

        public static async Task<List<comments>> GetAllComments(int photoID)
        {
            await Task.Delay(200);
            var proc = Task.Run(
                () =>
                GetContext().
                comments.
                Where(finding => finding.id_photo == photoID).
                OrderByDescending(ord => ord.id).
                ToList()
                );
            List<comments> coms = await proc;
            proc.Dispose();
            return coms;
        }

        public static async Task<comments> UploadNewComment(comments newComment)
        {
            comments uploadingComment = await Task.Run(
                () =>
                GetContext().
                comments.
                Add(newComment)
                );

            if (uploadingComment != null)
            {
                SaveChanges();
                return uploadingComment;
            }
            else return null;
        }

        public static async Task<bool> RedactComment(comments redactingComment, string newMessage)
        {
            try
            {
                await Task.Run(
                    () =>
                    GetContext().
                    comments.
                    Attach(redactingComment)
                    );
                redactingComment.message = newMessage;

                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static async Task<bool> DeleteComment(comments onDelete)
        {
            try
            {
                await Task.Run(
                    () =>
                    GetContext().
                    comments.
                    Attach(onDelete)
                    );

                await Task.Run(
                    () =>
                    GetContext().
                    comments.
                    Remove(onDelete)
                    );

                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static async Task<List<conversation>> GetAllUsersConversations(users selectedUser)
        {
            return await Task.Run
                (
                () => GetContext()
                .conversation
                .Where(c => c.conversation_members.FirstOrDefault().id_member == selectedUser.id)
                .ToList()
                );
        }

        public static async Task<messages> UploadNewMessage(messages newMessage)
        {
            messages uploadingMessage = await Task.Run(
                () =>
                GetContext().
                messages.
                Add(newMessage)
                );

            if (uploadingMessage != null)
            {
                SaveChanges();
                return uploadingMessage;
            }
            else return null;
        }

        public static async Task<bool> RedactMessage(messages redactingMessage, string newMessage)
        {
            try
            {
                await Task.Run(
                    () =>
                    GetContext().
                    messages.
                    Attach(redactingMessage)
                    );
                redactingMessage.message = newMessage;

                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static async Task<bool> DeleteMessage(List<messages> onDelete)
        {
            try
            {
                await Task.Run(
                    () =>
                    GetContext().
                    messages.
                    RemoveRange(onDelete)
                    );

                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static async Task<List<messages>> GetAllConversationMessages(conversation selectedConversation)
        {
            await Task.Delay(200);
            return await Task.Run
                (
                () => GetContext()
                .messages
                .Where(m => m.id_conversation == selectedConversation.id)
                .ToList()
                );
        }

        
    }
}
