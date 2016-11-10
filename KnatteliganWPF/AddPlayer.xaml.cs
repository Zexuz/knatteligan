using knatteligan.Domain.Entities;
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
    /// Interaction logic for AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Window
    {
        public string PlayerName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public AddPlayer()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
            this.Close();
        }

        public void searchTextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
