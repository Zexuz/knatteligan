using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
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
            return _leagueRepository.GetAll();
        }

        public void Add(TeamOrLeagueName name, List<Team> teams )
        {
            _leagueRepository.Add(name, teams);
        }

        public void Remove(Guid leagueGuid)
        {
            _leagueRepository.Remove(leagueGuid);
        }

    }
}
