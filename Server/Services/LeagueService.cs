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

        public IEnumerable<League> GetAll()
        {
            return _leagueRepository.GetAll();
        }

        public void Add(League league)
        {
            _leagueRepository.Add(league);
        }

        public void Remove(Guid leagueId)
        {
            _leagueRepository.Remove(leagueId);
        }

        public void Edit(League league, LeagueName newLeagueName, List<Guid> newTeamIds)
        {
            _leagueRepository.Edit(league, newLeagueName, newTeamIds);
        }

        public League FindById(Guid leagueId)
        {
            return _leagueRepository.FindById(leagueId);
        }

    }
}
