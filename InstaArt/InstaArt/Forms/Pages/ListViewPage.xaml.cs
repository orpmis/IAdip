using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;

namespace InstaArt
{
    /// <summary>
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class ListViewPage : Page
    {
        private List<users> viewingUsers;
        private List<group> viewingGroups;
        private bool isStandartCollection = true;
        public ListViewPage(List<users> usersCollection)
        {
            InitializeComponent();

            viewingUsers = usersCollection;
            RefreshItemsList(usersCollection);
            SearchButton.MouseDown += SearchButton_SearchUsers;
            CreateGroup.Visibility = Visibility.Hidden;
        }

        public ListViewPage(List<group> groupsCollection)
        {
            InitializeComponent();

            viewingGroups = groupsCollection;
            RefreshItemsList(groupsCollection);
            SearchButton.MouseDown += SearchButton_SearchGroups;
        }

        public void RefreshItemsList(List<users> usersCollection)
        {
            ItemList.Children.Clear();

            foreach (users u in usersCollection)
            {
                Grid newUser = GUI.ViewUser(u);
                ItemList.Children.Add(newUser);

            }
        }

        public void RefreshItemsList(List<group> groupsCollection)
        {
            ItemList.Children.Clear();

            foreach (group g in groupsCollection)
            {
                RowDefinition r = new RowDefinition();
                Grid newGroup = GUI.ViewGroup(g);
                ItemList.Children.Add(newGroup);

            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            GroupCreate g = new GroupCreate();
            g.Show();
        }

        private async void SearchButton_SearchUsers(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != string.Empty)
            {
                isStandartCollection = false;
                RefreshItemsList(
                    await FindAndSorting.SortUsersByName(viewingUsers, NameBox.Text)
                    ); 
            }
            else if (!isStandartCollection) RefreshItemsList(viewingUsers);
        }

        private async void SearchButton_SearchGroups(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != string.Empty)
            {
                isStandartCollection = false;
                RefreshItemsList(
                    await FindAndSorting.SortGroupsByName(viewingGroups, NameBox.Text)
                    );
            }
            else if (!isStandartCollection)
            {
                RefreshItemsList(viewingGroups);
                isStandartCollection = true;
            }
            
        }
    }


}
