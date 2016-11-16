﻿using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        private readonly TeamService _teamService = new TeamService();
        private readonly PersonService _personService = new PersonService();

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Match Match { get; set; }
        public List<Player> HomeTeamPlayers { get; set; }
        public List<Player> AwayTeamPlayers { get; set; }

        private ListBox _currentFocusedListBox;
        private readonly ObservableCollection<MatchEvent> _matchEventsHome;
        private readonly ObservableCollection<MatchEvent> _matchEventsAway;

        public MatchProtocol(Match match)
        {
            Match = match;
            AwayTeam = _teamService.FindTeamById(match.AwayTeam);
            HomeTeam = _teamService.FindTeamById(match.HomeTeam);

            HomeTeamPlayers =
                HomeTeam.PlayerIds.Select(playerId => _personService.FindPlayerById(playerId)).ToList();

            AwayTeamPlayers =
                AwayTeam.PlayerIds.Select(playerId => _personService.FindPlayerById(playerId)).ToList();

            //this needs to be before adding the list to WPF.. DHOOO!
            InitializeComponent();

            HomeTeamName.Text = HomeTeam.ToString();
            AwayTeamName.Text = AwayTeam.ToString();


            _matchEventsAway = new ObservableCollection<MatchEvent>();
            _matchEventsHome = new ObservableCollection<MatchEvent>();

            AwayTeamMatchEvents.ItemsSource = _matchEventsAway;
            HomeTeamMatchEvents.ItemsSource = _matchEventsHome;
        }

        #region OnClick /OnSelected Events
        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            if (datePicker?.SelectedDate == null)
            {
                throw new Exception("DatePicker is null and therfore not good!");
            }

            Match.MatchDate = datePicker.SelectedDate.Value;
            //TODO: Repo or service?
            MatchRepository.GetInstance().Save();
        }

        private void ButtonAddAwayTeamSquad_OnClick(object sender, RoutedEventArgs e)
        {
            AddTeamSquad(false);
        }

        private void ButtonAddHomeTeamSquad_OnClick(object sender, RoutedEventArgs e)
        {
            AddTeamSquad(true);
        }

        private void CancelProtocol_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SaveProtocol_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void AddGoal_OnClick(object sender, RoutedEventArgs e)
        {
            AddMatchEvent(MatchEvents.Goal);
        }

        private void AddAssist_OnClick(object sender, RoutedEventArgs e)
        {
            AddMatchEvent(MatchEvents.Assist);
        }

        private void AddYellowCard_OnClick(object sender, RoutedEventArgs e)
        {
            AddMatchEvent(MatchEvents.YellowCard);

        }

        private void AddRedCard_OnClick(object sender, RoutedEventArgs e)
        {
            AddMatchEvent(MatchEvents.RedCard);
        }
        #endregion

        private void AddMatchEvent(MatchEvents type)
        {
            var player = GetSelectedPlayerFromList();
            var team = TeamRepository.GetInstance().FindByPlayerId(player.Id);
            var matchEvent = GetMatchEvent(type, player, team);

            MatchEventRepository.GetInstance().Add(matchEvent);

            player.MatchEvents.Add(matchEvent.Id);
            Match.MatchEvents.Add(matchEvent.Id);

            PersonRepository.GetInstance().Save();
            MatchRepository.GetInstance().Save();
            if (team.Id == AwayTeam.Id)
            {
                _matchEventsAway.Add(matchEvent);
                AwayTeamGoals.Text = _matchEventsAway.Where(e => e.GetType() == MatchEvents.Goal).ToList().Count.ToString();
                return;
            }
            _matchEventsHome.Add(matchEvent);
            HomeTeamGoals.Text = _matchEventsHome.Where(e => e.GetType() == MatchEvents.Goal).ToList().Count.ToString();
            GetAllCardsAndSuspenPlayers();

        }

        private MatchEvent GetMatchEvent(MatchEvents type, Player player, Team team)
        {
            MatchEvent matchEvent;
            switch (type)
            {
                case MatchEvents.RedCard:
                    matchEvent = new RedCard(player.Id,Match.Id);
                    break;
                case MatchEvents.YellowCard:
                    matchEvent = new YellowCard(player.Id,Match.Id);
                    break;
                case MatchEvents.Assist:
                    matchEvent = new Assist(player.Id,Match.Id);
                    break;
                case MatchEvents.Goal:
                    matchEvent = new Goal(player.Id,team.Id,Match.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return matchEvent;
        }

        private Player GetSelectedPlayerFromList()
        {
            return (Player) _currentFocusedListBox.SelectedValue;
        }

        private void List_OnSelected(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            _currentFocusedListBox = listBox;
        }

        private void AddTeamSquad(bool isHomeTeam)
        {
            var listOfPlayers = isHomeTeam ? HomeTeamPlayers : AwayTeamPlayers;

            var setSquadWindow = new SetTeamSquadWindow(listOfPlayers, Match.Id);
            var resWindow = setSquadWindow.ShowDialog();
            if (resWindow.HasValue && !resWindow.Value)
            {
                Trace.WriteLine("Did not press the 'okey' button");
                return;
            }

            var items = setSquadWindow.PlayerListCeckBoxes.ItemsSource;

            var players = ((IEnumerable<CheckBox>) items)
                .Where(checkBox => checkBox.IsChecked.HasValue && checkBox.IsChecked.Value)
                .Select(checkBox => _personService.FindPlayerById((Guid) checkBox.Tag)).ToList();

            if (isHomeTeam)
            {
                Match.HomeTeamSquad = players.Select(p => p.Id).ToList();
                HomeTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
            else
            {
                Match.AwayTeamSquad = players.Select(p => p.Id).ToList();
                AwayTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
        }

        private void GetAllCardsAndSuspenPlayers() {

            var players = new List<Player>();
            players.AddRange(HomeTeamPlayers);
            players.AddRange(AwayTeamPlayers);

            foreach (var player in players) {


            var redCards = Match.MatchEvents
                .Select(eventId => MatchEventRepository.GetInstance().Find(eventId))
                .Where(mEvent => mEvent.GetType() ==MatchEvents.RedCard && player.Id == mEvent.PlayerGuid).ToList();
            var yellowCards = Match.MatchEvents
                .Select(eventId => MatchEventRepository.GetInstance().Find(eventId))
                .Where(mEvent => mEvent.GetType() ==MatchEvents.YellowCard && player.Id == mEvent.PlayerGuid ).ToList();

                if(yellowCards.Count == 0 && redCards.Count == 0)
                    continue;

                Trace.WriteLine(player.Name);
                Trace.WriteLine($"Yellow cards {yellowCards.Count}");
                Trace.WriteLine($"Red cards {redCards.Count}");
                new MatchWeekService().SetSuspensionLength(yellowCards.Count,redCards.Count,player,Match.Id);
            }
        }
    }
}