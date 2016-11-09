using System;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;
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

        public void Add(Person person)
        {
            _personRepository.Add(person);
        }

        public void Edit(Player player, PersonName name, PersonalNumber personId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll();
        }

    }
}