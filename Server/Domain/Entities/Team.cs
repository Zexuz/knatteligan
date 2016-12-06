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
        public Guid CoachId { get; set; }

        public int GamesPlayedCount => WonMatchIds.Count + LostMatchIds.Count
            + DrawMatchIds.Count;

        public List<Guid> WonMatchIds { get; set; }
        public List<Guid> DrawMatchIds { get; set; }
        public List<Guid> LostMatchIds { get; set; }
        public int DeltaScore => GoalsScoredIds.Count - GoalsConcededIds.Count ;
        public int Points => WonMatchIds.Count * 3 + DrawMatchIds.Count;
        public List<Guid> GoalsConcededIds { get; set; }
        public List<Guid> GoalsScoredIds { get; set; }

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