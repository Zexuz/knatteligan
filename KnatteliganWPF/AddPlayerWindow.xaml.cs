using knatteligan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;
using knatteligan.Repositories;
using MahApps.Metro.Controls;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : MetroWindow
    {
        public Player Player { get; set; }
        public PersonName PlayerName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }

        public AddPlayerWindow(bool isEdit, List<Player> playersFromTeam)
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

            if (playersFromTeam == null)
                playersFromTeam = new List<Player>();
            var freePlayers = PersonRepository.GetInstance().GetAllPlayers().Where(x => x.HasTeam.Equals(false));
            var playersFromTeamId = playersFromTeam.Select(p => p.Id).ToList();
            var availablePlayers  = freePlayers.Where(player => !playersFromTeamId.Contains(player.Id)).ToList();

            if (isEdit)
            {
                FreePlayersList.Visibility = Visibility.Hidden;
                FreeAgentsHeader.Visibility = Visibility.Hidden;
            }
                
            FreePlayersList.ItemsSource = availablePlayers;

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
                var result = MessageBox.Show("Are you sure you want to cancel?", "Message", MessageBoxButton.YesNo);
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


        private void FreePlayersList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Player = (Player)FreePlayersList.SelectedItem;
            DialogResult = true;
            Close();
        }
    }
}
