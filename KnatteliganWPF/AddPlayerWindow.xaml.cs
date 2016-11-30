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

        public AddPlayerWindow(bool isEdit)
        {
            InitializeComponent();

            if (isEdit)
            {
                SaveEditBtn.Visibility = Visibility.Visible;
                AddPlayerBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                SaveEditBtn.Visibility = Visibility.Hidden;
                AddPlayerBtn.Visibility = Visibility.Visible;
            }


            DataContext = this;
        }

        private void AddPlayerWindowActivated(object sender, EventArgs e)
        {
            if (PersonalNumber != null)
            {
                PersonalNumberTextBox.Text = PersonalNumber.ToString();
            }
        }

        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            if (PersonNameTextBox.Text.Length > 0 || PersonalNumberTextBox.Text.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Message", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else Close();
        }

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
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
