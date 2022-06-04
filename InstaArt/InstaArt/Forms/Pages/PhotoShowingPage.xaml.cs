using InstaArt.DataBaseControlClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using InstaArt.DbModel;

namespace InstaArt.Forms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PhotoShowingPage.xaml
    /// </summary>
    public partial class PhotoShowingPage : Page
    {
        likes usersLike;
        photos selectedPhoto;
        List<comments> comments;
        comments currentComment;
        public PhotoShowingPage(photos Photo)
        {
            InitializeComponent();

            selectedPhoto = Photo;

            PhotoName.Text = Photo.name;
            AnImage.Source = new BitmapImage(new Uri(Photo.address));
            Describtion.Text = Photo.description;

            RefreshLikeImage();
        }

        private async void RefreshLikeImage()
        {
            Task<likes> oper = PhotoManager.GetUsersLike(SessionManager.currentUser.id, selectedPhoto.id);

            usersLike = await oper;


            if (usersLike != null) await Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
            {
                LikeButton.Source = new BitmapImage(new Uri("/images/heart_filled.png", UriKind.Relative));
            });
            else
                await Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
                {
                    LikeButton.Source = new BitmapImage(new Uri("/images/heart.png", UriKind.Relative));
                });

        }

        private async void LikeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (usersLike != null)
            {
                await PhotoManager.RemoveLike(usersLike);
            }
            else
            {
                await PhotoManager.AddLike(SessionManager.currentUser.id, selectedPhoto.id);
            }
            RefreshLikeImage();
        }

        //private async Task<List<comments>> RefreshComments()
        //{
        //       return await DataBase.GetPhotoComments(selectedPhoto.id); ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ ПЕРЕДЕЛАТЬ
        //}

        private async void ShowComments(bool refresh = false)
        {
            if (comments == null || refresh)
            {
                CommentsList.Children.Clear();
                var proc = PhotoManager.GetAllComments(selectedPhoto.id); // заменить на имитацию при загрузке нового?
                using (proc)
                {
                   comments = await proc;
                }

                foreach (comments com in comments)
                {
                    Grid newCom = GUI.ViewCommet(com, RedactComment, DeleteComment);
                    CommentsList.Children.Add(newCom);
                }
            }

        }
        private void RedactComment(comments com)
        {
            SetSendMode.Visibility = Visibility.Visible;

            CommentInputBox.Text = com.message;
            currentComment = com;

            CommentUpload.Content = "Save";
            CommentUpload.Click -= CommentUpload_Click;
            CommentUpload.Click += CommentSave_Click;
        }
        private async Task<bool> SaveCommentRedaction(comments com)
        {
            return await PhotoManager.RedactComment(currentComment, CommentInputBox.Text);
        }

        private async void CommentSave_Click(object sender, RoutedEventArgs e)
        {
            await SaveCommentRedaction(currentComment);
            ShowComments(true);

            SetSendMode_Click(null, null);
        }

        private async void DeleteComment(comments com)
        {
            MessageBoxResult res = MessageBox.Show("Вы точно хотите удалить коммаентарий?","Удаление",MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                await PhotoManager.DeleteComment(com);
                ShowComments(true);
            }
        }
        private void CommentsButton_Up(object sender, MouseButtonEventArgs e)
        {
            ShowComments();

            ContentField.ColumnDefinitions[2].Width = new GridLength(0.3, GridUnitType.Star);

            CommentsButton.MouseDown -= CommentsButton_Up;
            CommentsButton.MouseDown += CommentsButton_Down;
        }

        private void CommentsButton_Down(object sender, MouseButtonEventArgs e)
        {
            ContentField.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);

            CommentsButton.MouseDown -= CommentsButton_Down;
            CommentsButton.MouseDown += CommentsButton_Up;
        }

        private async void CommentUpload_Click(object sender, RoutedEventArgs e)
        {
            comments newComment = new comments
            {
                id_user = SessionManager.currentUser.id,
                id_photo = selectedPhoto.id,
                message = CommentInputBox.Text,
                date = DateTime.Now
            };

            newComment = await PhotoManager.UploadNewComment(newComment);

            if (newComment != null)
            {
                CommentsList.Children.Insert(0, GUI.ViewCommet(newComment, RedactComment, DeleteComment));

                CommentInputBox.Text = string.Empty;
            }
            else MessageBox.Show("Ошибка при загрузке комментария");
        }

        private void DescriptionButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLength actual = ContentField.ColumnDefinitions[0].Width;
            if (actual.Value == 0)
                ContentField.ColumnDefinitions[0].Width = new GridLength(250, GridUnitType.Pixel);
            else
                ContentField.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
        }

        private void SetSendMode_Click(object sender, RoutedEventArgs e)
        {
            CommentInputBox.Text = string.Empty;
            CommentUpload.Content = "Send";
            CommentUpload.Click -= CommentSave_Click;
            CommentUpload.Click += CommentUpload_Click;

            SetSendMode.Visibility = Visibility.Collapsed;
        }
    }
}
