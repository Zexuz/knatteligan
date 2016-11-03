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

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for ManageLeague.xaml
    /// </summary>
    public partial class ManageLeague : Window
    {
        public ManageLeague()
        {
            InitializeComponent();
        }
        private void MainWindow_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
