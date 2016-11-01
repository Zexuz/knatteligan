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
            return _teamRepository.GetAllTeams();
        }

        public void AddTeam(Team team)
        {
            _teamRepository.AddTeam(team);
        }

        public void RemoveTeam(Team team)
        {
            _teamRepository.RemoveTeam(team);
        }

        public void AddPlayerToTeam(Team team, Player player)
        {
            _teamRepository.AddPlayerToTeam(team, player);
        }

        public void RemovePlayerFromTeam(Team team, Player player)
        {
            team.TeamPersons.Remove(player);
        }

        public void ChangeTeamName(Team team, TeamOrLeagueName newName)
        {
            team.Name = newName;
        }
    }
}
