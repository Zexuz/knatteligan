using knatteligan.Domain.Entities;
using System;
using System.Windows;
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
