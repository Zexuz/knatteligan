using System;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace knatteligan.Repositories {

    public class PersonRepository {

        private List<Person> _people = new List<Person>();
        private readonly string _fileName;

        private static PersonRepository _instance;

        public PersonRepository() {
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            _fileName = new Uri(Path.Combine(path, "Persons.xml")).LocalPath;
            Load();
        }

        public static PersonRepository GetInstance() {
            return _instance ?? (_instance = new PersonRepository());
        }

        internal void CreatePlayer(PersonName name, PersonalId dob) {
            var player = new Player(name, dob);
            _people.Add(player);
        }

        internal void EditPlayer(Player player, PersonName name, PersonalId dob) {
            player.Name = name;
            player.PersonId = dob;
        }

        internal void CreateCoach(PersonName name, PersonalId personalId, PhoneNumber phoneNumber, Email email) {
            var coach = new Coach(name, personalId, phoneNumber, email);
            _people.Add(coach);
        }

        internal void EditCoach(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email) {
            coach.Name = name;
            coach.PhoneNumber = phoneNumber;
            coach.Email = email;
        }

        public IEnumerable<Person> GetAllPeople() {
            return _people;
        }

        internal void Save() {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using (Stream stream = File.Open(_fileName, FileMode.Create)) {
                serializer.Serialize(stream, _people);
            }
        }

        private void Load() {
            using (Stream stream = File.Open(_fileName, FileMode.Open)) {
                var serializer = new XmlSerializer(typeof(List<Person>));
                _people = (List<Person>) serializer.Deserialize(stream);
            }
        }

    }

}