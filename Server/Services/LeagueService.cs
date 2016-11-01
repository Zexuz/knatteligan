using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class LeagueService
    {
        private readonly LeagueRepository _leagueRepository;

        public LeagueService()
        {
            _leagueRepository = LeagueRepository.GetInstance();
        }

        public IEnumerable<League> GetAllLeagues()
        {
            return _leagueRepository.GetAllLeagues();
        }

        public void AddLeague(League league)
        {
            _leagueRepository.AddLeague(league);
        }

    }
}
