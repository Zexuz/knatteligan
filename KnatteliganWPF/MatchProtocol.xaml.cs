using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Clicked(object sender, RoutedEventArgs e)
        {
            #region OldStuff
            //int goalResult;
            //int.TryParse(MålLista.SelectedItem.ToString(), out goalResult);

            //for (int i = 0; i < goalResult; i++)
            //{
            //    var goal = new Goal(player.Id, team.Id);
            //    MatchEventRepository.GetInstance().Add(goal);
            //    player.MatchEvents.Add(goal.Id);

            //}

            //int assistResult;
            //int.TryParse(MålLista.SelectedItem.ToString(), out assistResult);

            //for (int i = 0; i < assistResult; i++)
            //{

            //    var assist = new Assist(player.Id, team.Id);
            //    MatchEventRepository.GetInstance().Add(assist);
            //    player.MatchEvents.Add(assist.Id);

            //}

            //int yellowCardResult;
            //int.TryParse(MålLista.SelectedItem.ToString(), out yellowCardResult);

            //for (int i = 0; i < yellowCardResult; i++)
            //{
            //    var yellowCard = new YellowCard(player.Id, team.Id);
            //    MatchEventRepository.GetInstance().Add(yellowCard);
            //    player.MatchEvents.Add(yellowCard.Id);

            //}

            //int redCardResult;
            //int.TryParse(MålLista.SelectedItem.ToString(), out redCardResult);

            //for (int i = 0; i < redCardResult; i++)
            //{
            //    var redCard = new RedCard(player.Id, team.Id);
            //    MatchEventRepository.GetInstance().Add(redCard);
            //    player.MatchEvents.Add(redCard.Id);

            //}


            //var rooney = (Player)HomeTeamList.SelectedItem;

            //var rooneysYellowCards = rooney.MatchEvents.OfType<YellowCard>().ToList();
            //int yellowCardsAmount;
            //int.TryParse(YellowCardsList.SelectedItem.ToString(), out yellowCardsAmount);

            //for (int i = 0; i < yellowCardsAmount; i++)
            //{
            //    rooneysYellowCards.Add(new YellowCard(rooney.Id, Match.Id));
            //}

            //var rooneysMatchEvents = new List<MatchEvent>();
            //rooneysMatchEvents.AddRange(rooneysYellowCards);


            //foreach (var matchEvent in rooneysMatchEvents)
            //{
            //    _matchEventRepository.Add(matchEvent);
            //}
        }

        private void Clear_Click(object sender, RoutedEventArgs e) { }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Goal.Save();
            //PersonRepository.Save();
            //TeamRepository.Save();
            //YellowCardRepository.Save();
            //RedCardRepository.Save();
            //LeagueRepository.Save();
            //MatchRepository.Save(); 
            #endregion
        }

        private void HomeTeamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var playerName = ((Player)HomeTeamList.SelectedItem).Name.ToString();
            PlayerNameLabel.Content = playerName;
        }

        private void Goal_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

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

            var players = ((IEnumerable<CheckBox>)items)
                .Where(checkBox => checkBox.IsChecked.HasValue && checkBox.IsChecked.Value)
                .Select(checkBox => _personService.FindPlayerById((Guid)checkBox.Tag)).ToList();

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

    }
}