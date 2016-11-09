using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System.Linq;
using knatteligan.Services;

namespace knatteligan.Repositories
{

    public class TeamRepository : Repository<Team>
    {
        protected override string FilePath { get; }
        private readonly List<Team> _teams;
        private readonly PersonService _personService;

        public TeamRepository()
        {
            FilePath = GetFilePath("Teams.xml");
            _teams = Load().ToList();
            _personService = new PersonService();
        }

        public void Add(Team team)
        {
            AddAndSaveTeam(team);
        }

        public void AddTeam(Team team)
        {
            AddAndSaveTeam(team);
        }

        public void RemoveTeam(Guid id)
        {
            var team = FindTeamById(id);
            var teamPersons = _personService.GetAll().OfType<TeamPerson>().Where(x => x.Team.Id == id);
            _teams.Remove(team);
            foreach (var teamPerson in teamPersons)
            {
                _personService.RemovePerson(teamPerson.Id);
            }
            Save(_teams);
        }

        public Team FindTeamById(Guid id)
        {
            return _teams.Find(x => x.Id == id);
        }


        public void ChangeTeamName(Team team, TeamName newName)
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