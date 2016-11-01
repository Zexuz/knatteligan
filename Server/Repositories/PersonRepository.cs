using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System;
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

        internal void CreatePlayer(PersonName name, DateTime dob)
        {
            var player = new Player(name, dob);
            _people.Add(player);
        }
        internal void EditPlayer(Player player, PersonName name, DateTime dob)
        {
            player.Name = name;
            player.DateOfBirth = dob;
        }

        internal void CreateCoach(PersonName name, PhoneNumber phoneNumber, Email email)
        {
            var coach = new Coach(name, phoneNumber, email);
            _people.Add(coach);
        }

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