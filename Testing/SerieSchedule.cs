/*

using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class SerieSchedule
    {
        private List<Team> _listOfTeams;

        private Dictionary<int, List<Match>> _matches;

        [OneTimeSetUp]
        public void Init()
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
        }

        [Test]
        public void NrOfRoundsIsCorrect()
        {
            var number = (_listOfTeams.Count - 1) * 2;
            Assert.AreEqual(number, _matches.Count);
        }

        [Test]
        public void AllTeamsPlaysSameAmountOfMatchesHome()
        {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys)
            {
                allMatches.AddRange(_matches[keys]);
            }

            var groupedMatches = allMatches.GroupBy(match => match.HomeTeam);

            Assert.AreEqual(4,groupedMatches.ToList().Count);
        }

        [Test]
        public void AllTeamsPlaysSameAmountOfMatchesAway()
        {
            var allMatches = new List<Match>();

            foreach (var keys in _matches.Keys)
            {
                allMatches.AddRange(_matches[keys]);
            }

            var groupedMatches = allMatches.GroupBy(match => match.AwayTeam);

            Assert.AreEqual(4,groupedMatches.ToList().Count);
        }
    }
}

*/