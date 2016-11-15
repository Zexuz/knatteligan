using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public Team Team { get; set; }
        public Coach Coach { get; set; }
        public ObservableCollection<Player> Players { get; set; }
        public TeamName TeamName { get; set; }
        public PersonName PersonName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email EmailAddress { get; set; }

        private readonly PersonService _personService;
        

        public AddTeamWindow()
        {
            InitializeComponent();
            _personService = new PersonService();
            if (Players == null)
            {
                Players = new ObservableCollection<Player>();
            }
            PlayerList.ItemsSource = Players;
            DataContext = this;
        }

        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow();
            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (addPlayerResult.HasValue && !addPlayerResult.Value) {
                Trace.WriteLine("we did not press the add buttom");
                return;
            }

            _personService.Add(addPlayerWindow.Player);
            Players.Add(addPlayerWindow.Player);
        }

        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            Coach = new Coach(PersonName, PersonalNumber, PhoneNumber, EmailAddress);
            Team = new Team(TeamName, Players, Coach);

            DialogResult = true;
            Close();
        }

        private void PlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemovePlayerBtn.IsEnabled = true;
            EditPlayerBtn.IsEnabled = true;
        }

        private void AddTeamWindowActivated(object sender, EventArgs e)
        {
            PlayerList.ItemsSource = Players;
        }

        private void SaveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            Team.Name = TeamName;
            DialogResult = true;
            Close();
        }

        private void RemovePlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            var player = (Player)PlayerList.SelectedItem;
            _personService.RemovePlayer(player.Id);
            Players.Remove(player);
        }

        private void EditPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            var player = (Player)PlayerList.SelectedItem;

            //TODO: PersonalNumber not set
            var addPlayerWindow = new AddPlayerWindow
            {
                Player = player,
                PlayerName = player.Name,
                PersonalNumber = player.PersonalNumber
            }; 

            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (!addPlayerResult.HasValue) return;

            _personService.Edit(addPlayerWindow.Player, addPlayerWindow.PlayerName, addPlayerWindow.PersonalNumber);
            //TODO: Replace this hack
            Players.Remove(addPlayerWindow.Player);
            Players.Add(addPlayerWindow.Player);
            
        }
    }
}
