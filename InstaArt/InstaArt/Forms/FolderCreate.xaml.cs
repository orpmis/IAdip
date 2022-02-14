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
using System.Windows.Shapes;

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для FolderCreate.xaml
    /// </summary>
    public partial class FolderCreate : Window
    {
        private string FolderParent = string.Empty;
        private int? ParentId = null; 
        private string rootPlace = string.Empty;
        private group currentGroup = null;
        private bool groupMode = false;
        public FolderCreate()
        {
            InitializeComponent();

            rootPlace = "На страницу";
            List<photos> FoldersForSelecting = new List<photos> { new photos { name = rootPlace } };

            List<users_photo> userFolders = DataBase.GetContext().users_photo.Where(Finding => Finding.id_user == SessionManager.currentUser.id && Finding.photos.isFolder == 1).ToList();
            
            foreach (users_photo uf in userFolders)
            {
                FoldersForSelecting.Add(uf.photos);
            }

            FolderPlace.ItemsSource = FoldersForSelecting;
        }

        public FolderCreate(group thisGroup)
        {
            InitializeComponent();

            groupMode = true;
            currentGroup = thisGroup;

            rootPlace = "На главную";
            List<photos> FoldersForSelecting = new List<photos> { new photos { name = rootPlace } };

            List<group_photo> groupPhoto = DataBase.GetContext().group_photo.Where(Finding => Finding.id_group == currentGroup.id && Finding.photos.isFolder == 1).ToList();
            
            foreach (group_photo gf in groupPhoto)
            {
                FoldersForSelecting.Add(gf.photos);
            }

            FolderPlace.ItemsSource = FoldersForSelecting;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (FolderPlace.SelectedIndex == 0)
            {
                FolderParent = string.Empty;
                ParentId = null;
            }
            else
            {
                FolderParent = FolderPlace.Text;
                ParentId = (int)FolderPlace.SelectedValue;
            }

            photos NewFolder = new photos { name = FolderName.Text, address = "It's a folder", date = DateTime.Now.Date, owner = SessionManager.currentUser.id, isFolder = 1, root = ParentId };
            DataBase.GetContext().photos.Add(NewFolder);

            if (!groupMode)
            {
                DriveAPI.CreateFolder(FolderName.Text, FolderParent);

                users_photo MyPhoto = new users_photo();
                MyPhoto.id_user = SessionManager.currentUser.id;
                MyPhoto.id_photo = NewFolder.id;
                DataBase.GetContext().users_photo.Add(MyPhoto);
            }
            else
            {
                group_photo MyPhoto = new group_photo();
                MyPhoto.id_group = currentGroup.id;
                MyPhoto.id_photo = NewFolder.id;
                DataBase.GetContext().group_photo.Add(MyPhoto);
            }

            DataBase.GetContext().SaveChanges();
            Close();
        }
    }
}
