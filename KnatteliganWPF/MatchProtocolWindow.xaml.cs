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

        private readonly List<MatchEvent> _matchEventsTemp;

        private readonly PersonService _personService;
        private readonly MatchService _matchService;
        private ListBox _currentFocusedListBox;
        private readonly ObservableCollection<MatchEvent> _matchEventsHome;
        private readonly ObservableCollection<MatchEvent> _matchEventsAway;

        public MatchProtocolWindow(Match match)
        {
            var matchEventService = new MatchEventService();
            var teamService = new TeamService();
            _personService = new PersonService();
            _matchService = new MatchService();

            Match = match;
            AwayTeam = teamService.FindById(match.AwayTeamId);
            HomeTeam = teamService.FindById(match.HomeTeamId);

            HomeTeamPlayers =
                HomeTeam.PlayerIds.Select(playerId => _personService.FindPlayerById(playerId)).ToList();

            AwayTeamPlayers =
                AwayTeam.PlayerIds.Select(playerId => _personService.FindPlayerById(playerId)).ToList();

            //this needs to be before adding the list to WPF.. DHOOO!
            InitializeComponent();

            HomeTeamName.Text = HomeTeam.ToString();
            AwayTeamName.Text = AwayTeam.ToString();

            var homeTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.HomeTeamSquadId.Contains(mEvent.PlayerId));

            var awayTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.AwayTeamSquadId.Contains(mEvent.PlayerId));

            var homeGoal = homeTeamEvents.Where(e => e.GetType() == MatchEvents.Goal);
            var awayGoal = awayTeamEvents.Where(e => e.GetType() == MatchEvents.Goal);


            _matchEventsAway = new ObservableCollection<MatchEvent>(awayTeamEvents.ToList());
            _matchEventsHome = new ObservableCollection<MatchEvent>(homeTeamEvents.ToList());

            AwayTeamMatchEvents.ItemsSource = _matchEventsAway;
            HomeTeamMatchEvents.ItemsSource = _matchEventsHome;

            HomeTeamGoals.Text = homeGoal.ToList().Count.ToString();
            AwayTeamGoals.Text = awayGoal.ToList().Count.ToString();
            DatePicker.DisplayDate = match.MatchDate;


            AwayTeamList.ItemsSource =
                new ObservableCollection<Player>(match.AwayTeamSquadId.Select(_personService.FindPlayerById));
            HomeTeamList.ItemsSource =
                new ObservableCollection<Player>(match.HomeTeamSquadId.Select(_personService.FindPlayerById));

            _matchEventsTemp = new List<MatchEvent>();
        }

        #region OnClick /OnSelected Events

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;

            if (datePicker?.SelectedDate == null)
            {
                throw new Exception("DatePicker is null and therfore not good!");
            }

            _matchService.ChangeDate(Match.Id, datePicker.SelectedDate.Value);
        }

        private void ButtonAddAwayTeamSquad_OnClick(object sender, RoutedEventArgs e)
        {
            OpenAddTeamSquadAndGetPlayerIds(false);
        }

        private void ButtonAddHomeTeamSquad_OnClick(object sender, RoutedEventArgs e)
        {
            OpenAddTeamSquadAndGetPlayerIds(true);
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
            var awayTeam = new TeamService().FindById(Match.AwayTeamId);
            var homeTeam = new TeamService().FindById(Match.HomeTeamId);
            var matchEvent = GetMatchEvent(type, player, awayTeam);

            _matchEventsTemp.Add(matchEvent);
            if (team.Id == AwayTeam.Id)
            {
                var awayEvenst = GetMatchEventsForTeam(awayTeam)
                    .Where(e => e.GetType() == MatchEvents.Goal)
                    .ToList();

                AwayTeamGoals.Text = awayEvenst.Count.ToString();

                _matchEventsAway.Add(matchEvent);
                return;
            }

            var test = GetMatchEventsForTeam(homeTeam);
            HomeTeamGoals.Text = test
                .Where(e => e.GetType() == MatchEvents.Goal)
                .ToList()
                .Count.ToString();

            _matchEventsHome.Add(matchEvent);
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

        private void OpenAddTeamSquadAndGetPlayerIds(bool isHomeTeam)
        {
            var listOfPlayers = isHomeTeam ? HomeTeamPlayers : AwayTeamPlayers;

            var setSquadWindow = new SetTeamSquadWindow(listOfPlayers, Match.Id);
            var resWindow = setSquadWindow.ShowDialog();
            if (resWindow.HasValue && !resWindow.Value)
            {
                Trace.WriteLine("Did not press the 'okey' button");
                return;
            }

            //todo refacto the "set squad code" so we can use it to set squads if the props is alredy in a played match

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

            var matchWeekService = new MatchWeekService();

            if (matchEvent.GetType() == MatchEvents.RedCard)
            {
                matchWeekService.RemoveSuspension(3, player, Match.Id);
            }

            if (matchEvent.GetType() == MatchEvents.YellowCard)
            {
                matchWeekService.RemoveSuspension(1, player, Match.Id);
            }
        }

        private List<MatchEvent> GetMatchEventsForTeam(Team team)
        {
            var list = _matchEventsTemp.Where(mEvent => team.PlayerIds.Contains(mEvent.PlayerId));
            return list.ToList();
        }
    }
}