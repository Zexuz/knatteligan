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

        public void Add(Team team)
        {
            _teamRepository.Add(team);
        }

        public void Remove(Team team)
        {
            _teamRepository.Remove(team);
        }

        public void Edit(Team team, TeamName newTeamName, IEnumerable<Player> players, Coach coach)
        {
            _teamRepository.Edit(team, newTeamName, players, coach);
        }

        public Team FindTeamById(Guid id)
        {
            return _teamRepository.FindTeamById(id);
        }

        public void ChangeTeamName(Team team, TeamName newName)
        {
            team.Name = newName;
        }

        public Team FindById(Guid teamId)
        {
            return _teamRepository.FindBy(teamId);
        }
    }
}
