using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;

namespace knatteligan.Repositories
{
    public class PersonRepository
    {
        List<Person> _people = new List<Person>();

        private static PersonRepository _instance;

        public static PersonRepository GetInstance()
        {
            return _instance ?? (_instance = new PersonRepository());
        }

        internal void CreatePlayer(PersonName name, PersonalId dob)
        {
            var player = new Player(name, dob);
            _people.Add(player);
        }

        internal void EditPlayer(Player player, PersonName name, PersonalId dob)
        {
            player.Name = name;
            player.PersonId = dob;
        }

        internal void CreateCoach(PersonName name, PersonalId personalId, PhoneNumber phoneNumber, Email email)
        {
            var coach = new Coach(name, personalId, phoneNumber, email);
            _people.Add(coach);
        }

        public List<Person> AllPerson { get; set; }


        public static PersonRepository Instance;

        internal void EditCoach(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            coach.Name = name;
            coach.PhoneNumber = phoneNumber;
            coach.Email = email;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _people;
        }
    }
}