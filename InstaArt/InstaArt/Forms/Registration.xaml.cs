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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            MessageBox.Show("Перед регистрацией аккаунта зайдите в свою учетную запись гугл в браузере");
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (Nick.Text != "Введите ник" && Email.Text != "Введите почту" && Pass.Text != "Введите пароль" && Repeatpass.Text != "Повторите пароль")
            {
                if (Pass.Text == Repeatpass.Text)
                {
                    users reguser = new users();
                    reguser.nickname = Nick.Text;
                    reguser.email = Email.Text;
                    reguser.password = Pass.Text;
                    reguser.registration = DateTime.Now.Date;


                    if (IsNicknameFree(reguser.nickname))
                    {
                        if (IsMailFree(reguser.email))
                        {
                            MessageBox.Show("Подтвердите доступ к аккаунту для приложения");

                            try
                            {
                                DataBase.GetContext().users.Add(reguser);
                                DataBase.GetContext().SaveChanges();

                                DriveAPI.RegistrateCredential
                                (
                                DataBase.GetContext().users.Where(Finding => Finding.nickname == reguser.nickname && Finding.password == reguser.password).FirstOrDefault().id
                                );

                                MessageBox.Show("Регистрация прошла успешно");

                                MainWindow a = new MainWindow();
                                a.Show();
                                Close();
                            }
                            catch (Exception ex) { MessageBox.Show("Доступ не был предоставлен"); }

                        }
                        else MessageBox.Show("Указанная почта уже зарегистрирована");
                    }
                    else MessageBox.Show("Никнейм занят, придумайте другой");
                    
                    
                }
                else MessageBox.Show("Пароли не совпадают");
            }
            else MessageBox.Show("Введите все данные");
        }

        private bool IsNicknameFree(string nick)
        {
            bool flag = DataBase.GetContext().users.FirstOrDefault(someuser => someuser.nickname == nick) is null ? true : false;
            return flag;
        }

        private bool IsMailFree(string mail)
        {
            bool flag = DataBase.GetContext().users.FirstOrDefault(someuser => someuser.email == mail) is null ? true : false;
            return flag;
        }

        private void Nick_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Nick.Text == "Введите ник")
            {
                Nick.Text = "";
                Nick.Style = (Style)FindResource("Focused");
            }
        }

        private void Nick_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Nick.Text == "")
            {
                Nick.Text = "Введите ник";
                Nick.Style = (Style)FindResource("NoFocused");
            }
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "Введите почту")
            {
                Email.Text = "";
                Email.Style = (Style)FindResource("Focused");
            }
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "")
            {
                Email.Text = "Введите почту";
                Email.Style = (Style)FindResource("NoFocused");
            }
        }

        private void Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == "Введите пароль")
            {
                Pass.Text = "";
                Pass.Style = (Style)FindResource("Focused");
            }
        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == "")
            {
                Pass.Text = "Введите пароль";
                Pass.Style = (Style)FindResource("NoFocused");
            }
        }

        private void Repeatpass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Repeatpass.Text == "Повторите пароль")
            {
                Repeatpass.Text = "";
                Repeatpass.Style = (Style)FindResource("Focused");
            }
        }

        private void Repeatpass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Repeatpass.Text == "")
            {
                Repeatpass.Text = "Повторите пароль";
                Repeatpass.Style = (Style)FindResource("NoFocused");
            }
        }
    }
}
