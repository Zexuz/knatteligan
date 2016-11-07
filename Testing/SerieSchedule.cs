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

        private Dictionary<int, List<Match>> matches;

        [OneTimeSetUp]
        public void Init()
        {
            _listOfTeams = new List<Team>
            {
                new Team
                {
                    Name = new LeagueName("One")
                },
                new Team
                {
                    Name = new LeagueName("two")
                },
                new Team
                {
                    Name = new LeagueName("tree")
                },
                new Team
                {
                    Name = new LeagueName("four")
                }
            };

            var serieCreateer = new CreateSeriesSchedule();
            matches = serieCreateer.GetFullSeries(_listOfTeams);
            serieCreateer.PrintMatches(matches);
        }

        [Test]
        public void NrOfRoundsIsCorrect()
        {
            var number = (_listOfTeams.Count - 1) * 2;
            Assert.AreEqual(number, matches.Count);
        }

        [Test]
        public void AllTeamsPlaysSameAmountOfMatchesHome()
        {
            var allMatches = new List<Match>();

            foreach (var keys in matches.Keys)
            {
                allMatches.AddRange(matches[keys]);
            }

            var groupedMatches = allMatches.GroupBy(match => match.HomeTeam);

            Assert.AreEqual(4,groupedMatches.ToList().Count);
        }

        [Test]
        public void AllTeamsPlaysSameAmountOfMatchesAway()
        {
            var allMatches = new List<Match>();

            foreach (var keys in matches.Keys)
            {
                allMatches.AddRange(matches[keys]);
            }

            var groupedMatches = allMatches.GroupBy(match => match.AwayTeam);

            Assert.AreEqual(4,groupedMatches.ToList().Count);
        }
    }
}