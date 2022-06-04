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
using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;

namespace InstaArt.Forms.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileRedact.xaml
    /// </summary>
    public partial class ProfileRedact : Page
    {
        List<users_photo> userPhotos;
        photos currentAvatar;
        string currentStatus = string.Empty;
        public ProfileRedact()
        {
            InitializeComponent();

            GetPhotos();

            Nick.Text = SessionManager.currentUser.nickname;
            Email.Text = SessionManager.currentUser.email;
            if (SessionManager.currentUser.status != null)
            {
                Status.Text = SessionManager.currentUser.status;
                Status.Style = (Style)Application.Current.FindResource("Focused");
            }
            currentAvatar = SessionManager.currentUser.photos1;
            Avatar.Fill = GUI.ViewAvatar(SessionManager.currentUser.photos1);

        }

        public async void GetPhotos()
        {
            userPhotos = await UsersManager.GetAllUserPhoto(SessionManager.currentUser.id);

            foreach (users_photo userPhoto in userPhotos)
            {
                if (userPhoto.photos.isFolder == 0)
                {
                    Image view = GUI.ViewPhoto(userPhoto.photos);
                    UserPhotoList.Children.Add(view);
                    view.MouseDown += (s, e) =>
                    {
                        Avatar.Fill = GUI.ViewAvatar(userPhoto.photos);
                        currentAvatar = userPhoto.photos;
                    };
                }
            }
        }

        private void Avatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1.5, GridUnitType.Star);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Status.Text != "Введите статус...") currentStatus = Status.Text;

            users newInfo = new users
            {
                nickname = Nick.Text,
                status = currentStatus,
                email = Email.Text,
                photos1 = currentAvatar
            };

            if (await UsersManager.ChangeUserInfo(newInfo))
            {
                SessionManager.currentProfile = new Profile(SessionManager.currentUser);
                SessionManager.MainFrame.Navigate(SessionManager.currentProfile);
                SessionManager.MainFrame.RemoveBackEntry();
            }
        }

        private void NoAvatar_Click(object sender, RoutedEventArgs e)
        {
            Avatar.Fill = GUI.ViewAvatar(null);
            currentAvatar = null;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
        }

        private void Status_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Status.Text == "")
            {
                Status.Text = "Введите статус...";
                Status.Style = (Style)FindResource("NoFocused");
            }
        }

        private void Status_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Status.Text == "Введите статус...")
            {
                Status.Text = "";
                Status.Style = (Style)FindResource("Focused");
            }
        }
    }
}
