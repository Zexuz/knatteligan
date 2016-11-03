using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Repositories
{

    public class LeagueRepository : Repository<League>
    {

        protected override string FilePath { get; }
        private readonly List<League> _leagues;

        public LeagueRepository()
        {
            FilePath = GetFilePath("League.xml");
            _leagues = Load().ToList();
        }

        public void Add(TeamOrLeagueName name, List<Team> teams )
        {
            var league = new League(name, teams);
            AddAndSaveLeague(league);
        }

        public void Remove(Guid leagueGuid)
        {
            var league = _leagues.Find(l => l.Guid == leagueGuid);
            _leagues.Remove(league);
        }

        public override IEnumerable<League> GetAll()
        {
            return _leagues;
        }

        private void AddAndSaveLeague(League league)
        {
            _leagues.Add(league);
            Save(_leagues);
        }

        public static LeagueRepository GetInstance()
        {
            return (LeagueRepository)(Repo ?? (Repo = new LeagueRepository()));
        }
    }
}