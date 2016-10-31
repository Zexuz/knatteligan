using System.Collections.Generic;
using knatteligan.Domain.Entities;

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

        public IEnumerable<Team> GetAllLeagues()
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

        public void AddPlayerToTeam(Player player)
        {
            
        }

    }
}