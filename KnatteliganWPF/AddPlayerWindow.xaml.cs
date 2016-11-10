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
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        public Player Player { get; set; }

        public PersonName PlayerName { get; set; }
        public PersonalNumber SocialSecurityNumber { get; set; }
        public AddPlayerWindow()
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

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
            Player = new Player(PlayerName, SocialSecurityNumber);
            DialogResult = true;
            Close();


        }
    }
}
