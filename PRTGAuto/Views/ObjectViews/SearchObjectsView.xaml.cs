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

namespace PRTGAuto.Views.ObjectViews
{
    /// <summary>
    /// Логика взаимодействия для SearchObjectsView.xaml
    /// </summary>
    public partial class SearchObjectsView : Page
    {
        public static SearchObjectsView Instance;
        public SearchObjectsView()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
