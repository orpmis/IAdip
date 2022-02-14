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
            SearchButton.Click += SearchButton_SearchUsers;
        }

        public ListViewPage(List<group> groupsCollection)
        {
            InitializeComponent();

            viewingGroups = groupsCollection;
            RefreshItemsList(groupsCollection);
            SearchButton.Click += SearchButton_SearchGroups;
        }

        public void RefreshItemsList(List<users> usersCollection)
        {
            ItemList.Children.Clear();
            ItemList.RowDefinitions.Clear();
            foreach (users u in usersCollection)
            {
                RowDefinition r = new RowDefinition();
                ItemList.RowDefinitions.Add(r);
                Grid newUser = GUI.ViewUser(u);
                ItemList.Children.Add(newUser);
                Grid.SetRow(newUser, ItemList.RowDefinitions.Count - 1);

            }
        }

        public void RefreshItemsList(List<group> groupsCollection)
        {
            ItemList.Children.Clear();
            ItemList.RowDefinitions.Clear();
            foreach (group g in groupsCollection)
            {
                RowDefinition r = new RowDefinition();
                ItemList.RowDefinitions.Add(r);
                Grid newGroup = GUI.ViewGroup(g);
                ItemList.Children.Add(newGroup);
                Grid.SetRow(newGroup, ItemList.RowDefinitions.Count - 1);

            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            GroupCreate g = new GroupCreate();
            g.Show();
        }

        private void SearchButton_SearchUsers(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != string.Empty)
            {
                isStandartCollection = false;
                RefreshItemsList(DataBase.SortUsersBy(viewingUsers, NameBox.Text)); 
            }
            else if (!isStandartCollection) RefreshItemsList(viewingUsers);
        }

        private void SearchButton_SearchGroups(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != string.Empty)
            {
                isStandartCollection = false;
                RefreshItemsList(DataBase.SortGroupsBy(viewingGroups, NameBox.Text));
            }
            else if (!isStandartCollection)
            {
                RefreshItemsList(viewingGroups);
                isStandartCollection = true;
            }
            
        }
    }


}
