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

        public void Add(PersonName name, PersonalNumber personId, Team team)
        {
            _personRepository.Add(name, personId, team);
        }

        public void Edit(Player player, PersonName name, PersonalNumber personId)
        {
            _personRepository.Edit(player, name, personId);
        }

        public void Add(PersonName name, PersonalNumber personId, PhoneNumber phoneNumber, Email email, Team team)
        {
            _personRepository.Add(name, personId, phoneNumber, email, team);
        }

        public void Edit(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            _personRepository.Edit(coach, name, phoneNumber, email);
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll();
        }
    }
}