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
using InstaArt.DbModel;

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для GroupCreate.xaml
    /// </summary>
    public partial class GroupCreate : Window
    {
        public GroupCreate()
        {
            InitializeComponent();
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupName.Text != string.Empty)
            {
                group NewGroup = new group();
                NewGroup.name = GroupName.Text;
                NewGroup.short_descp = ShortDescription.Text;
                NewGroup.description = Describtion.Text;
                NewGroup.registration = DateTime.Now.Date;

                NewGroup = DataBase.GetContext().group.Add(NewGroup);
                DataBase.SaveChanges();

                subs FirstSub = new subs();
                FirstSub.id_user = SessionManager.currentUser.id;
                FirstSub.id_group = NewGroup.id;
                FirstSub.id_role = 4;

                DataBase.GetContext().subs.Add(FirstSub);
                DataBase.SaveChanges();

                MessageBox.Show("OK"); // сюда переход на страницу группы
                Close();
            }
            else MessageBox.Show("Введите название группы");
        }
    }
}
