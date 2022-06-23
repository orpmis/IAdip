using InstaArt.Forms.Pages;
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
using InstaArt.DbModel;

namespace InstaArt
{
    public class GUI
    {
        private static SolidColorBrush mouseEnteringColor = new SolidColorBrush(Color.FromArgb(30, 100, 100, 255));
        public static Image ViewPhoto(photos Photo)
        {
            Image ImgViewer = new Image();

            Uri uri = new Uri(Photo.address);
            BitmapImage img = new BitmapImage(uri);
            ImgViewer.Source = img;
            ImgViewer.Margin = new Thickness(15);
            ImgViewer.Height = 200;
            ImgViewer.Width = 200;
            ImgViewer.Stretch = Stretch.UniformToFill;

            //ImgViewer.MouseDown += (s, e) =>
            //{
            //    SessionManager.MainFrame.Navigate(new PhotoShowingPage(Photo));
            //};

            return ImgViewer;
        }

        public static Border ViewFolder(photos Folder)
        {
            StackPanel folderView = new StackPanel
            {
                Height = 40,
                Width = 150,
                Orientation = Orientation.Horizontal,
                Background = Brushes.Transparent
            };
            
            Border border = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                BorderThickness = new Thickness(3),
                Margin = new Thickness(15),
            };

            Image folderIco = new Image
            {
                Source = new BitmapImage(new Uri("/images/folderIcon.png", UriKind.Relative)),
                Stretch = Stretch.Uniform,
                Margin = new Thickness(0,0,0,0),
                Height = 40,
                Width = 40,
                VerticalAlignment = VerticalAlignment.Center
            };

            TextBlock folderName = new TextBlock
            {
                Text = Folder.name,
                FontSize = 15,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextTrimming = TextTrimming.CharacterEllipsis
            };

            folderView.Children.Add(folderIco);
            folderView.Children.Add(folderName);
            border.Child = folderView;

            return border;
        }

        public static Grid ViewUser(users viewingUser)
        {
            Grid newUser = new Grid();
            newUser.Height = 150;
            newUser.HorizontalAlignment = HorizontalAlignment.Stretch;

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

            avatarView.Fill = ViewAvatar(viewingUser.photos1);

            StackPanel insidePanel = new StackPanel { Margin = new Thickness(10,0,0,0)};

            Label nickLabel = new Label 
            {
                Content = viewingUser.nickname,
                FontSize = 40,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            Label statusLabel = new Label
            {
                Content = viewingUser.status,
                FontSize = 15,
                Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220))
            };

            newUser.Children.Add(mainBorder);
            mainBorder.Child = mainPanel;
            mainPanel.Children.Add(avatarView);
            mainPanel.Children.Add(insidePanel);
            insidePanel.Children.Add(nickLabel);
            insidePanel.Children.Add(statusLabel);

            newUser.MouseEnter += (s, e) =>
            {
                newUser.Background = mouseEnteringColor;
            };

            newUser.MouseLeave += (s, e) =>
            {
                newUser.Background = Brushes.Transparent;
            };

            newUser.MouseDown += (s, e) =>
            {
                SessionManager.MainFrame.Navigate(new Profile(viewingUser));
            };

            return newUser;
        }

        public static Grid ViewGroup(group viewingGroup)
        {
            Grid newGroup = new Grid();
            newGroup.Height = 350;

            RowDefinition r = new RowDefinition { Height = new GridLength(3, GridUnitType.Star) };
            newGroup.RowDefinitions.Add(r);
            newGroup.RowDefinitions.Add(new RowDefinition());

            Rectangle avatarView = new Rectangle
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            
            avatarView.Fill = ViewAvatar(viewingGroup.photos);

            Label nameLabel = new Label
            {
                Content = viewingGroup.name,
                FontSize = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };

            newGroup.Children.Add(avatarView);
            newGroup.Children.Add(nameLabel);

            Grid.SetRow(nameLabel, 1);

            newGroup.MouseEnter += (s, e) =>
            {
                newGroup.Background = mouseEnteringColor;
            };

            newGroup.MouseLeave += (s, e) =>
            {
                newGroup.Background = Brushes.Transparent;
            };

            newGroup.MouseDown += (s, e) =>
            {
                SessionManager.MainFrame.Navigate(new GroupProfile(viewingGroup));
            };

            return newGroup;
        }

        public static ImageBrush ViewAvatar(photos avatar)
        {
                if (avatar != null)
                return new ImageBrush 
                {
                    ImageSource = new BitmapImage(new Uri(avatar.address)),
                    Stretch = Stretch.UniformToFill
                };
                else 
                return new ImageBrush 
                { 
                    ImageSource = new BitmapImage(new Uri("./images/nonAvatar.png", UriKind.Relative)), 
                    Stretch = Stretch.Uniform 
                }; 
        }

        public static Grid ViewCommet(comments ViewingComment, Action<comments> actionOnRedact, Action<comments> actionOnDelete)
        {
            Grid newComment = new Grid();

            //newComment.Height = 100;
            newComment.RowDefinitions.Add(new RowDefinition());
            newComment.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            newComment.RowDefinitions.Add(new RowDefinition());
            newComment.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(60, GridUnitType.Pixel) });
            newComment.ColumnDefinitions.Add(new ColumnDefinition());

            newComment.MouseEnter += (s, e) =>
            {
                newComment.Background = mouseEnteringColor;
            };

            newComment.MouseLeave += (s, e) =>
            {
                newComment.Background = Brushes.Transparent;
            };

            Ellipse avatarView = new Ellipse
            {
                Height = 50,
                Width = 50
            };

            avatarView.Fill = ViewAvatar(ViewingComment.users.photos1);

            TextBlock nickLabel = new TextBlock
            {
                Text = ViewingComment.users.nickname,
                FontSize = 20,
                Margin = new Thickness(0,20,0,0),
                Foreground = Brushes.White
            };

            TextBlock messageLabel = new TextBlock
            {
                Text = ViewingComment.message,
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.White
            };

            Label dateLabel = new Label
            {
                Content = ViewingComment.date,
                FontSize = 10,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0,0,10,0),
                Foreground = Brushes.White
            };

            if (ViewingComment.users.id == SessionManager.currentUser.id)
            {
                WrapPanel buttonsPanel = new WrapPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                Button redactCommentButton = new Button
                {
                    Width = 20,
                    Height = 20,
                    Content = "🖉",
                    FontSize = 10,
                    Margin = new Thickness(0,0,0,0),
                    Style = (Style)App.Current.FindResource("RoundedFunctionalButton")
                };

                redactCommentButton.Click += (s, e) => { actionOnRedact(ViewingComment); };

                Button deleteCommentButton = new Button
                {
                    Width = 20,
                    Height = 20,
                    Content = "X",
                    FontSize = 10,
                    Margin = new Thickness(10, 0, 5, 0),
                    Style = (Style)App.Current.FindResource("RoundedFunctionalButton")
                };

                deleteCommentButton.Click += (s, e) => { actionOnDelete(ViewingComment); };

                buttonsPanel.Children.Add(redactCommentButton);
                buttonsPanel.Children.Add(deleteCommentButton);

                newComment.Children.Add(buttonsPanel);

                Grid.SetColumn(buttonsPanel, 1);
                Grid.SetRow(buttonsPanel, 0);
            }

            newComment.Children.Add(avatarView);
            newComment.Children.Add(nickLabel);
            newComment.Children.Add(messageLabel);
            newComment.Children.Add(dateLabel);

            Grid.SetRowSpan(avatarView, 3);
            Grid.SetColumn(nickLabel, 1);

            Grid.SetColumn(messageLabel, 1);
            Grid.SetRow(messageLabel, 1);

            Grid.SetColumn(dateLabel, 1);
            Grid.SetRow(dateLabel, 2);

            return newComment;
        }

        public static Grid ViewConversation(conversation viewingConversation)
        {
            string lastMes = string.Empty;
            string convName = string.Empty;

            Grid newConversation = new Grid();
            newConversation.Height = 100;

            newConversation.RowDefinitions.Add(new RowDefinition());
            newConversation.RowDefinitions.Add(new RowDefinition());

            newConversation.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Pixel)});
            newConversation.ColumnDefinitions.Add(new ColumnDefinition());

            Ellipse avatarView = new Ellipse
            {
                Height = 80,
                Width = 80
            };

            if (viewingConversation.conversation_members.Count > 2)
            {
                // funcs for group convers
            }
            else
            {
                users companion = viewingConversation.conversation_members.
                    Where(f => f.id_member != SessionManager.currentUser.id).
                    FirstOrDefault().users;

                avatarView.Fill = ViewAvatar(companion.photos1);

                convName = viewingConversation.name;
                if (convName == viewingConversation.id_creator + "&" + companion.id || convName == companion.id + "&" + viewingConversation.id_creator)
                {
                    convName = companion.nickname;
                }

                if (viewingConversation.messages.Count > 0)
                {
                    if (viewingConversation.messages.Last().id_sender == SessionManager.currentUser.id)
                    {
                        lastMes = "Вы: ";
                    }
                    lastMes += viewingConversation.messages.Last().message;
                }
            }

            TextBlock nameLabel = new TextBlock
            {
                Text = convName,
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10,0,0,0),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };

            TextBlock lastMessage = new TextBlock
            {
                Text = lastMes,
                FontSize = 15,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextTrimming = TextTrimming.CharacterEllipsis,
                Margin = new Thickness(10, 0, 0, 0),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };

            newConversation.Children.Add(nameLabel);
            Grid.SetColumn(nameLabel, 1);

            newConversation.Children.Add(lastMessage);
            Grid.SetRow(lastMessage, 1);
            Grid.SetColumn(lastMessage, 1);

            newConversation.Children.Add(avatarView);
            Grid.SetRowSpan(avatarView, 2);

            return newConversation;
        }

        public static Grid ViewMessage(messages viewingMessage, bool isFirst)
        {
            Grid newMessage = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            newMessage.MouseEnter += (s, e) =>
            {
                newMessage.Background = mouseEnteringColor;
            };

            newMessage.MouseLeave += (s, e) =>
            {
                newMessage.Background = Brushes.Transparent;
            };

            Border messageBorder = new Border
            {
                MaxWidth = 250,
                Margin = new Thickness(5, 15, 5, 5),
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Top
            };

            SolidColorBrush backgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x2E, 0x5E, 0xB8));//273168

            StackPanel messageOutPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Ellipse avatarView = null;

            TextBlock messageText = new TextBlock
            {
                Text = viewingMessage.message,
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Color.FromRgb(255,255,255))
            };

            if (isFirst && viewingMessage.users != null)
            {
                avatarView = new Ellipse
                {
                    Height = 50,
                    Width = 50,
                    VerticalAlignment = VerticalAlignment.Top
                };

                avatarView.Fill = ViewAvatar(viewingMessage.users.photos1);
            }

            if (viewingMessage.id_sender != SessionManager.currentUser.id)
            {
                backgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xCC, 0x4E, 0x2B)); //9D7F35
                messageBorder.HorizontalAlignment = HorizontalAlignment.Left;
                messageOutPanel.HorizontalAlignment = HorizontalAlignment.Left;
                if (avatarView != null)
                    messageOutPanel.Children.Add(avatarView);
                else
                    messageBorder.Margin = new Thickness(55, 15, 5, 5);
                messageOutPanel.Children.Add(messageBorder);
            }
            else
            {
                messageOutPanel.HorizontalAlignment = HorizontalAlignment.Right;
                messageBorder.HorizontalAlignment = HorizontalAlignment.Right;
                messageOutPanel.Children.Add(messageBorder);
                if (avatarView != null)
                    messageOutPanel.Children.Add(avatarView);
                else
                    messageBorder.Margin = new Thickness(5, 15, 55, 5);
            }

            messageBorder.Background = backgroundColor;

            messageBorder.Child = messageText;

            newMessage.Children.Add(messageOutPanel);

            return newMessage;
        }
    }
}
