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
        private group SelectedGroup;
        private bool hasFolders = false;

        public bool HasFolders
        {
            get { return hasFolders; }
            set
            {
                if (value)
                {
                    Grid.SetRow(GroupPhotoList, 1);
                    Grid.SetRowSpan(GroupPhotoList, 1);
                }
                else
                {
                    Grid.SetRow(GroupPhotoList, 0);
                    Grid.SetRowSpan(GroupPhotoList, 2);
                }

                hasFolders = value;
            }
        }
        public GroupProfile(group Group)
        {
            InitializeComponent();

            SessionManager.currentGroup = this;
            SessionManager.currentFolder = null;

            SelectedGroup = Group;

            GroupName.Text = SelectedGroup.name;
            GroupShortDescription.Text = SelectedGroup.short_descp;

            RefreshPhoto();
        }

        private void UpdateInterface()
        {
            if (SessionManager.currentFolder != null) BackButton.Visibility = Visibility.Visible;
            else BackButton.Visibility = Visibility.Hidden;
            HasFolders = false;
            GroupFolderList.Children.Clear();
            GroupPhotoList.Children.Clear();
        }
        public void RefreshPhoto()
        {
            UpdateInterface();

            foreach (group_photo GroupPhoto in DataBase.GetContext().group_photo.Where(Finding => Finding.id_group == SelectedGroup.id && Finding.photos.root == SessionManager.currentFolder).OrderBy(ordering => ordering.id))
            {
                if (GroupPhoto.photos.isFolder == 0) GroupPhotoList.Children.Add(GUI.ViewPhoto(GroupPhoto.photos));
                else
                {
                    if (!HasFolders) HasFolders = true;

                    GroupFolderList.Children.Add(GUI.ViewFolder(GroupPhoto.photos));
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.currentFolder = DataBase.GetContext().photos.Where(Finding => Finding.id == SessionManager.currentFolder).FirstOrDefault().root;
            RefreshPhoto();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                new UploadForm(dialog.FileName, SelectedGroup);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderCreate fc = new FolderCreate(SelectedGroup);
            fc.Show();
        }
    }
}
