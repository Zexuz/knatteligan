using System;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class PersonService
    {
        //public Player CreatePlayer(string name, DateTime dOb)

        public void CreatePlayer(Player player)
        {
            //repository.CreatePerson(player);
        }
        public void EditPlayer(Player player)
        {
            //repository.EditPerson(player);
        }
        private PersonRepository Repository
        {
            get { return PersonRepository.Instance; }
        }
    }
}