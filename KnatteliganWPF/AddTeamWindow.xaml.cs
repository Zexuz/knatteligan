﻿using System;
using System.Collections.Generic;
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
        public List<Player> Players { get; set; }
        public TeamName TeamName { get; set; }
        public PersonName PersonName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email EmailAddress { get; set; }
        private readonly PersonService _personService;


        public AddTeamWindow()
        {
            InitializeComponent();
            Players = new List<Player>();
            _personService = new PersonService();
            DataContext = this;

            if (Players.Count >= 0)
            {
                AddTeamBtn.IsEnabled = true;
            }

        }

        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow();
            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (addPlayerResult.HasValue && !addPlayerResult.Value) {
                Trace.WriteLine("we did not press the add buttom");
                return;
            }

            DialogResult = true;

            _personService.Add(addPlayerWindow.Player);
            Players.Add(addPlayerWindow.Player);
            PlayerList.ItemsSource = new ObservableCollection<Player>(Players);

            if (Players.Count >= 0)
            {
                AddTeamBtn.IsEnabled = true;
            }
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

        private void RemoveTeam_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditTeam_Click(object sender, RoutedEventArgs e)
        {
        }

        private void PlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveTeamBtn.IsEnabled = true;
            EditTeamBtn.IsEnabled = true;
        }

        private void AddTeamWindowActivated(object sender, EventArgs e)
        {
            PlayerList.ItemsSource = new ObservableCollection<Player>(Players);
        }
    }
}
