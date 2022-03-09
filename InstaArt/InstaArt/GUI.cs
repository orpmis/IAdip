using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InstaArt
{
    public class GUI
    {
        public static Image ViewPhoto(photos Photo)
        {
            Image ImgViewer = new Image();

            Uri uri = new Uri(Photo.address);
            BitmapImage img = new BitmapImage(uri);
            ImgViewer.Source = img;
            ImgViewer.Margin = new Thickness(15);
            ImgViewer.Height = 200;
            ImgViewer.Width = 200;
            ImgViewer.Stretch = System.Windows.Media.Stretch.UniformToFill;

            ImgViewer.MouseDown += (s, e) =>
            {
                ShowPhotoForm View = new ShowPhotoForm(Photo);
                View.Show();
            };

            return ImgViewer;
        }

        public static Border ViewFolder(photos Folder)
        {
            StackPanel folderView = new StackPanel();
            //folderView.Margin = new Thickness(15);
            folderView.Height = 30;
            folderView.Width = 150;
            folderView.Orientation = Orientation.Horizontal;
            folderView.Background = new SolidColorBrush(Color.FromArgb(50, 255, 255,255));

            Border border = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromArgb(80, 75, 75, 75)),
                BorderThickness = new Thickness(3),
                Margin = new Thickness(15),
                Padding = new Thickness(5)
            };

            Image folderIco = new Image();
            folderIco.Source = new BitmapImage(new Uri("./Resources/FolderIcon.png", UriKind.Relative));
            folderIco.Margin = new Thickness(15);
            folderIco.Height = 30;
            folderIco.Width = 30;
            folderIco.Stretch = System.Windows.Media.Stretch.UniformToFill;

            TextBlock folderName = new TextBlock();
            folderName.Text = Folder.name;
            folderName.FontSize = 15;
            folderName.Foreground = new SolidColorBrush(Color.FromRgb(100,100,100));
            folderName.VerticalAlignment = VerticalAlignment.Center;
            folderName.HorizontalAlignment = HorizontalAlignment.Left;

            folderView.Children.Add(folderIco);
            folderView.Children.Add(folderName);
            border.Child = folderView;

            folderView.MouseDown += (s, e) =>
            {
                SessionManager.currentFolder = Folder.id;
                SessionManager.currentProfile.RefreshPhotos();
            };

            return border;
        }

        public static Grid ViewUser(users ViewingUser)
        {
            Grid newUser = new Grid();
            newUser.Height = 150;

            Border mainBorder = new Border
            {
                BorderThickness = new Thickness(1),
                Margin = new Thickness(8, 2, 8, 2),
                CornerRadius = new CornerRadius(4)
            };

            StackPanel mainPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 0, 0, 0)
            };

            Ellipse avatarView = new Ellipse
            {
                Height = 100,
                Width = 100
            };

            ImageBrush avatarBrush = ViewAvatar(ViewingUser.avatar);
            if (avatarBrush != null) avatarView.Fill = avatarBrush;
            else avatarView.Fill = new SolidColorBrush(Color.FromRgb(156, 56, 58));

            StackPanel insidePanel = new StackPanel { Margin = new Thickness(10,0,0,0)};

            Label nickLabel = new Label 
            {
                Content = ViewingUser.nickname,
                FontSize = 40
            };
            Label statusLabel = new Label
            {
                Content = ViewingUser.status,
                Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
                FontSize = 15
            };

            newUser.Children.Add(mainBorder);
            mainBorder.Child = mainPanel;
            mainPanel.Children.Add(avatarView);
            mainPanel.Children.Add(insidePanel);
            insidePanel.Children.Add(nickLabel);
            insidePanel.Children.Add(statusLabel);

            newUser.MouseEnter += (s, e) =>
            {
                newUser.Background = new SolidColorBrush(Color.FromArgb(30, 100, 100, 255));
            };

            newUser.MouseLeave += (s, e) =>
            {
                newUser.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            };

            newUser.MouseDown += (s, e) =>
            {
                SessionManager.MainFrame.Navigate(new Profile(ViewingUser));
            };

            return newUser;
        }

        public static Grid ViewGroup(group ViewingGroup)
        {
            Grid newGroup = new Grid();
            newGroup.Height = 350;

            RowDefinition r = new RowDefinition { Height = new GridLength(3, GridUnitType.Star) };
            newGroup.RowDefinitions.Add(r);
            newGroup.RowDefinitions.Add(new RowDefinition());

            Image avatarView = new Image();

            if (ViewingGroup.preview != null)
            {
                photos Avatar = DataBase.GetContext().photos.Where(Finding => Finding.id == ViewingGroup.preview).FirstOrDefault();
                if (Avatar != null) avatarView.Source = new BitmapImage(new Uri(Avatar.address));
                else MessageBox.Show("avatar has been deleted"); //заглушку
            }
            //else MessageBox.Show("No avatar");

            Label nameLabel = new Label
            {
                Content = ViewingGroup.name,
                FontSize = 35,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            newGroup.Children.Add(avatarView);
            newGroup.Children.Add(nameLabel);

            Grid.SetRow(nameLabel, 1);

            newGroup.MouseEnter += (s, e) =>
            {
                newGroup.Background = new SolidColorBrush(Color.FromArgb(30, 100, 100, 255));
            };

            newGroup.MouseLeave += (s, e) =>
            {
                newGroup.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            };

            newGroup.MouseDown += (s, e) =>
            {
                SessionManager.MainFrame.Navigate(new GroupProfile(ViewingGroup));
            };

            return newGroup;
        }

        public static ImageBrush ViewAvatar(int? AvatarID)
        {
            if (AvatarID != null)
            {
                photos Avatar = DataBase.GetContext().photos.Where(Finding => Finding.id == AvatarID).FirstOrDefault();
                if (Avatar != null)
                    return new ImageBrush { ImageSource = new BitmapImage(new Uri(Avatar.address)) };
                else return null; //сюда заглушку
            }
            else return null;
        }


    }
}
