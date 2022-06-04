using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using InstaArt.DbModel;

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для ShowPhotoForm.xaml
    /// </summary>
    public partial class ShowPhotoForm : Window
    {
        likes usersLike;
        photos selectedPhoto;
        List<comments> comments;
        
        public ShowPhotoForm(photos Photo)
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
                usersLike = await DataBase.GetUsersLike(SessionManager.currentUser.id, selectedPhoto.id);

                if (usersLike != null) await Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action) delegate
                {
                    LikeButton.Source = new BitmapImage(new Uri("/images/heart_filled.png", UriKind.Relative)); 
                });
                else 
                await Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action) delegate
                {
                    LikeButton.Source = new BitmapImage(new Uri("/images/heart.png", UriKind.Relative));
                });
            
        }

        private async void LikeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (usersLike != null)
            {
                await DataBase.RemoveLike(usersLike);
            }
            else
            {
                await DataBase.AddLike(SessionManager.currentUser.id, selectedPhoto.id);
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
                var proc = DataBase.GetAllComments(selectedPhoto.id);
                comments = await proc;
                proc.Dispose();
            }

            foreach (comments com in comments)
            {
                CommentsList.RowDefinitions.Add(new RowDefinition());
                //Grid newCom = GUI.ViewCommet(com);
              //  CommentsList.Children.Add(newCom);
               // Grid.SetRow(newCom, CommentsList.RowDefinitions.Count - 1);
            }
        }

        private void CommentsButton_Up(object sender, MouseButtonEventArgs e)
        {
            ShowComments();

            Content.ColumnDefinitions[1].Width = new GridLength(0.5, GridUnitType.Star);

            CommentsButton.MouseDown -= CommentsButton_Up;
            CommentsButton.MouseDown += CommentsButton_Down;
        }

        private void CommentsButton_Down(object sender, MouseButtonEventArgs e)
        {
            Content.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Pixel);

            CommentsButton.MouseDown -= CommentsButton_Down;
            CommentsButton.MouseDown += CommentsButton_Up;
        }

        private async void CommentUpload_Click(object sender, RoutedEventArgs e)
        {
            comments newComment = new comments
            {
                id_user = SessionManager.currentUser.id,
                id_photo = selectedPhoto.id,
                //message = CommentInputBox.Text,
                date = DateTime.Now
            };

           // if (await DataBase.UploadNewComment(newComment))
           // {
                ShowComments(true);
            //}
           // else MessageBox.Show("Ошибка при загрузке комментария");
        }
    }
}
