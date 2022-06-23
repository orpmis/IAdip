using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;
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

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для GroupProfile.xaml
    /// </summary>
    public partial class GroupProfile : Page
    {
        private group selectedGroup;
        private List<group_photo> groupPhotos;
        private subs subStatus;

        private List<SearchBlock> filter = new List<SearchBlock>();
        private List<SearchParametr> parametrs;

        public GroupProfile(group Group)
        {
            InitializeComponent();

            selectedGroup = Group;

            MemberInterface();

            parametrs = new List<SearchParametr>
            {
                new SearchParametr("Дата", SearchType.date),
                new SearchParametr("Название", SearchType.str)
            };

            SessionManager.currentGroup = this;
            SessionManager.currentFolder = null;

            GroupName.Text = selectedGroup.name;
            GroupShortDescription.Text = selectedGroup.short_descp;
            GroupPreview.Fill = GUI.ViewAvatar(selectedGroup.photos);

            AddSearchParametr_Click(null, null);
            filter[0].DeclareFunction(SearchAsync);

            RefreshPhotos();
        }

        public async void MemberInterface()
        {
            subStatus = await GroupManager.GetUserSubscribeRole(selectedGroup, SessionManager.currentUser);
            int roleID = (subStatus != null) ? subStatus.id_role : 0;

            switch (roleID)
            {
                case 0:
                    AddPhoto.Visibility = Visibility.Collapsed;
                    AddFolder.Visibility = Visibility.Collapsed;

                    SubscribeButton.Content = "Подписаться";
                    SubscribeButton.Click -= UnSubscribe_Click;
                    SubscribeButton.Click += SubscribeButton_Click;
                    break;

                case 1:
                    AddFolder.Visibility = Visibility.Collapsed;
                    break;

                case 2:

                    break;

                case 3:
                    break;

                case 4:
                    SubscribeButton.Visibility = Visibility.Collapsed;
                    break;
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
            if (!onlyPhotos) GroupFolderList.Children.RemoveRange(1, GroupFolderList.Children.Count - 1);
            GroupPhotoList.Children.RemoveRange(1, GroupPhotoList.Children.Count - 1);
        }
        public async void RefreshPhotos()
        {
            UpdateInterface();

            groupPhotos = await GroupManager.GetGroupPhotosInFolder(selectedGroup.id, SessionManager.currentFolder);

            foreach (group_photo groupPhoto in groupPhotos)
            {
                if (groupPhoto.photos.isFolder == 0)
                {
                    Image photoView = GUI.ViewPhoto(groupPhoto.photos);
                    GroupPhotoList.Children.Add(photoView);
                    photoView.MouseDown += (s, e) =>
                    {
                        SessionManager.MainFrame.Navigate(new PhotoShowingPage(groupPhoto.photos));
                    };
                    GroupPhotoList.Children.Add(GUI.ViewPhoto(groupPhoto.photos));
                }
                else
                {
                    Border folderView = GUI.ViewFolder(groupPhoto.photos);
                    folderView.MouseDown += (s, e) =>
                    {
                        SessionManager.currentFolder = groupPhoto.photos.id;
                        RefreshPhotos();
                    };
                    GroupFolderList.Children.Add(folderView);
                }
            }
        }

        public void RefreshPhotos(List<group_photo> viewingCollection)
        {
            UpdateInterface(true);

            foreach (group_photo groupPhoto in viewingCollection)
            {
                if (groupPhoto.photos.isFolder == 0) GroupPhotoList.Children.Add(GUI.ViewPhoto(groupPhoto.photos));
            }
        }

        private void CanInsertNewSearchParametr()
        {
            AddSearchParametr.Visibility = Visibility.Visible;
            if (filter.Count >= parametrs.Count) AddSearchParametr.Visibility = Visibility.Hidden;
        }

        private void AddSearchParametr_Click(object sender, RoutedEventArgs e)
        {
            SearchBlock newParametr = new SearchBlock(parametrs);
            newParametr.index = filter.Count;
            filter.Add(newParametr);

            SearchPart.RowDefinitions.Add(new RowDefinition());

            SearchPart.Children.Add(newParametr.mainGrid);
            Grid.SetRow(newParametr.mainGrid, SearchPart.RowDefinitions.Count - 1);

            newParametr.DeclareFunction(DeleteParametr);

            CanInsertNewSearchParametr();
        }

        private void BackButton_click(object sender, RoutedEventArgs e)
        {
            SessionManager.currentFolder = DataBase.GetContext().photos.Where(Finding => Finding.id == SessionManager.currentFolder).FirstOrDefault().root;
            RefreshPhotos();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                new UploadForm(dialog.FileName, selectedGroup);
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderCreate fc = new FolderCreate(selectedGroup);
            fc.Show();
        }

        private void RootButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.currentFolder = null;
            RefreshPhotos();
        }

        public async void SearchAsync()
        {
            List<group_photo> selected = groupPhotos;
            string name = null;
            DateTime? date = null;

            for (int i = 0; i < filter.Count; i++)
            {
                if (filter[i].GetParametr().Name == "Дата") date = filter[i].GetValue();
                if (filter[i].GetParametr().Name == "Название") name = filter[i].GetValue();
            }

            selected = await FindAndSorting.FindGroupPhotoByParametrs(selectedGroup.id, SessionManager.currentFolder, name, date, selected);

            RefreshPhotos(selected);
        }

        public void DeleteParametr(int index)
        {
            filter.RemoveAt(index);
            SearchPart.Children.RemoveAt(index);
            SearchPart.RowDefinitions.RemoveAt(index);

            for (int i = index; i < filter.Count; i++)
            {
                filter[i].index--;
            }

            CanInsertNewSearchParametr();
        }

        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            await GroupManager.AddSubscriber(selectedGroup, SessionManager.currentUser);
            MemberInterface();
        }

        private async void UnSubscribe_Click(object s, RoutedEventArgs e)
        {
            await GroupManager.RemoveSubscriber(subStatus);
            MemberInterface();
        }
    }
}
