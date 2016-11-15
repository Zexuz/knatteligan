using System;
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

        public void Edit(Team team, TeamName newTeamName, IEnumerable<Player> players, Coach coach)
        {
            var index = _teams.FindIndex(x => x.Id == team.Id);
            _teams[index] = new Team(newTeamName, players, coach);
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

            foreach (var player in team.PlayerIds)
            {
                _personService.RemovePlayer(player);
            }


            Save(_teamPath, _teams);
        }

        public Team FindTeamById(Guid id)
        {
            return _teams.Find(x => x.Id == id);
        }

        public Team FindBy(Guid teamId)
        {
            return _teams.First(team => team.Id == teamId);
        }

        public Team FindByPlayerId(Guid playerId)
        {
            return _teams.First(team => team.PlayerIds.Contains(playerId));
        }
    }
}