using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System.IO;
using System;
using System.Xml.Serialization;

namespace knatteligan.Repositories
{
    public class TeamRepository
    {
        private List<Team> _teams = new List<Team>();
        private readonly string _fileName;
        private static TeamRepository _instance;

        public TeamRepository()
        {
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            _fileName = new Uri(Path.Combine(path, "Teams.xml")).LocalPath;
            Load();
        }

        public static TeamRepository GetInstance()
        {
            return _instance ?? (_instance = new TeamRepository());
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }

        public void AddTeam(Team team)
        {
            _teams.Add(team);
            Save();
        }

        public void RemoveTeam(Team team)
        {
            _teams.Remove(team);
            Save();
        }

        public void AddPlayerToTeam(Team team, Player player)
        {
            team.TeamPersons.Add(player);
            Save();
        }

        public void RemovePlayerFromTeam(Team team, Player player)
        {
            team.TeamPersons.Remove(player);
            Save();
        }

        public void ChangeTeamName(Team team, TeamOrLeagueName newName)
        {
            team.Name = newName;
            Save();
        }

        private void Load()
        {
            using (Stream stream = File.Open(_fileName, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(List<Team>));
                _teams = (List<Team>)serializer.Deserialize(stream);
            }
        }
        internal void Save()
        {
            var serializer = new XmlSerializer(typeof(List<Team>));
            using (Stream stream = File.Open(_fileName, FileMode.Create))
            {
                serializer.Serialize(stream, _teams);
            }
        }

    }
}