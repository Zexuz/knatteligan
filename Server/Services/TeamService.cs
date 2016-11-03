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

        public void Add(LeagueName name)
        {
            _teamRepository.Add(name);
        }

        public void RemoveTeam(Team team)
        {
            _teamRepository.Remove(team);
        }

        public void ChangeTeamName(Team team, LeagueName newName)
        {
            team.Name = newName;
        }
    }
}
