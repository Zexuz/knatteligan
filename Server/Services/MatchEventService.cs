using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class MatchEventService
    {
        private readonly MatchEventRepository _matchEventRepository;

        public MatchEventService()
        {
            _matchEventRepository = MatchEventRepository.GetInstance();
        }

        public IEnumerable<Goal> GetAllGoals()
        {
            return _matchEventRepository.GetAllGoals();
        }
        public IEnumerable<Assist> GetAllAssists()
        {
            return _matchEventRepository.GetAllAssists();
        }
        public IEnumerable<RedCard> GetAllRedCards()
        {
            return _matchEventRepository.GetAllRedCards();
        }
        public IEnumerable<YellowCard> GetAllYellowCards()
        {
            return _matchEventRepository.GetAllYellowCards();
        }
        public IEnumerable<MatchEvent> GetAll()
        {
            return _matchEventRepository.GetAll();
        }

        public void Add(MatchEvent matchEvent)
        {
            _matchEventRepository.Add(matchEvent);
        }

    }
}
