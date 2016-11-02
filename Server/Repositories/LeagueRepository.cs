using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;

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

        public void AddLeague(League league)
        {
            _leagues.Add(league);
            Save(_leagues);
        }

        public override IEnumerable<League> GetAll()
        {
            return _leagues;
        }

        public static LeagueRepository GetInstance()
        {
            return (LeagueRepository)(Repo ?? (Repo = new LeagueRepository()));
        }
    }
}