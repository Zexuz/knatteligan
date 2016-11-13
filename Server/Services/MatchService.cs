using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Match FindById(Guid mathId)
        {
            return _matchRepository.Find(mathId);
        }
    }
}
