﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;
using knatteligan.Repositories;


namespace DeleteMeConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new TestClass().Start();

            Environment.Exit(0);

            /*

    var teamNames = new[] {"Mad Amigos", "Good Saints", "Crazy Mehicans", "Cool Cats", "The wolfs", "Cowboys"};
              var teams = new List<Team>();

              foreach (var teamName in teamNames)
              {
                  var team = new Team(new TeamName(teamName));

                  for (int i = 0; i < 15; i++)
                  {
                      var player = GenareNewPlayer(team);
                      team.TeamPersons.Add(player.Id);

                      PersonRepository.GetInstance().Add(player);
                  }

                  teams.Add(team);
                  TeamRepository.GetInstance().Add(team);
              }

              Console.WriteLine(teams);

  */
        }

        private static Player GenareNewPlayer(Team team)
        {
            var randomName = GenerateNewPersonFirstAndLastName();
            var randomPersonalNumber = GenerateNewPersonalNumber();

            return new Player(randomName, randomPersonalNumber, team);
        }

        private static PersonalNumber GenerateNewPersonalNumber()
        {
            var date = new DateTime(GenNewNumber(1990, 2010), GenNewNumber(1, 12), GenNewNumber(1, 28));

            var lastfour = $"{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}";
            return new PersonalNumber(date, lastfour);
        }

        private static PersonName GenerateNewPersonFirstAndLastName()
        {
            var firstNames = new[] {"Jhon", "Adam", "Kevein", "Daniel", "Alex", "James", "Peter", "Sandra", "Roberto"};
            var lastNames = new[] {"Doe", "Smith", "Jhonsson", "Karlsson", "Kevinsson", "Moth", "Rodrigas", "Aleandro"};

            var firstName = firstNames[GenNewNumber(0, firstNames.Length)];
            var lastName = lastNames[GenNewNumber(0, lastNames.Length)];
            return new PersonName(firstName, lastName);
        }

        private static int GenNewNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }

    internal class TestClass
    {
        private List<Team> _listOfTeams;

        private Dictionary<int, MatchWeek> _matches;

        public void Start()
        {
            _listOfTeams = new List<Team>
            {
                new Team
                {
                    Name = new TeamName("One")
                },
                new Team
                {
                    Name = new TeamName("Two")
                },
                new Team
                {
                    Name = new TeamName("Three")
                },
                new Team
                {
                    Name = new TeamName("Four")
                }
            };

            var serieCreateer = new CreateSeriesSchedule();
            _matches = serieCreateer.GetFullSeries(_listOfTeams);
            serieCreateer.PrintMatches(_matches);

            Console.WriteLine($"NrOfRoundsIsCorrect: {NrOfRoundsIsCorrect()}");
            Console.WriteLine($"AllTeamsPlaysSameAmountOfMatchesHome: {AllTeamsPlaysSameAmountOfMatchesHome()}");
            Console.WriteLine($"AllTeamsPlaysSameAmountOfMatchesAway: {AllTeamsPlaysSameAmountOfMatchesAway()}");
        }

        public bool NrOfRoundsIsCorrect()
        {
            var number = (_listOfTeams.Count - 1) * 2;
            return number == _matches.Count;
        }

        public bool AllTeamsPlaysSameAmountOfMatchesHome()
        {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys)
            {
                allMatches.AddRange(_matches[keys].Matches.Select(matchId => MatchRepository.GetInstance().Find(matchId)));
            }

            var groupedMatches = allMatches.GroupBy(match => match.HomeTeam);

            return (4 == groupedMatches.ToList().Count);
        }

        public bool AllTeamsPlaysSameAmountOfMatchesAway()
        {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys)
            {
                allMatches.AddRange(_matches[keys].Matches.Select(matchId => MatchRepository.GetInstance().Find(matchId)));
            }

            var groupedMatches = allMatches.GroupBy(match => match.AwayTeam);

            return (4 == groupedMatches.ToList().Count);
        }
    }
}