using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepository;

        public MatchService()
        {
            _matchRepository = MatchRepository.GetInstance();
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.GetAll();
        }

        public Match FindById(Guid id)
        {
            return _matchRepository.FindById(id);
        }

        public void Add(Match match)
        {
            _matchRepository.Add(match);
        }

        public void Save()
        {
           _matchRepository.Save();
        }
    }
}