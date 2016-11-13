using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Repositories;

namespace knatteligan.Services {

    public class MatchRepositoryService {

        private readonly MatchRepository _matchRepository;

        public MatchRepositoryService() {
            _matchRepository = MatchRepository.GetInstance();
        }

        public IEnumerable<Match> GetAll() {
            return _matchRepository.GetAll();
        }

    }

}