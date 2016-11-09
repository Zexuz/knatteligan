﻿using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;

namespace knatteligan.Repositories
{
    public class LeagueRepository : Repository<League>
    {
        private readonly List<League> _leagues;

        private static string _leaguePath;

        public LeagueRepository()
        {
            _leaguePath = GetFilePath("Leagues.xml");
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
            return (LeagueRepository) (Repo ?? (Repo = new LeagueRepository()));
        }
    }
}