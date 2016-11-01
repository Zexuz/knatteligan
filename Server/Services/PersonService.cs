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

        public void CreatePlayer(Player player)
        {
            //repository.CreatePerson(player);
        }
        public void EditPlayer(Player player)
        {
            //repository.EditPerson(player);
        }

    }
}