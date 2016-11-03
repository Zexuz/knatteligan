using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System.Linq;

namespace knatteligan.Repositories
{

    public class TeamRepository : Repository<Team>
    {
        protected override string FilePath { get; }
        private readonly List<Team> _teams;

        public TeamRepository()
        {
            FilePath = GetFilePath("Teams.xml");
            _teams = Load().ToList();
        }

        public void Add(TeamOrLeagueName name)
        {
            var team = new Team(name);
            AddAndSaveTeam(team);
        }

        public void Remove(Team team)
        {
            _teams.Remove(team);
            Save(_teams);
        }


        public void ChangeTeamName(Team team, TeamOrLeagueName newName)
        {
            team.Name = newName;
            Save(_teams);
        }

        private void AddAndSaveTeam(Team team)
        {
            _teams.Add(team);
            Save(_teams);
        }

        public override IEnumerable<Team> GetAll()
        {
            return _teams;
        }

        public static TeamRepository GetInstance()
        {
            return (TeamRepository)(Repo ?? (Repo = new TeamRepository()));
        }
    }
}