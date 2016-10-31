using System.Collections.Generic;
using knatteligan.Domain.Entities;

namespace knatteligan.Repositories
{
    public class LeagueRepository
    {
        private readonly List<League> _leagues = new List<League>();

        private static LeagueRepository _instance;

        public static LeagueRepository GetInstance()
        {
            return _instance ?? (_instance = new LeagueRepository());
        }

        public IEnumerable<League> GetAllLeagues()
        {
            return _leagues;
        }

        public void AddLeague(League league)
        {
            _leagues.Add(league);
        }

        //TODO: Remove league?

        

    }
}