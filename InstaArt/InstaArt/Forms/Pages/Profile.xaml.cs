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

            Nick.Text = SelectedUser.nickname;
            Status.Text = SelectedUser.status;

            AddSearchParametr_Click(null, null);
            filter[0].DeclareFunction(SearchAsync);

            RefreshPhotos();
        }

        private void UpdateInterface(bool onlyPhotos = false)
        {
            if (SessionManager.currentFolder != null) BackButton.Visibility = Visibility.Visible;
            else BackButton.Visibility = Visibility.Hidden;
            if(!onlyPhotos) UserFolderList.Children.RemoveRange(1, UserFolderList.Children.Count - 1);
            UserPhotoList.Children.RemoveRange(1, UserPhotoList.Children.Count - 1);
        }
        public async void RefreshPhotos()
        {
            UpdateInterface();

            userPhoto = await DataBase.GetUserPhotos(SelectedUser.id, SessionManager.currentFolder);

            foreach (users_photo userPhoto in userPhoto)
            {
                if (userPhoto.photos.isFolder == 0) UserPhotoList.Children.Add(GUI.ViewPhoto(userPhoto.photos));
                else
                {
                    UserFolderList.Children.Add(GUI.ViewFolder(userPhoto.photos));
                }
            }
            List<users_photo> ds = DataBase.GetContext().users_photo.Where(Finding => Finding.id_user == SelectedUser.id && Finding.photos.root == SessionManager.currentFolder).ToList();
            Console.WriteLine("AAAAAAAAAAAA THERE IS " + ds.Count);
        }

        public void RefreshPhotos(List<users_photo> viewingCollection)
        {
            UpdateInterface(true);

            foreach (users_photo userPhoto in viewingCollection)
            {
                if (userPhoto.photos.isFolder == 0) UserPhotoList.Children.Add(GUI.ViewPhoto(userPhoto.photos));
            }
        }

        private void CanInsertNewSearchParametr()
        {
            AddSearchParametr.Visibility = Visibility.Visible;
            if (SearchPart.Children.Count >= parametrs.Count+1) AddSearchParametr.Visibility = Visibility.Hidden;
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
            SessionManager.currentFolder = DataBase.GoBackFrom(SessionManager.currentFolder).Result;
            RefreshPhotos();
        }

        private void AddSearchParametr_Click(object sender, RoutedEventArgs e)
        {
            CanInsertNewSearchParametr();

            SearchBlock newParametr = new SearchBlock(parametrs);
            filter.Add(newParametr);

            SearchPart.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(AddSearchParametr, SearchPart.RowDefinitions.Count - 1);

            SearchPart.Children.Add(newParametr.mainGrid);
            Grid.SetRow(newParametr.mainGrid, SearchPart.RowDefinitions.Count-2);
        }

        public async void SearchAsync()
        {
            List<users_photo> selected = userPhoto;
            string name = null;
            DateTime? date = null;

            for (int i = 0; i < SearchPart.Children.Count - 1; i++)
            {
                if (filter[i].GetParametr().name == "Дата") date = filter[i].GetValue();
                if (filter[i].GetParametr().name == "Название") name = filter[i].GetValue();
            }

            if (date != null) selected = await DataBase.FindUserPhotoByDate(date.Value, SelectedUser.id, SessionManager.currentFolder, selected);
            if(name != null) selected = await DataBase.FindUserPhotoByName(name, SelectedUser.id, SessionManager.currentFolder, selected);

            RefreshPhotos(selected);
        }
    }
}
