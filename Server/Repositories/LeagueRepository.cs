using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Repositories
{
    public class LeagueRepository : Repository<League>
    {
        private readonly List<League> _leagues;

        private static string _leaguePath;

        public LeagueRepository()
        {
            _leaguePath = GetFilePath("\\Leagues.xml");
            _leagues = Load<League>(_leaguePath).ToList();
        }

        public void Add(League league)
        {
            _leagues.Add(league);
            Save(_leaguePath, _leagues);
        }

        public override IEnumerable<League> GetAll()
        {
            return _leagues;
        }

        public static LeagueRepository GetInstance()
        {
            return (LeagueRepository)(Repo ?? (Repo = new LeagueRepository()));
        }

        public void Edit(League league, LeagueName newLeagueName, List<Guid> newTeamIds)
        {
            league.Name = newLeagueName;
            league.TeamIds = newTeamIds;
            Save(_leaguePath, _leagues);
        }

        public League FindById(Guid leagueId)
        {
            return _leagues.First(league => league.Id == leagueId);
        }

        public void Remove(Guid leagueId)
        {
            _leagues.Remove(_leagues.First(league => league.Id == leagueId));
        }

        public void Save()
        {
            Save(_leaguePath, _leagues);
        }
    }
}