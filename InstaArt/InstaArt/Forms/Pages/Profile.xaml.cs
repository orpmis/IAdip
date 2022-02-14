using Microsoft.Win32;
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

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private users SelectedUser;
        private bool hasFolders = false;

        public bool HasFolders 
        {
            get { return hasFolders; }
            set 
            {
                if (value)
                {
                    Grid.SetRow(UserPhotoList, 1);
                    Grid.SetRowSpan(UserPhotoList, 1);
                }
                else
                {
                    Grid.SetRow(UserPhotoList, 0);
                    Grid.SetRowSpan(UserPhotoList, 2);
                }

                hasFolders = value;
            }
        }
        public Profile(users User)
        {
            InitializeComponent();

            SessionManager.currentFolder = null;
            SelectedUser = User;

            Nick.Text = SelectedUser.nickname;
            Status.Text = SelectedUser.status;

            RefreshPhoto();
        }

        private void UpdateInterface()
        {
            if (SessionManager.currentFolder != null) BackButton.Visibility = Visibility.Visible;
            else BackButton.Visibility = Visibility.Hidden;
            HasFolders = false;
            UserFolderList.Children.Clear();
            UserPhotoList.Children.Clear();
        }
        public void RefreshPhoto()
        {
            UpdateInterface();

            foreach (users_photo UserPhoto in DataBase.GetContext().users_photo.Where(Finding => Finding.id_user == SelectedUser.id && Finding.photos.root == SessionManager.currentFolder).OrderByDescending(ordering => ordering.id))
            {
                if (UserPhoto.photos.isFolder == 0) UserPhotoList.Children.Add(GUI.ViewPhoto(UserPhoto.photos));
                else
                {
                    if (!HasFolders) HasFolders = true;

                    UserFolderList.Children.Add(GUI.ViewFolder(UserPhoto.photos));
                }
            }
            List<users_photo> ds = DataBase.GetContext().users_photo.Where(Finding => Finding.id_user == SelectedUser.id && Finding.photos.root == SessionManager.currentFolder).ToList();
            Console.WriteLine("AAAAAAAAAAAA THERE IS " + ds.Count);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                new UploadForm(dialog.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderCreate fc = new FolderCreate();
            fc.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SessionManager.currentFolder = DataBase.GetContext().photos.Where(Finding => Finding.id == SessionManager.currentFolder).FirstOrDefault().root;
            RefreshPhoto();
        }
    }
}
