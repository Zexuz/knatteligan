using System.Collections.Generic;
using knatteligan.Domain.Entities;
using System.IO;
using System;
using System.Xml.Serialization;

namespace knatteligan.Repositories
{
    public class LeagueRepository
    {
        private List<League> _leagues = new List<League>();
        private string _fileName;
        private static LeagueRepository _instance;

        public LeagueRepository()
        {
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            _fileName = new Uri(Path.Combine(path, "League.xml")).LocalPath;
            Load();
        }

        public static LeagueRepository GetInstance()
        {
            return _instance ?? (_instance = new LeagueRepository());
        }

        public IEnumerable<League> GetAllLeagues()
        {
            return _leagues;
        }

        public void AddLeague(League league)
        {
            _leagues.Add(league);
            Save();
        }

        private void Load()
        {
            using (Stream stream = File.Open(_fileName, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(List<League>));
                _leagues = (List<League>)serializer.Deserialize(stream);
            }
        }
        internal void Save()
        {
            var serializer = new XmlSerializer(typeof(List<League>));
            using (Stream stream = File.Open(_fileName, FileMode.Create))
            {
                serializer.Serialize(stream, _leagues);
            }
        }
    }
}