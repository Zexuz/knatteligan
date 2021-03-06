﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;
using knatteligan.Services;
using MahApps.Metro.Controls;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : MetroWindow
    {
        public Team Team { get; set; }
        public Coach Coach { get; set; }
        public ObservableCollection<Player> Players { get; set; }
        public TeamName TeamName { get; set; }
        public PersonName PersonName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }

        private readonly PersonService _personService;

        public AddTeamWindow(bool isEdit)
        {
            InitializeComponent();

            if (isEdit)
            {
                SaveEditBtn.Visibility = Visibility.Visible;
                AddTeamBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                SaveEditBtn.Visibility = Visibility.Hidden;
                AddTeamBtn.Visibility = Visibility.Visible;
            }

            _personService = new PersonService();
            Players = new ObservableCollection<Player>();
            DataContext = this;
            if (TeamNameTxt.Text.Equals(string.Empty))
            {
            }
        }

        private void AddTeamWindowActivated(object sender, EventArgs e)
        {
            PlayerList.ItemsSource = Players;
            if (PersonalNumber != null)
            {
                PersonalNumberTextBox.Text = PersonalNumber.ToString();
            }
        }

        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var playerList = PlayerList.Items.Cast<Player>().ToList();
            var addPlayerWindow = new AddPlayerWindow(false, playerList);
            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (addPlayerResult.HasValue && !addPlayerResult.Value)
            {
                Trace.WriteLine("we did not press the add buttom");
                return;
            }

            var newPlayer = addPlayerWindow.Player;

            var playerAlreadyExists = _personService.FindPlayerById(newPlayer.Id) != null;

            if (!playerAlreadyExists)
                _personService.Add(newPlayer);
            Players.Add(addPlayerWindow.Player);
        }

        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            if (TeamNameTxt.Text.Length > 0 || CoachNameTextBox.Text.Length > 0 || PersonalNumberTextBox.Text.Length > 0 ||
                PhoneNumberTextBox.Text.Length > 0 || CoachEmailTextBox.Text.Length > 0)
            {
                var result = MessageBox.Show("Are you sure you want to cancel?", "Message",
                    MessageBoxButton.YesNo);
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

        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var str = PersonalNumberTextBox.Text;
            PersonalNumber = ConvertHelper.ConvertStringToPersonalNumber(str);

            Coach = new Coach(PersonName, PersonalNumber, PhoneNumber, Email);
            Team = new Team(TeamName, Players, Coach);
            foreach (var item in PlayerList.Items)
            {
                var player = (Player)item;
                player.HasTeam = true;
            }
            DialogResult = true;
            Close();
        }

        private void SaveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var str = PersonalNumberTextBox.Text;
            PersonalNumber = ConvertHelper.ConvertStringToPersonalNumber(str);

            var coach = _personService.FindCoachById(Team.CoachId);
            coach.PersonalNumber = PersonalNumber;
            coach.Name = PersonName;
            coach.PhoneNumber = PhoneNumber;
            coach.Email = Email;
            Coach = coach;

            Team.Name = TeamName;
            Team.CoachId = Coach.Id;
            Team.PlayerIds = Players.Select(x => x.Id).ToList();
            DialogResult = true;
            Close();
        }

        private void RemovePlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            var player = (Player)PlayerList.SelectedItem;

            if (player == null) return;
            player.HasTeam = false;

            var teamService = new TeamService();
            teamService.Save();
            _personService.Save();
            Players.Remove(player);
        }

        private void EditPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            var player = (Player)PlayerList.SelectedItem;
            if (player == null) return;

            var addPlayerWindow = new AddPlayerWindow(true, null)
            {
                Player = player,
                PlayerName = player.Name,
                PersonalNumber = player.PersonalNumber
            };

            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (!addPlayerResult.HasValue) return;

            _personService.Edit(addPlayerWindow.Player, addPlayerWindow.PlayerName, addPlayerWindow.PersonalNumber);
            Players.Remove(addPlayerWindow.Player);
            Players.Add(addPlayerWindow.Player);
        }
    }
}