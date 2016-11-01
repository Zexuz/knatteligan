using knatteligan.Domain.Entities;
using System;

namespace knatteligan.Repositories
{
    public class PersonRepository
    {
        private static PersonRepository _instance;

        internal static PersonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonRepository();
                }
                return _instance;
            }
        }

        internal void CreatePlayer(PersonName name, DateTime dob)
        {
            var player = new Player(name, dob);
            people.Add(player);
        }
        internal void EditPlayer(Player player, PersonName name, DateTime dob)
        {
            player.Name = name;
            player.DateOfBirth = dob;
        }

        internal void CreateCoach(PersonName name, PhoneNumber phoneNumber, Email email)
        {
            var coach = new Coach(name, phoneNumber, email);
            people.Add(coach);
        }

        internal void EditCoach(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            coach.Name = name;
            coach.PhoneNumber = phoneNumber;
            coach.Email = email;
        }
    }
}