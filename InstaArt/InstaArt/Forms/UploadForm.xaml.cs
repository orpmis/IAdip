using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для UploadForm.xaml
    /// </summary>
    public partial class UploadForm : Window
    {
        private string MimeType = string.Empty;
        private string FilePath = string.Empty;
        private string FileParent = string.Empty;
        private int? ParentId = null;
        private string rootPlace = string.Empty;
        private group currentGroup = null; 

        public UploadForm(string Path)
        {
            InitializeComponent();

            FilePath = Path;

            if (IsExtentValid(System.IO.Path.GetExtension(FilePath)))
            {
                Show();

                Uri uri = new Uri(FilePath);
                BitmapImage img = new BitmapImage(uri);
                PreviewImage.Source = img;

                rootPlace = "На страницу";
                List<photos> FoldersForSelecting = new List<photos> { new photos { name = rootPlace } };

                List<users_photo> UserFolders = DataBase.GetContext().users_photo.Where(Finding => Finding.id_user == SessionManager.currentUser.id && Finding.photos.isFolder == 1).ToList();

                foreach (users_photo uf in UserFolders)
                {
                    FoldersForSelecting.Add(uf.photos);
                }

                PhotoPlace.ItemsSource = FoldersForSelecting;

                UploadButton.Click += UploadButton_ToUser;
            }
            else Close();
        }

        public UploadForm(string Path, group thisGroup)
        {
            InitializeComponent();

            FilePath = Path;
            currentGroup = thisGroup;
            if (IsExtentValid(System.IO.Path.GetExtension(FilePath)))
            {
                Show();

                Uri uri = new Uri(FilePath);
                BitmapImage img = new BitmapImage(uri);
                PreviewImage.Source = img;

                rootPlace = "На главную";
                List<photos> FoldersForSelecting = new List<photos> { new photos { name = rootPlace } };

                List<group_photo> UserFolders = DataBase.GetContext().group_photo.Where(Finding => Finding.id_group == currentGroup.id && Finding.photos.isFolder == 1).ToList();

                foreach (group_photo uf in UserFolders)
                {
                    FoldersForSelecting.Add(uf.photos);
                }

                PhotoPlace.ItemsSource = FoldersForSelecting;

                UploadButton.Click += UploadButton_ToGroup;
            }
            else Close();
        }



        private void UploadButton_ToUser(object sender, RoutedEventArgs e)
        {
            if (PhotoPlace.SelectedIndex == 0)
            {
                FileParent = string.Empty;
                ParentId = null;
            }
            else
            {
                FileParent = PhotoPlace.Text;
                ParentId = (int)PhotoPlace.SelectedValue;
            }

            string NewPhotoUri = DriveAPI.Upload(FilePath, System.IO.Path.GetFileName(FilePath), MimeType, FileParent);
            
            photos NewPhoto = new photos{name=PhotoName.Text,description = PhotoDescriprion.Text, address = NewPhotoUri, date=DateTime.Now.Date, owner=SessionManager.currentUser.id, isFolder = 0, root = ParentId };
            DataBase.GetContext().photos.Add(NewPhoto);

            users_photo MyPhoto = new users_photo();
            MyPhoto.id_user = SessionManager.currentUser.id;
            MyPhoto.id_photo = NewPhoto.id;
            DataBase.GetContext().users_photo.Add(MyPhoto);

            DataBase.GetContext().SaveChanges();

            SessionManager.currentProfile.RefreshPhoto();

            Close();
        }

        private void UploadButton_ToGroup(object sender, RoutedEventArgs e)
        {
            if (PhotoPlace.SelectedIndex == 0)
            {
                FileParent = "GroupsPhotoHeap";
                ParentId = null;
            }
            else
            {
                FileParent = PhotoPlace.Text;
                ParentId = (int)PhotoPlace.SelectedValue;
            }

            string NewPhotoUri = DriveAPI.Upload(FilePath, System.IO.Path.GetFileName(FilePath), MimeType, FileParent);

            photos NewPhoto = new photos { name = PhotoName.Text, description = PhotoDescriprion.Text, address = NewPhotoUri, date = DateTime.Now.Date, owner = SessionManager.currentUser.id, isFolder = 0, root = ParentId };
            DataBase.GetContext().photos.Add(NewPhoto);

            group_photo groupPhotos = new group_photo();
            groupPhotos.id_group = currentGroup.id;
            groupPhotos.id_photo = NewPhoto.id;
            DataBase.GetContext().group_photo.Add(groupPhotos);

            DataBase.GetContext().SaveChanges();

            SessionManager.currentGroup.RefreshPhoto();

            Close();
        }
        private bool IsExtentValid(string extent)
        {
            switch (extent)
            {
                case ".jpg":
                    MimeType = "image/jpeg";
                    return true;
                case ".jpeg":
                    MimeType = "image/jpeg";
                    return true;
                case ".png":
                    MimeType = "image/png";
                    return true;
                default:
                    MessageBox.Show("Недопустимый формат файла");
                    return false;
            }
        }


    }
}
