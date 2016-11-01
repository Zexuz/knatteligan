using System;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
using System.Collections;
using System.Collections.Generic;

namespace knatteligan.Services
{
    public class PersonService
    {
        private readonly PersonRepository _personRepository;

        public PersonService()
        {
            _personRepository = PersonRepository.GetInstance();
        }

        public void CreatePlayer(PersonName name, DateTime dob)
        {
            _personRepository.CreatePlayer(name, dob);
        }
        public void EditPlayer(Player player, string name, DateTime dob)
        {
            _personRepository.EditPlayer(player, name, dob);
        }
        public void CreateCoach(PersonName name, PhoneNumber phoneNumber, Email email)
        {
            _personRepository.CreateCoach(name, phoneNumber, email);
        }
        public void EditCoach(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            _personRepository.EditCoach(coach, name, phoneNumber, email);
        }
        public IEnumerable<Person> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }

    }
}