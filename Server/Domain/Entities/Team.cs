using System;
using System.Collections.Generic;
using System.Linq;

using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team : Entity
    {
        public TeamName Name { get; set; }
        public List<Guid> PlayerIds { get; set; } = new List<Guid>();
        public List<Guid> Goals { get; set; } = new List<Guid>();
        public Guid CoachId { get; set; }

        public Team() { }


        public Team(TeamName name, IEnumerable<Player> players, Coach coach)
        {
            Name = name;
            CoachId = coach.Id;
            PlayerIds = players.Select(player => player.Id).ToList();
        }
        public override string ToString()
        {
            return Name.Value;
        }
    }
}