using System;
using knatteligan.Domain.Entities;

namespace knatteligan.Services
{
    public class PersonService
    {
        public Player CreatePlayer(string name, DateTime dOb)
        {
            var player = new Player(name, dOb)
            {
                Id = Guid.NewGuid()
            };

            return player;
        }
    }
}