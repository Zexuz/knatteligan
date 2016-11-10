using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;
using knatteligan.Repositories;


namespace DeleteMeConsoleApplication {

    internal class Program {

        public static void Main(string[] args) {
            // Our new "test" for createSeries
            // new TestClass().Start();
            // Environment.Exit(0);

            var teamNames = new[] {"Mad Amigos", "Good Saints", "Crazy Mehicans", "Cool Cats", "The wolfs", "Cowboys"};
            var teams = new List<Team>();

            foreach (var teamName in teamNames) {
                var players = new List<Player>();
                var coach = GenareNewCoach();
                var team = new Team(new TeamName(teamName), players, coach);


                for (var i = 0; i < 15; i++) {
                    var player = GenareNewPlayer();

                    players.Add(player);

                    PersonRepository.GetInstance().Add(player);
                }

                teams.Add(team);
                TeamRepository.GetInstance().Add(team);
            }

            var seriesCerater = new CreateSeriesSchedule();

            var ourLeage = new League(new LeagueName("Robins Test league"), teams.Select(team => team.Id).ToList()) {
                MatchWeeks = seriesCerater.GetFullSeries(teams)
            };

            LeagueRepository.GetInstance().Add(ourLeage);
            Console.WriteLine(teams);
        }

        private static Player GenareNewPlayer() {
            var randomName = GenerateNewPersonFirstAndLastName();
            var randomPersonalNumber = GenerateNewPersonalNumber();

            return new Player(randomName, randomPersonalNumber);
        }

        private static Coach GenareNewCoach() {
            var randomName = GenerateNewPersonFirstAndLastName();

            return new Coach(randomName, new PersonalNumber(new DateTime(1996, 08, 01), "8811"),
                new PhoneNumber("0733209064"), new Email("leon@l.se"));
        }

        private static PersonalNumber GenerateNewPersonalNumber() {
            var date = new DateTime(GenNewNumber(1990, 2010), GenNewNumber(1, 12), GenNewNumber(1, 28));

            var lastfour = $"{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}";
            return new PersonalNumber(date, lastfour);
        }

        private static PersonName GenerateNewPersonFirstAndLastName() {
            var firstNames = new[] {"Jhon", "Adam", "Kevein", "Daniel", "Alex", "James", "Peter", "Sandra", "Roberto"};
            var lastNames = new[] {"Doe", "Smith", "Jhonsson", "Karlsson", "Kevinsson", "Moth", "Rodrigas", "Aleandro"};

            var firstName = firstNames[GenNewNumber(0, firstNames.Length)];
            var lastName = lastNames[GenNewNumber(0, lastNames.Length)];
            return new PersonName(firstName);
        }

        private static int GenNewNumber(int min, int max) {
            return new Random().Next(min, max);
        }

    }

    internal class TestClass {

        private List<Team> _listOfTeams;

        private Dictionary<int, MatchWeek> _matches;

        public void Start() {
            _listOfTeams = new List<Team> {
                new Team {
                    Name = new TeamName("One")
                },
                new Team {
                    Name = new TeamName("Two")
                },
                new Team {
                    Name = new TeamName("Three")
                },
                new Team {
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

        public bool NrOfRoundsIsCorrect() {
            var number = (_listOfTeams.Count - 1) * 2;
            return number == _matches.Count;
        }

        public bool AllTeamsPlaysSameAmountOfMatchesHome() {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys) {
                allMatches.AddRange(_matches[keys].Matches.Select(matchId => MatchRepository.GetInstance().Find(matchId)));
            }

            var groupedMatches = allMatches.GroupBy(match => match.HomeTeam);

            return (4 == groupedMatches.ToList().Count);
        }

        public bool AllTeamsPlaysSameAmountOfMatchesAway() {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys) {
                allMatches.AddRange(_matches[keys].Matches.Select(matchId => MatchRepository.GetInstance().Find(matchId)));
            }

            var groupedMatches = allMatches.GroupBy(match => match.AwayTeam);

            return (4 == groupedMatches.ToList().Count);
        }

    }

}