﻿using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.Linq;
using knatteligan.Domain.ValueObjects;
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

        public void Edit(Team team, TeamName newTeamName, IEnumerable<TeamPerson> newTeamPersons)
        {
            var index = _teams.FindIndex(x => x.Id == team.Id);
            _teams[index] = new Team(newTeamName, newTeamPersons);
            Save(_teamPath, _teams);
        }

        public override IEnumerable<Team> GetAll()
        {
            return _teams;
        }

        public static TeamRepository GetInstance()
        {
            return (TeamRepository)(Repo ?? (Repo = new TeamRepository()));
        }

        public void Remove(Team team)
        {
            _teams.Remove(team);

            foreach (var teamPerson in team.TeamPersonIds)
            {
                _personService.RemovePlayer(teamPerson);
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