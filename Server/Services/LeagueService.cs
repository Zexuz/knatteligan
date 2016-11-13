using System;
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
            return _leagueRepository.GetAll();
        }

        public void Add(League league)
        {
            _leagueRepository.Add(league);
        }


        public void Remove(Guid leagueGuid)
        {
            throw new NotImplementedException();
        }

        public League FindById(Guid leagueGuid)
        {
            return _leagueRepository.FindBy(leagueGuid);
        }

    }
}
