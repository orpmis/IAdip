using InstaArt.Forms.Pages;
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
using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private users SelectedUser;
        private List<users_photo> userPhoto;

        private List<SearchBlock> filter = new List<SearchBlock>();
        private List<SearchParametr> parametrs;
        

        public Profile(users User)
        {
            InitializeComponent();

            parametrs = new List<SearchParametr>
            {
                new SearchParametr("Дата", SearchType.date),
                new SearchParametr("Название", SearchType.str)
            };

            SessionManager.currentFolder = null;
            SelectedUser = User;

            OtherProfile();

            Nick.Text = SelectedUser.nickname;
            Status.Text = SelectedUser.status;
            Avatar.Fill = GUI.ViewAvatar(User.photos1);

            AddSearchParametr_Click(null, null);
            filter[0].DeclareFunction(SearchAsync);

            RefreshContent();
        }
        public void OtherProfile()
        {
            if (SelectedUser != SessionManager.currentUser)
            {
                AddPhoto.Visibility = Visibility.Collapsed;
                AddFolder.Visibility = Visibility.Collapsed;
                RedactUserInfo.Visibility = Visibility.Collapsed;
            }
        }
        public void UpdateInterface(bool onlyPhotos = false)
        {
            if (SessionManager.currentFolder != null)
            {
                BackButton.Visibility = Visibility.Visible;
                RootButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Hidden;
                RootButton.Visibility = Visibility.Hidden;
            }
            if(!onlyPhotos) UserFolderList.Children.RemoveRange(1, UserFolderList.Children.Count - 1);
            UserPhotoList.Children.RemoveRange(1, UserPhotoList.Children.Count - 1);
        }
        public async void RefreshContent()
        {
            UpdateInterface();

            userPhoto = await UsersManager.GetUserPhotosInFolder(SelectedUser.id, SessionManager.currentFolder);

            foreach (users_photo userPhoto in userPhoto)
            {
                if (userPhoto.photos.isFolder == 0)
                {
                    Image photoView = GUI.ViewPhoto(userPhoto.photos);
                    UserPhotoList.Children.Add(photoView);
                    photoView.MouseDown += (s, e) =>
                    {
                        SessionManager.MainFrame.Navigate(new PhotoShowingPage(userPhoto.photos));
                    };
                }
                else
                {
                    Border folderView = GUI.ViewFolder(userPhoto.photos);
                        folderView.MouseDown += (s, e) =>
                        {
                            SessionManager.currentFolder = userPhoto.photos.id;
                            RefreshContent();
                        };
                    UserFolderList.Children.Add(folderView);
                }
            }
        }

        public void RefreshPhotosOnly(List<users_photo> viewingCollection)
        {
            UpdateInterface(true);

            foreach (users_photo userPhoto in viewingCollection)
            {
                if (userPhoto.photos.isFolder == 0)
                {
                    Image view = GUI.ViewPhoto(userPhoto.photos);
                    UserPhotoList.Children.Add(view);
                    view.MouseDown += (s, e) =>
                    {
                        SessionManager.MainFrame.Navigate(new PhotoShowingPage(userPhoto.photos));
                    };
                }
            }
        }

        private void CanInsertNewSearchParametr()
        {
            AddSearchParametr.Visibility = Visibility.Visible;
            if (filter.Count >= parametrs.Count) AddSearchParametr.Visibility = Visibility.Hidden;
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                new UploadForm(dialog.FileName);
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderCreate fc = new FolderCreate();
            fc.Show();
        }

        private async void BackButton_click(object sender, RoutedEventArgs e)
        {
            SessionManager.currentFolder = await FolderNavigation.GoBackFrom(SessionManager.currentFolder);
            RefreshContent();
        }

        private void RootButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.currentFolder = null;
            RefreshContent();
        }

        private void AddSearchParametr_Click(object sender, RoutedEventArgs e)
        {
            SearchBlock newParametr = new SearchBlock(parametrs);
            newParametr.index = filter.Count;
            filter.Add(newParametr);

            SearchPart.Children.Add(newParametr.mainGrid);

            newParametr.DeclareFunction(DeleteParametr);

            CanInsertNewSearchParametr();
        }

        public async void SearchAsync()
        {
            List<users_photo> selected = userPhoto;
            string name = null;
            DateTime? date = null;

            for (int i = 0; i < filter.Count; i++)
            {
                if (filter[i].GetParametr().Name == "Дата") date = filter[i].GetValue();
                if (filter[i].GetParametr().Name == "Название") name = filter[i].GetValue();
            }

            selected = await FindAndSorting.FindUserPhotoByParametrs(SelectedUser.id, SessionManager.currentFolder, name, date, selected);

            RefreshPhotosOnly(selected);
        }

        public void DeleteParametr(int index)
        {
            filter.RemoveAt(index);
            SearchPart.Children.RemoveAt(index);

            for (int i = index; i < filter.Count; i++)
            {
                filter[i].index--;
            }

            CanInsertNewSearchParametr();
        }

        private void RedactUserInfo_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.MainFrame.Navigate(new ProfileRedact());
        }
    }
}
