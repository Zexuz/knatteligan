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
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll();
        }
        public IEnumerable<Player> GetAllPlayers()
        {
            return _personRepository.GetAllPlayers();
        }

        public void RemovePlayer(Guid id)
        {
            _personRepository.RemovePlayer(id);
        }

        public Person FindPlayerById(Guid id)
        {
            return _personRepository.FindPlayerById(id);
        }

        public Coach FindCoachById(Guid id)
        {
            return _personRepository.FindCoachById(id);
        }
    }
}