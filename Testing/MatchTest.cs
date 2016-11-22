using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;
using knatteligan.Services;

namespace Testing
{
    [TestFixture]
    public class MatchTest
    {
        [Test]
        public void TestCanAddGoals()
        {
            /*

            const int expectedGoals = 1;
            var homeTeam = new Team(new TeamName("Man U"));
            var awayTeam = new Team(new TeamName("Liverpool"));
            var player1 = new Player(new PersonName("Kalle", "Sten"), new PersonalNumber(new DateTime(1996, 5, 28), "8819"), homeTeam);
            homeTeam.PlayerIds.Add(player1);
            var match1 = new Match(homeTeam, awayTeam)
            {
                HomeTeamId = homeTeam,
                AwayTeamId = awayTeam
            };
            player1.Goals.Add(new Goal(player1.Id, homeTeam.Id));
            player1.YellowCards.Add(new YellowCard(player1.Id, homeTeam.Id));

            var homeTeamGoals = homeTeam.PlayerIds.OfType<Player>().Select(x => x.Goals).Count();
            var player1Goals = player1.Goals.Count;

            Assert.AreEqual(expectedGoals, homeTeamGoals);
            Assert.AreEqual(1, player1Goals);

            */
        }


    }
}
