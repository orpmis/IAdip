using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InstaArt
{
    class SessionManager
    {
        public static Frame MainFrame { get; set; }
        public static users currentUser { get; set; }
        public static Profile currentProfile { get; set; }
        public static GroupProfile currentGroup { get; set; }
        public static int? currentFolder { get; set; } = null;
        public static bool IsMyComputer { get; set; }
    }
}
