using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for MatchProtocolWindow.xaml
    /// </summary>
    public partial class MatchProtocolWindow : Window
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Match Match { get; set; }
        public List<Player> HomeTeamPlayers { get; set; }
        public List<Player> AwayTeamPlayers { get; set; }

        private readonly TeamService _teamService;
        private readonly PersonService _personService;
        private readonly MatchService _matchService;
        private ListBox _currentFocusedListBox;
        private readonly ObservableCollection<MatchEvent> _matchEventsHome;
        private readonly ObservableCollection<MatchEvent> _matchEventsAway;

        public MatchProtocolWindow(Match match)
        {
            _teamService = new TeamService();
            _personService = new PersonService();
            _matchService = new MatchService();

            Match = match;
            AwayTeam = _teamService.FindById(match.AwayTeamId);
            HomeTeam = _teamService.FindById(match.HomeTeamId);

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
            _matchService.Save();
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
            var team = TeamRepository.GetInstance().FindTeamByPlayerId(player.Id);
            var matchEvent = GetMatchEvent(type, player, team);

            MatchEventRepository.GetInstance().Add(matchEvent);

            player.MatchEvents.Add(matchEvent.Id);
            Match.MatchEventIds.Add(matchEvent.Id);

            PersonRepository.GetInstance().Save();
            MatchRepository.GetInstance().Save();
            if (team.Id == AwayTeam.Id)
            {
                _matchEventsAway.Add(matchEvent);
                AwayTeamGoals.Text =
                    _matchEventsAway.Where(e => e.GetType() == MatchEvents.Goal).ToList().Count.ToString();
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
                    matchEvent = new RedCard(player.Id, Match.Id);
                    break;
                case MatchEvents.YellowCard:
                    matchEvent = new YellowCard(player.Id, Match.Id);
                    break;
                case MatchEvents.Assist:
                    matchEvent = new Assist(player.Id, Match.Id);
                    break;
                case MatchEvents.Goal:
                    matchEvent = new Goal(player.Id, team.Id, Match.Id);
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
                Match.HomeTeamSquadId = players.Select(p => p.Id).ToList();
                HomeTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
            else
            {
                Match.AwayTeamSquadId = players.Select(p => p.Id).ToList();
                AwayTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
        }

        private void GetAllCardsAndSuspenPlayers()
        {
            var players = new List<Player>();
            players.AddRange(HomeTeamPlayers);
            players.AddRange(AwayTeamPlayers);

            foreach (var player in players)
            {
                var redCards = Match.MatchEventIds
                    .Select(eventId => MatchEventRepository.GetInstance().FindById(eventId))
                    .Where(mEvent => mEvent.GetType() == MatchEvents.RedCard && player.Id == mEvent.PlayerId).ToList();
                var yellowCards = Match.MatchEventIds
                    .Select(eventId => MatchEventRepository.GetInstance().FindById(eventId))
                    .Where(mEvent => mEvent.GetType() == MatchEvents.YellowCard && player.Id == mEvent.PlayerId)
                    .ToList();

                if (yellowCards.Count == 0 && redCards.Count == 0)
                    continue;

                Trace.WriteLine(player.Name);
                Trace.WriteLine($"Yellow cards {yellowCards.Count}");
                Trace.WriteLine($"Red cards {redCards.Count}");
                new MatchWeekService().SetSuspensionLength(yellowCards.Count, redCards.Count, player, Match.Id);
            }
        }

        private void RemoveEvent_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedItem = (MatchEvent) listBox.SelectedItems[0];
            MatchEvent matchEvent;
            switch (selectedItem.GetType())
            {
                case MatchEvents.RedCard:
                    matchEvent = (RedCard) selectedItem;
                    break;
                case MatchEvents.YellowCard:
                    matchEvent = (YellowCard) selectedItem;
                    break;
                case MatchEvents.Assist:
                    matchEvent = (Assist) selectedItem;
                    break;
                case MatchEvents.Goal:
                    matchEvent = (Goal) selectedItem;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var player = PersonRepository.GetInstance().FindPlayerById(matchEvent.PlayerId);
            player.MatchEvents.Remove(matchEvent.Id);
            PersonRepository.GetInstance().Save();
            MatchEventRepository.GetInstance().Remove(matchEvent);
            Match.MatchEventIds.Remove(matchEvent.Id);
            MatchRepository.GetInstance().Save();

            var matchWeekService =  new MatchWeekService();

            if (matchEvent.GetType() == MatchEvents.RedCard)
            {
                matchWeekService.RemoveSuspension(3, player, Match.Id);
            }

            if (matchEvent.GetType() == MatchEvents.YellowCard)
            {
                matchWeekService.RemoveSuspension(1, player, Match.Id);
            }
        }

    }
}