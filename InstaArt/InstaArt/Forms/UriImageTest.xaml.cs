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
    /// Логика взаимодействия для UriImageTest.xaml
    /// </summary>
    public partial class UriImageTest : Window
    {
        public UriImageTest()
        {
            InitializeComponent();


            MyImg.Source = new BitmapImage(new Uri("images/FolderIcon.png", UriKind.Relative));
            //Uri uri = new Uri("https://intuit.ru/EDI/18_07_20_2/1595024415-12778/tutorial/1010/objects/6/files/l01_02.png");
            //BitmapImage img = new BitmapImage(uri);
            //MyImg.Source = img;

        }
    }
}
