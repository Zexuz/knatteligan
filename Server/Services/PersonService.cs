﻿using System;
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
            _personRepository.Edit(player, name, personId);
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll();
        }

        public Person FindById(Guid personId)
        {
            return _personRepository.FindBy(personId);
        }

    }
}