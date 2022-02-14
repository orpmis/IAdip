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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(new ListViewPage
                (
                    DataBase.GetContext().users.ToList()
                ));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(SessionManager.currentProfile);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!SessionManager.IsMyComputer)
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                credPath = System.IO.Path.Combine(credPath, "InstaArtAuthSettings", "drive-credentials.json", "User" + SessionManager.currentUser.id.ToString());

                Directory.Delete(credPath); 
            }

            //сюда еще выкл онлайн
            DataBase.GetContext().users.Attach(SessionManager.currentUser);
            SessionManager.currentUser.last_online = DateTime.Now;
            DataBase.SaveChanges();

            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(new ListViewPage
                (
                    DataBase.GetContext().group.ToList()
                ));
        }
    }
}
