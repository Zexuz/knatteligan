using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var leagueRepository = new LeagueRepository();
            var teamRepository = new TeamRepository();

            var player1 = new Player("Wayne Rooney", new DateTime(2015-03-03));

            var teamPersonList = new List<TeamPerson> { player1 };

            var team1 = new Team(new TeamOrLeagueName("Manchester United"), teamPersonList);

            var teamList = new List<Team>{team1};

            teamRepository.ChangeTeamName(team1, new TeamOrLeagueName("Liverpool"));

            var league1 = new League(new TeamOrLeagueName("Premier League"), teamList);



            Console.WriteLine(team1.Name.Value);
            Console.ReadLine();

        }
    }
}
