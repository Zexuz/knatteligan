using System;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
using System.Collections;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Services
{
    public class PersonService
    {
        private readonly PersonRepository _personRepository;

        public PersonService()
        {
            _personRepository = PersonRepository.GetInstance();
        }

        public void CreatePlayer(PersonName name, PersonalId personId)
        {
            _personRepository.CreatePlayer(name, personId);
        }

        public void EditPlayer(Player player, PersonName name, PersonalId personId)
        {
            _personRepository.EditPlayer(player, name, personId);
        }

        public void CreateCoach(PersonName name, PersonalId personId, PhoneNumber phoneNumber, Email email)
        {
            _personRepository.CreateCoach(name, personId, phoneNumber, email);
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