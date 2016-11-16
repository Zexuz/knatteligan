using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;

namespace knatteligan.Repositories
{
    public class MatchRepository : Repository<Match>
    {
        private readonly List<Match> _matches;

        private static string _matchPath;

        private MatchRepository()
        {
            _matchPath = GetFilePath("\\Matches.xml");
            _matches = Load<Match>(_matchPath).ToList();
        }

        public void Add(Match match)
        {
            _matches.Add(match);
            Save(_matchPath, _matches);
        }

        public Match Find(Guid guid)
        {
            return _matches.First(match => match.Id == guid);
        }

        public override IEnumerable<Match> GetAll()
        {
            return _matches;
        }

        public static MatchRepository GetInstance()
        {
            return (MatchRepository)(Repo ?? (Repo = new MatchRepository()));
        }

        public void Save()
        {
            Save(_matchPath, _matches);
        }
    }
}