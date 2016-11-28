using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            // Our new "test" for createSeries
            // new TestClass().Start();
            // Environment.Exit(0);

            for (var j = 0; j < 10; j++)
            {
                var teamNames = new List<string>
                {
                    "The Mad Amigos",
                    "The Crazy Mehicans",
                    "The General Skunks",
                    "The Deadpan Alligators",
                    "The Hushed Ducks",
                    "The Third Partridges",
                    "The Big Ponies",
                    "The Hulking Cockroaches",
                    "The Screeching Cheetahs",
                    "The Moaning Tigers",
                    "The Thankful Crocodiles",
                    "The Typical Pigs",
                    "The Conscious Meerkats",
                    "The Grumpy Hyenas",
                    "The Waggish Baboons",
                    "The Telling Ferrets"
                };


                var teams = new List<Team>();

                foreach (var teamName in teamNames)
                {
                    var coach = GenareNewCoach();
                    var team = new Team
                    {
                        CoachId = coach.Id,
                        Name = new TeamName(teamName),
                        PlayerIds = new List<Guid>()
                    };

                    PersonRepository.GetInstance().Add(coach);

                    for (var i = 0; i < 16; i++)
                    {
                        var player = GenareNewPlayer();

                        PersonRepository.GetInstance().Add(player);
                        team.PlayerIds.Add(player.Id);
                    }


                    teams.Add(team);
                    TeamRepository.GetInstance().Add(team);
                }

                var teamIds = teams.Select(team => team.Id).ToList();

                var seriesCerater = new CreateSeriesSchedule();
                var gameWeeks = seriesCerater.GetFullSeries(teams);


                var ourLeage = new League
                {
                    TeamIds = teamIds,
                    MatchWeeks = gameWeeks,
                    Name = new LeagueName($"Robins Test league {j}")
                };


                LeagueRepository.GetInstance().Add(ourLeage);
            }
        }

        private static Player GenareNewPlayer()
        {
            var randomName = GenerateNewPersonFirstAndLastName();
            var randomPersonalNumber = GenerateNewPersonalNumber();

            return new Player(randomName, randomPersonalNumber);
        }

        private static Coach GenareNewCoach()
        {
            var randomName = GenerateNewPersonFirstAndLastName();

            return new Coach(randomName, new PersonalNumber(new DateTime(1996, 08, 01), "8811"),
                new PhoneNumber("0733209064"), new Email("leon@l.se"));
        }

        private static PersonalNumber GenerateNewPersonalNumber()
        {
            var date = new DateTime(GenNewNumber(1990, 2010), GenNewNumber(1, 12), GenNewNumber(1, 28));

            var lastfour = $"{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}{GenNewNumber(0, 10)}";
            return new PersonalNumber(date, lastfour);
        }

        private static PersonName GenerateNewPersonFirstAndLastName()
        {
            var firstNames = new List<string>{
                "Jhon",
                "Adam",
                "Kevein",
                "Daniel",
                "Alex",
                "James",
                "Peter",
                "Sandra",
                "Robin",
                "Leon",
                "Mattias",
                "Emil",
                "Jesper",
                "Dennis",
                "Arne",
                "Bryngylf",
                "Marcus",
                "Anders",
                "Lars",
                "Karl",
                "Butters",
                "The man in black",
                "Per",
                "Delores",
                "Nils",
                "Hans",
                "Jan",
                "Olof",
                "Peter",
                "Gunnar",
                "Bo",
                "Åke",
                "Martin",
                "Leif",
            };
            var lastNames = new List<string>{
                "Doe",
                "Smith",
                "Jhonsson",
                "Karlsson",
                "Kevinsson",
                "Moth",
                "Rodrigas",
                "Aleandro",
                "Arnesson",
                "Gregerson",
                "Adamson",
                "Lineberg",
                "Edbom",
                "Bertilsson",
                "Didrig",
                "Svensson",
                "Jonsson",
                "Larsson",
                "Throsen",
                "PoorMacDummy",
                "BigMac",
                "Arturson",
                "Wilson",
                "Moore",
                "Jackson",
                "White",
                "Orange"

            };

            var firstName = firstNames[GenNewNumber(0, firstNames.Count)];
            var lastName = lastNames[GenNewNumber(0, lastNames.Count)];
            return new PersonName($"{firstName} {lastName}");
        }

        private static int GenNewNumber(int min, int max)
        {
            Thread.Sleep(10);
            return new Random().Next(min, max);
        }
    }

    internal class TestClass
    {
        private List<Team> _listOfTeams;

        private SerializableDictionary<int, MatchWeek> _matches;

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
                allMatches.AddRange(
                    _matches[keys].MatchIds.Select(matchId => MatchRepository.GetInstance().FindById(matchId)));
            }

            var groupedMatches = allMatches.GroupBy(match => match.HomeTeamId);

            return (4 == groupedMatches.ToList().Count);
        }

        public bool AllTeamsPlaysSameAmountOfMatchesAway()
        {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys)
            {
                allMatches.AddRange(
                    _matches[keys].MatchIds.Select(matchId => MatchRepository.GetInstance().FindById(matchId)));
            }

            var groupedMatches = allMatches.GroupBy(match => match.AwayTeamId);

            return (4 == groupedMatches.ToList().Count);
        }
    }
}