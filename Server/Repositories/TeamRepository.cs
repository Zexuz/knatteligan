﻿using System;
 using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;

namespace knatteligan.Repositories
{
    public class TeamRepository : Repository<Team>
    {
        private readonly List<Team> _teams;

        private static string _teamPath;

        public TeamRepository()
        {
            _teamPath = GetFilePath("\\Teams.xml");
            _teams = Load<Team>(_teamPath).ToList();
        }

        public void Add(Team team)
        {
            _teams.Add(team);
            Save(_teamPath, _teams);
        }

        public override IEnumerable<Team> GetAll()
        {
            return _teams;
        }

        public static TeamRepository GetInstance()
        {
            return (TeamRepository) (Repo ?? (Repo = new TeamRepository()));
        }

        public Team FindBy(Guid teamId)
        {
            return _teams.First(team => team.Id == teamId);
        }
    }
}