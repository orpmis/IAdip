using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SessionManager.IsMyComputer = true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Nickenter.Text != "Введите ник" && Passenter.Text != "Введите пароль")
            {
                users authUser = new users();
                authUser.nickname = Nickenter.Text;
                authUser.password = Passenter.Text;

                try
                {
                    SessionManager.currentUser = await UserSessionManager.Authorization(authUser.nickname, authUser.password);

                    if (SessionManager.currentUser != null)
                    {
                        DriveAPI.InitializeUsersDrive(SessionManager.currentUser.id);
                        UserSessionManager.SignIn(SessionManager.currentUser);

                        MainForm m = new MainForm();
                        m.Show();
                        Close();
                    }
                    else MessageBox.Show("Неверный логин или пароль");
                }
                catch { MessageBox.Show("Ошибка обращения к диску, попробуйте еще раз" , "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
            else MessageBox.Show("Введите все данные");

        }

        private void Nickenter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Nickenter.Text == "Введите ник")
            {
                Nickenter.Text = "";
                Nickenter.Style = (Style)FindResource("Focused");
            }
        }

        private void Nickenter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Nickenter.Text == "")
            {
                Nickenter.Text = "Введите ник";
                Nickenter.Style = (Style)FindResource("NoFocused");
            }
        }

        private void Passenter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Passenter.Text == "")
            {
                Passenter.Text = "Введите пароль";
                Passenter.Style = (Style)FindResource("NoFocused");
            }
        }

        private void Passenter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Passenter.Text == "Введите пароль")
            {
                Passenter.Text = "";
                Passenter.Style = (Style)FindResource("Focused");
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registration r = new Registration();
            r.Show();
            Close();
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
