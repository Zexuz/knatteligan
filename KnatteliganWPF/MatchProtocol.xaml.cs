﻿using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
using knatteligan.Domain.ValueObjects;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        private readonly TeamRepository _teamRepository = new TeamRepository();
        private readonly PersonRepository _personRepository = new PersonRepository();
        private readonly MatchEventRepository _matchEventRepository = new MatchEventRepository();




        //public Player player { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Match Match { get; set; }



        public MatchProtocol()
        {
            InitializeComponent();


            //var homeTeam = new Team(new TeamName("Liverpool"));
            //var awayTeam = new Team(new TeamName("Chelsea"));

            //_teamRepository.Add(homeTeam);
            //_teamRepository.Add(awayTeam);

            //var player = new Player(new PersonName("Kalle", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), homeTeam);
            //var player2 = new Player(new PersonName("Nils", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), homeTeam);
            //var player3 = new Player(new PersonName("Bertil", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), homeTeam);

            //var player4 = new Player(new PersonName("Adam", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), awayTeam);
            //var player5 = new Player(new PersonName("Donald", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), awayTeam);
            //var player6 = new Player(new PersonName("Hillary", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), awayTeam);

            //_personRepository.Add(player);
            //_personRepository.Add(player2);
            //_personRepository.Add(player3);
            //_personRepository.Add(player4);
            //_personRepository.Add(player5);
            //_personRepository.Add(player6);

            //Match = new Match(homeTeam, awayTeam);

            

            //var homeTeamPlayerGuids = _personRepository.GetAll().OfType<Player>().Where(x => x.Team.Id == homeTeam.Id).Select(x => x.Id).ToList();
            //homeTeam.TeamPersons = homeTeamPlayerGuids;

            //var homeTeamPlayers = _personRepository.GetAll().OfType<Player>().Where(x => x.Team.Id == homeTeam.Id);
            //HomeTeamList.ItemsSource = homeTeamPlayers;




            
            
        }
       

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void Update_Clicked(object sender, RoutedEventArgs e)
        {

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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Goal.Save();
            //PersonRepository.Save();
            //TeamRepository.Save();
            //YellowCardRepository.Save();
            //RedCardRepository.Save();
            //LeagueRepository.Save();
            //MatchRepository.Save();
        }

        private void HomeTeamList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var playerName = ((Player)HomeTeamList.SelectedItem).Name.ToString();
            PlayerNameLabel.Content = playerName;
        }

        private void Goal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}



