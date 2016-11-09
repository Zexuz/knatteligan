﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {

        public Player player { get; set; }
        public Team team { get; set; }
        public Match match { get; set; }



        public MatchProtocol()
        {
            InitializeComponent();

            //var team = new Team(new TeamName("Man U"));
            //var player = new Player(new PersonName("Kalle", "Karlsson"), new PersonalNumber(new DateTime(1996, 8, 1), "8817"), team);


            //När man klickar på update, ta antal goals och utför denna

        }
        
        
        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void Update_Clicked(object sender, RoutedEventArgs e)
        {

            int goalResult;
            int.TryParse(MålLista.SelectedItem.ToString(), out goalResult);

            for (int i = 0; i < goalResult; i++)
            {
                player.Goals.Add(new Goal(player.Id, team.Id));

            }

            int assistResult;
            int.TryParse(MålLista.SelectedItem.ToString(), out assistResult);

            for (int i = 0; i < assistResult; i++)
            {
                player.Assists.Add(new Assist(player.Id, match.Id));

            }

            int yellowCardResult;
            int.TryParse(MålLista.SelectedItem.ToString(), out yellowCardResult);

            for (int i = 0; i < yellowCardResult; i++)
            {
                player.YellowCards.Add(new YellowCard(player.Id, match.Id));

            }

            int redCardResult;
            int.TryParse(MålLista.SelectedItem.ToString(), out redCardResult);

            for (int i = 0; i < redCardResult; i++)
            {
                player.RedCards.Add(new RedCard(player.Id, match.Id));

            }








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
    }
}



