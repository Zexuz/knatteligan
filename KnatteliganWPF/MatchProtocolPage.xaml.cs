using System.Windows;
using knatteligan.Domain.Entities;
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
    /// Interaction logic for MatchProtocolPage.xaml
    /// </summary>
    public partial class MatchProtocolPage : Page
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Match Match { get; set; }
        public List<Player> HomeTeamPlayers { get; set; }
        public List<Player> AwayTeamPlayers { get; set; }

        private readonly List<MatchEvent> _matchEventsTemp;

        private readonly PersonService _personService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;

        private ListBox _currentFocusedListBox;
        private readonly ObservableCollection<MatchEvent> _matchEventsHome;
        private readonly ObservableCollection<MatchEvent> _matchEventsAway;
        private List<Guid> _awayTeamSquadId;
        private List<Guid> _homeTeamSquadId;

        public MatchProtocolPage(Match match)
        {
            var matchEventService = new MatchEventService();
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

            var homeTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.HomeTeamSquadId.Contains(mEvent.PlayerId));

            var awayTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.AwayTeamSquadId.Contains(mEvent.PlayerId));

            var homeMatchEvents = homeTeamEvents as IList<MatchEvent> ?? homeTeamEvents.ToList();
            var awayMatchEvents = awayTeamEvents as IList<MatchEvent> ?? awayTeamEvents.ToList();

            var homeGoal = homeMatchEvents.Where(e => e.GetType() == MatchEvents.Goal);
            var awayGoal = awayMatchEvents.Where(e => e.GetType() == MatchEvents.Goal);


            _matchEventsAway = new ObservableCollection<MatchEvent>(awayMatchEvents);
            _matchEventsHome = new ObservableCollection<MatchEvent>(homeMatchEvents);

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
            NavigationService?.GoBack();
        }

        private void SaveProtocol_OnClick(object sender, RoutedEventArgs e)
        {
            _matchService.SaveMatch(Match.Id, _matchEventsTemp);
            _matchService.ChangeDate(Match.Id, DatePicker.DisplayDate);
            _matchService.SetStartSquad(Match.Id, true, _homeTeamSquadId);
            _matchService.SetStartSquad(Match.Id, false, _awayTeamSquadId);
            NavigationService?.GoBack();
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
            var playersTeamId = _teamService.FindTeamByPlayerId(player.Id);
            var matchEvent = GetMatchEvent(type, player, AwayTeam);

            _matchEventsTemp.Add(matchEvent);
            if (playersTeamId.Id == AwayTeam.Id)
            {
                var awayEvenst = GetMatchEventsForTeam(AwayTeam)
                    .Where(e => e.GetType() == MatchEvents.Goal)
                    .ToList();

                AwayTeamGoals.Text = awayEvenst.Count.ToString();

                _matchEventsAway.Add(matchEvent);
                return;
            }

            var test = GetMatchEventsForTeam(HomeTeam);
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
                _homeTeamSquadId = players.Select(p => p.Id).ToList();
                HomeTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
            else
            {
                _awayTeamSquadId = players.Select(p => p.Id).ToList();
                AwayTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
        }

        private void RemoveEvent_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox?.SelectedItems == null) return;

            var matchEvent = (MatchEvent) listBox.SelectedItems[0];
            _matchEventsTemp.Remove(matchEvent);
            _matchEventsAway.Remove(matchEvent);
            _matchEventsHome.Remove(matchEvent);


            var homeTeam= new TeamService().FindById(Match.HomeTeamId);
            var awayTeam= new TeamService().FindById(Match.AwayTeamId);

            var homeGoal = GetMatchEventsForTeam(homeTeam).Where(ev => ev.GetType() == MatchEvents.Goal);
            var awayGoal = GetMatchEventsForTeam(awayTeam).Where(ev => ev.GetType() == MatchEvents.Goal);
            HomeTeamGoals.Text = homeGoal.ToList().Count.ToString();
            AwayTeamGoals.Text = awayGoal.ToList().Count.ToString();
        }

        private List<MatchEvent> GetMatchEventsForTeam(Team team)
        {
            var list = _matchEventsTemp.Where(mEvent => team.PlayerIds.Contains(mEvent.PlayerId));
            return list.ToList();
        }
    }
}