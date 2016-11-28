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

        public int GamesPlayedCount => WonMatchIds + LostMatchIds
            + DrawMatchIds;

        public int WonMatchIds { get; set; }
        public int DrawMatchIds { get; set; }
        public int LostMatchIds { get; set; }
        public int DeltaScore => GoalsConcededIds - GoalsScoredIds;
        public int Points => WonMatchIds * 3 + DrawMatchIds;
        public int GoalsConcededIds { get; set; }
        public int GoalsScoredIds { get; set; }

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