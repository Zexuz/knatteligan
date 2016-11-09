﻿using System;
 using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;
 using knatteligan.Services;

namespace knatteligan.Repositories
{
    public class TeamRepository : Repository<Team>
    {
        private readonly List<Team> _teams;

        private static string _teamPath;
        private readonly PersonService _personService = new PersonService();

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

        public void RemoveTeam(Team team)
        {
            _teams.Remove(team);

            var teamPersons = _personService.GetAll().OfType<TeamPerson>().Where(x => x.Team.Id == team.Id);

            foreach (var teamPerson in teamPersons)
            {
                _personService.RemovePlayer(teamPerson.Id);
                //TODO: Remove coaches
            }

            Save(_teamPath, _teams);
        }

        public Team FindTeamById(Guid id)
        {
            return _teams.Find(x => x.Id == id);
        }
    }
}