using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Repositories
{

    public class MatchRepository : Repository<Match>
    {

        private readonly List<Match> _matches;
        protected  string FilePath { get; }

        private MatchRepository()
        {
            FilePath = GetFilePath("Matches.xml");
        }

        public void Add(Team homeTeam, Team awayTeam)
        {
            var match = new Match(homeTeam, awayTeam);
            _matches.Add(match);
            AddAndSaveMatch(match);
        }

        private void AddAndSaveMatch(Match match)
        {
            _matches.Add(match);
            //Save(_matches);
        }

        public override IEnumerable<Match> GetAll()
        {
            return _matches;
        }

        public static MatchRepository GetInstance()
        {
            return (MatchRepository)(Repo ?? (Repo = new MatchRepository()));
        }
    }
}