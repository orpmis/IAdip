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
using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;

namespace InstaArt.Forms.Pages
{
    /// <summary>
    /// Логика взаимодействия для Messager.xaml
    /// </summary>
    public partial class Messager : Page
    {
        List<conversation> userConversations;
        public Messager(List<conversation> userConversationsCollection)
        {
            InitializeComponent();

            userConversations = userConversationsCollection;

            RefreshConversations();
        }

        public void RefreshConversations()
        {
            foreach (conversation conversation in userConversations)
            {
                Grid conv = GUI.ViewConversation(conversation);

                conv.MouseDown += (s, e) =>
                {
                    SelectDialog(conversation);
                };

                UserConversations.Children.Add(conv);
            }
        }

        public void SelectDialog(conversation conversation)
        {
            DialogeZone.Navigate(new DialogPage(conversation));
        }

    }
}
