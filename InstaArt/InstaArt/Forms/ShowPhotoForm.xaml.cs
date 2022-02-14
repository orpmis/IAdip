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
    /// Логика взаимодействия для ShowPhotoForm.xaml
    /// </summary>
    public partial class ShowPhotoForm : Window
    {
        public ShowPhotoForm(photos Photo)
        {
            InitializeComponent();

            PhotoName.Text = Photo.name;
            AnImage.Source = new BitmapImage(new Uri(Photo.address));
            Describtion.Text = Photo.description;
        }
    }
}
