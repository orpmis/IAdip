using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;
using InstaArt.Forms.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        
        public MainForm()
        {
            InitializeComponent();

            SessionManager.MainFrame = MainPage;

            SessionManager.currentProfile = new Profile(SessionManager.currentUser);
            MainPage.Navigate(SessionManager.currentProfile);

        }

        private async void GoToUserList_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(new ListViewPage
                (
                    await UsersManager.GetAllUsers()
                ));
        }

        private void GoToProfile_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.currentProfile = new Profile(SessionManager.currentUser);
            MainPage.Navigate(SessionManager.currentProfile);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (!SessionManager.IsMyComputer)
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                credPath = System.IO.Path.Combine(credPath, "InstaArtAuthSettings", "drive-credentials.json", "User" + SessionManager.currentUser.id.ToString());

                Directory.Delete(credPath); 
            }

            UserSessionManager.SignOut(SessionManager.currentUser);

            Application.Current.Shutdown();
        }

        private async void GoToGroupList_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(new ListViewPage
                (
                    await GroupManager.GetAllGroups()
                ));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.MainFrame.CanGoBack)
            {
                SessionManager.MainFrame.GoBack();
            }
        }

        private void MainPage_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!SessionManager.MainFrame.CanGoBack)
            {
                BackButton.Visibility = Visibility.Hidden;
            }
            else BackButton.Visibility = Visibility.Visible;
        }

        private async void GoToMessages_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(new Messager
                (
                await UsersManager.GetAllUsersConversations(SessionManager.currentUser)
                ));
        }

        private void HideWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void SetFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
