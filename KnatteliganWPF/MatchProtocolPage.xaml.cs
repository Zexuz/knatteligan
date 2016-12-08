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
        private List<Player> _awayTeamSquadIds;
        private List<Player> _homeTeamSquadIds;

        public MatchProtocolPage(Match match, League league)
        {
            var matchEventService = new MatchEventService();
            _teamService = new TeamService();
            _personService = new PersonService();
            _matchService = new MatchService();
            var str = league.Name.ToString();



            Match = match;
            AwayTeam = _teamService.FindById(match.AwayTeamId);
            HomeTeam = _teamService.FindById(match.HomeTeamId);

            HomeTeamPlayers =
                HomeTeam.PlayerIds.Select(playerId => _personService.FindPlayerById(playerId)).ToList();

            AwayTeamPlayers =
                AwayTeam.PlayerIds.Select(playerId => _personService.FindPlayerById(playerId)).ToList();

            //this needs to be before adding the list to WPF.. DHOOO!
            InitializeComponent();
            LeagueNameHeader.Text = str;

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

            if (match.AwayTeamSquadId != null)
            {
                _awayTeamSquadIds = match.AwayTeamSquadId.Select(_personService.FindPlayerById).ToList();
                AwayTeamList.ItemsSource = new ObservableCollection<Player>(_awayTeamSquadIds);
            }
            if (match.HomeTeamSquadId != null)
            {
                _homeTeamSquadIds = match.HomeTeamSquadId.Select(_personService.FindPlayerById).ToList();
                HomeTeamList.ItemsSource = new ObservableCollection<Player>(_homeTeamSquadIds);
            }
            _matchEventsTemp = new List<MatchEvent>(Match.MatchEventIds.Select(new MatchEventService().FindById));
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
            _matchService.RemoveMatchEventsFromMatchAndTeams(Match.Id, Match.AwayTeamId, Match.HomeTeamId);
            _matchService.ChangeDate(Match.Id, DatePicker.DisplayDate);
            _matchService.SetStartSquad(Match.Id, true, _homeTeamSquadIds.Select(p => p.Id).ToList());
            _matchService.SetStartSquad(Match.Id, false, _awayTeamSquadIds.Select(p => p.Id).ToList());
            _matchService.SaveMatch(Match.Id, _matchEventsTemp);
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
            var playersTeam = _teamService.FindTeamByPlayerId(player.Id);
            var matchEvent = GetMatchEvent(type, player, playersTeam);

            _matchEventsTemp.Add(matchEvent);
            if (playersTeam.Id == AwayTeam.Id)
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
            PlayerHasMaxCardsOnHim(player);
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
            return (Player)_currentFocusedListBox.SelectedValue;
        }

        private void List_OnMouseUp(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            _currentFocusedListBox = listBox;
            AddGoalButton.IsEnabled = true;
            AddAssistButton.IsEnabled = true;
            AddYellowCardButton.IsEnabled = true;
            AddRedCardButton.IsEnabled = true;

            Trace.WriteLine(GetSelectedPlayerFromList().Name);

            if (listBox?.SelectedItems == null || listBox.SelectedItems.Count == 0) return;
            var player = (Player)listBox.SelectedItem;
            PlayerHasMaxCardsOnHim(player);
        }

        private void OpenAddTeamSquadAndGetPlayerIds(bool isHomeTeam)
        {
            var listOfPlayers = isHomeTeam ? HomeTeamPlayers : AwayTeamPlayers;
            var listOfPlayersAlreadySet = isHomeTeam ? _homeTeamSquadIds : _awayTeamSquadIds;

            var setSquadWindow = new SetTeamSquadWindow(listOfPlayers, Match.Id,
                listOfPlayersAlreadySet.Select(p => p.Id).ToList());
            var resWindow = setSquadWindow.ShowDialog();
            if (resWindow.HasValue && !resWindow.Value)
            {
                Trace.WriteLine("Did not press the 'okey' button");
                return;
            }

            //todo refacto the "set squad code" so we can use it to set squads if the props is alredy in a played match

            var items = setSquadWindow.PlayerListCeckBoxes.ItemsSource;

            var players = ((IEnumerable<CheckBox>)items)
                .Where(checkBox => checkBox.IsChecked.HasValue && checkBox.IsChecked.Value)
                .Select(checkBox => _personService.FindPlayerById((Guid)checkBox.Tag)).ToList();

            if (isHomeTeam)
            {
                _homeTeamSquadIds = players;
                HomeTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
            else
            {
                _awayTeamSquadIds = players;
                AwayTeamList.ItemsSource = new ObservableCollection<Player>(players);
            }
        }

        private void RemoveEvent_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox?.SelectedItems == null || listBox.SelectedItems.Count == 0) return;

            var matchEvent = (MatchEvent)listBox.SelectedItems[0];
            _matchEventsTemp.Remove(matchEvent);
            _matchEventsAway.Remove(matchEvent);
            _matchEventsHome.Remove(matchEvent);


            var homeTeam = new TeamService().FindById(Match.HomeTeamId);
            var awayTeam = new TeamService().FindById(Match.AwayTeamId);

            var homeGoal = GetMatchEventsForTeam(homeTeam).Where(ev => ev.GetType() == MatchEvents.Goal);
            var awayGoal = GetMatchEventsForTeam(awayTeam).Where(ev => ev.GetType() == MatchEvents.Goal);
            HomeTeamGoals.Text = homeGoal.ToList().Count.ToString();
            AwayTeamGoals.Text = awayGoal.ToList().Count.ToString();
        }

        private IEnumerable<MatchEvent> GetMatchEventsForTeam(Team team)
        {
            var list = _matchEventsTemp.Where(mEvent => team.PlayerIds.Contains(mEvent.PlayerId));
            return list.ToList();
        }


        private void PlayerHasMaxCardsOnHim(Player player)
        {
            var playerId = player.Id;
            var redCards = _matchEventsTemp.Where(e => e.PlayerId == playerId && e.GetType() == MatchEvents.RedCard);
            var yellowCards =
                _matchEventsTemp.Where(e => e.PlayerId == playerId && e.GetType() == MatchEvents.YellowCard);

            if (redCards.ToList().Count > 0)
            {
                AddRedCardButton.IsEnabled = false;
            }

            if (yellowCards.ToList().Count > 1)
            {
                AddYellowCardButton.IsEnabled = false;
            }
        }

        private void List_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (_currentFocusedListBox != null) _currentFocusedListBox.SelectedItem = null;
        }
    }
}