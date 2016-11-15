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
        public PersonalNumber PersonalNumber { get; set; }

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

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
            //TODO: PersonalNumber not set
            var text = PersonalNumberTextBox.Text;

            //PersonalNumber =  PersonalNumber.ConvertStringToPersonalNumber(text);
            Player = new Player(PlayerName, PersonalNumber);
            DialogResult = true;
            Close();
        }

    }
}
