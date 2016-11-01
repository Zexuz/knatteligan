using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Repositories
{
    public class TeamRepository
    {
        private readonly List<Team> _teams = new List<Team>();

        private static TeamRepository _instance;

        public static TeamRepository GetInstance()
        {
            return _instance ?? (_instance = new TeamRepository());
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }

        public void AddTeam(Team team)
        {
            _teams.Add(team);
        }

        public void RemoveTeam(Team team)
        {
            _teams.Remove(team);
        }

        public void AddPlayerToTeam(Team team, Player player)
        {
            team.TeamPersons.Add(player);
        }

        public void RemovePlayerFromTeam(Team team, Player player)
        {
            team.TeamPersons.Remove(player);
        }

        public void ChangeTeamName(Team team, TeamOrLeagueName newName)
        {
            team.Name = newName;
        }

    }
}