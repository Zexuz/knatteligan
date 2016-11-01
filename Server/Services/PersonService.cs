using System;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class PersonService
    {
        private PersonRepository Repository
        {
            get { return PersonRepository.Instance; }
        }

        public void CreatePlayer(PersonName name, DateTime dob)
        {
            Repository.CreatePlayer(name, dob);
        }
        public void EditPlayer(Player player, string name, DateTime dob)
        {
            Repository.EditPlayer(player, name, dob);
        }
        public void CreateCoach(PersonName name, PhoneNumber phoneNumber, Email email)
        {
            Repository.CreateCoach(name, phoneNumber, email);
        }
        public void EditCoach(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            Repository.EditCoach(coach, name, phoneNumber, email);
        }

    }
}