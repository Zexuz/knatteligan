using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class TeamService
    {
        private readonly TeamRepository _teamRepository;

        public TeamService()
        {
            _teamRepository = TeamRepository.GetInstance();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teamRepository.GetAll();
        }

        public void Add(TeamName name)
        {
            _teamRepository.Add(name);
        }

        public void AddTeam(Team team)
        {
            _teamRepository.AddTeam(team);
        }

        public void RemoveTeam(Guid id)
        {
            _teamRepository.RemoveTeam(id);
        }

        public void ChangeTeamName(Team team, TeamName newName)
        {
            team.Name = newName;
        }
    }
}
