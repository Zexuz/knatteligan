using System.Collections.Generic;
using knatteligan.Domain.Entities;

namespace knatteligan.Repositories
{

    public class MatchRepository : Repository<Match>
    {

        private readonly List<Match> _matches;
        protected override string FilePath { get; }

        private MatchRepository()
        {
            FilePath = GetFilePath("Matches.xml");
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