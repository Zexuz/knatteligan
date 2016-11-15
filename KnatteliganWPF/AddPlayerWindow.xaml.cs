using knatteligan.Domain.Entities;
using System;
using System.Windows;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;

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

        private void AddPlayerWindowActivated(object sender, EventArgs e)
        {
            //TODO: fuck, is this really needed?
            if (PersonalNumber != null)
            {
                PersonalNumberTextBox.Text = PersonalNumber.ToString();
            }
        }

        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
            this.Close();
        }

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
            //TODO: fuck, is this really needed?
            if (PersonNameTextBox.Text != string.Empty)
            {
                var str = PersonalNumberTextBox.Text;
                PersonalNumber = ConvertHelper.ConvertStringToPersonalNumber(str);
            }

            Player = new Player(PlayerName, PersonalNumber);
            DialogResult = true;
            Close();
        }

        private void SaveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var str = PersonalNumberTextBox.Text;
            PersonalNumber = ConvertHelper.ConvertStringToPersonalNumber(str);

            Player.Name = PlayerName;
            Player.PersonalNumber = PersonalNumber;
            DialogResult = true;
            Close();
        }
    }
}
